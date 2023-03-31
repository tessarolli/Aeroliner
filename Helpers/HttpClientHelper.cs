using System.Net.Http.Headers;
using System.Text;

namespace Aeroliner.Helpers
{
    public class HttpClientHelper
    {
        private readonly HttpClient _httpClient;

        public HttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetStringAsync(string uri, string contentType = "application/json")
        {
            SetRequestHeaders(contentType);

            var response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }


        public async Task<T?> GetObjectAsync<T>(string uri, string contentType = "application/json")
        {
            SetRequestHeaders(contentType);

            var response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            try
            {
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T data, string contentType = "application/json")
        {
            SetRequestHeaders(contentType);

            var payload = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, contentType);

            return await _httpClient.PostAsync(uri, payload);
        }

        private void SetRequestHeaders(string contentType)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
        }
    }
}
