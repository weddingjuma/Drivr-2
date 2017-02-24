using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Drivr.API
{
    public class ApiWrapper
    {
        private readonly string _token;
        private readonly string _uri;
        public ApiWrapper(string token)
        {
            _uri = "https://localhost:44399"; //TODO: change API URI in production
            _token = token;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                var body = new StringContent($"username={username}&password={password}");

                var response = await client.PostAsync(_uri + "/token", body);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Token>(content);
                    if (obj != null)
                    {
                        return obj.AccessToken;
                    }
                }

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

                var response = await client.GetAsync(_uri + query);
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
        public TimeSpan ExpiresIn { get; set; }
    }
}