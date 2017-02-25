using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Drivr.API
{
    public class ApiWrapper
    {
        private string _token;
        private readonly string _uri;
        public ApiWrapper(string token)
        {
            _token = token;
            _uri = "http://169.254.80.80:55214";  //TODO: change API URI in production
        }

        public async Task<string> Authenticate(string username, string password)
        {
           
            try
            {
                using (var client = new HttpClient())
                {
                    var body = new StringContent($"username={username}&password={password}");
                    body.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                    client.BaseAddress = new Uri(_uri);
                    var response = await client.PostAsync("token", body);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var obj = JsonConvert.DeserializeObject<Token>(content);
                        if (obj != null)
                        {
                            _token = obj.AccessToken;
                            return obj.AccessToken;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // TODO: Log error
            }
            return null;
        }

        public async Task<T> Get<T>(string query)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                client.BaseAddress = new Uri(_uri);
                var response = await client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<T>(content);
                    if (obj != null)
                    {
                        return obj;
                    }
                }
                return default(T);
            }
        }
        
        public void Post()
        {
            
        }
    }

    internal class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}