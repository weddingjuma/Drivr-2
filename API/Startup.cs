using System;
using System.Diagnostics;
using System.Text;
using API.Migrations;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddSingleton(_ => Configuration);

            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<Seed>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, Seed seeder)
        {
            seeder.SeedAdminUser();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var devPort = Configuration.GetValue<int>("iisSettings:iisExpress:sslPort");
            Debug.WriteLine(devPort);
            app.Use(async (context, next) =>
            {
                var request = context.Request;

                if (request.IsHttps)
                {
                    await next();
                }
                else
                {
                    var host = env.IsDevelopment()
                        ? new HostString(request.Host.Host, devPort)
                        : new HostString(request.Host.Host);

                    string newUrl = $"https://{host}{request.PathBase}{request.Path}{request.QueryString}";
                    context.Response.Redirect(newUrl, true);
                }
            });
            //app.UseStaticFiles();

            app.UseIdentity();


            // secretKey contains a secret passphrase only your server knows
            var secretKey = Configuration.GetSection("AppSettings")["SecretKey"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var options = new TokenProviderOptions
            {
                Path = "/token",
                Audience = "User",
                Issuer = "Drivr",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = (username, password) =>
                {
                    using (
                        var serviceScope =
                            app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                        var user = userManager.FindByNameAsync(username).Result;

                        if (user != null && userManager.CheckPasswordAsync(user, password).Result)
                        {
                            return user;
                        }

                        // Credentials are invalid, or account doesn't exist
                        return null;
                    }
                }
            };

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "Drivr",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "User",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));

            app.UseMvc();
        }
    }
}
