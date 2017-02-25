using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Drivr.Models;
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

        public async Task<ApiResponse<string>> Authenticate(string username, string password)
        {
            var apiResponse = new ApiResponse<string>();
            using (var client = new HttpClient())
            {

                try
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
                            apiResponse.Object = _token;
                        }
                    }
                }
                catch (Exception exception)
                {
                    // TODO: Log error
                    apiResponse.Error = exception.Message;
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<T>> Get<T>(string query) where T : class
        {
            var apiResponse = new ApiResponse<T>();
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                    client.BaseAddress = new Uri(_uri);
                    var response = await client.GetAsync(query);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var obj = JsonConvert.DeserializeObject<T>(content);
                        if (obj != null)
                        {
                            apiResponse.Object = obj;
                        }
                    }
                }
                catch (Exception exception)
                {
                    apiResponse.Error = exception.ToString();
                }
                return apiResponse;
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