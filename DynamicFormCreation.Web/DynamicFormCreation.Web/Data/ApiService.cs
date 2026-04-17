using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace DynamicFormCreation.Web.Data
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = string.Empty;

        public ApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["ApiSettings:BaseUrl"] ?? throw new Exception("API BaseUrl not configured");
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(_baseUrl + endpoint);
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data)!;
        }

        public async Task<T> PostAsync<T>(string endpoint, object model)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(model),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(_baseUrl + endpoint, content);
            var data = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(data)!;
        }
    }
}