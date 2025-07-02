using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Phanlichphongkham.Services
{
    public class ApiService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly Dictionary<string, string> _defaultHeaders;

        public ApiService(string baseUrl, int timeoutSeconds = 30)
        {
            _baseUrl = baseUrl?.TrimEnd('/') ?? throw new ArgumentNullException(nameof(baseUrl));
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(timeoutSeconds)
            };
            _defaultHeaders = new Dictionary<string, string>();
        }

        // Thêm header mặc định
        public void AddDefaultHeader(string key, string value)
        {
            _defaultHeaders[key] = value;
        }

        // Xóa header mặc định
        public void RemoveDefaultHeader(string key)
        {
            _defaultHeaders.Remove(key);
        }

        // Thiết lập Bearer Token
        public void SetBearerToken(string token)
        {
            AddDefaultHeader("Authorization", $"Bearer {token}");
        }

        // Thiết lập API Key
        public void SetApiKey(string apiKey, string headerName = "X-API-Key")
        {
            AddDefaultHeader(headerName, apiKey);
        }

        // GET Request
        public async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null)
        {
            var url = BuildUrl(endpoint, parameters);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API call failed: {response.StatusCode} - {content}");
            }

            return JsonConvert.DeserializeObject<T>(content);
        }

        // GET Request trả về string
        public async Task<string> GetAsync(string endpoint, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null)
        {
            var url = BuildUrl(endpoint, parameters);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API call failed: {response.StatusCode} - {content}");
            }

            return content;
        }

        // POST Request với JSON body
        public async Task<T> PostAsync<T>(string endpoint, object data = null, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BuildUrl(endpoint));

            if (data != null)
            {
                var json = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API call failed: {response.StatusCode} - {content}");
            }

            return JsonConvert.DeserializeObject<T>(content);
        }

        // POST Request trả về string
        public async Task<string> PostAsync(string endpoint, object data = null, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BuildUrl(endpoint));

            if (data != null)
            {
                var json = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API call failed: {response.StatusCode} - {content}");
            }

            return content;
        }

        // POST Request với form data
        public async Task<T> PostFormAsync<T>(string endpoint, Dictionary<string, string> formData, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BuildUrl(endpoint));
            request.Content = new FormUrlEncodedContent(formData);

            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API call failed: {response.StatusCode} - {content}");
            }

            return JsonConvert.DeserializeObject<T>(content);
        }

        // ===== HÀM HỖ TRỢ XÂY DỰNG URL VỚI QUERY PARAMETERS =====
        private string BuildUrl(string endpoint, Dictionary<string, string> parameters = null)
        {
            // Kết hợp base URL với endpoint
            var url = $"{_baseUrl}/{endpoint.TrimStart('/')}";

            // Thêm query parameters nếu có
            if (parameters != null && parameters.Count > 0)
            {
                var queryString = string.Join("&",
                    Array.ConvertAll(parameters.Keys.ToArray(),
                    key => $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(parameters[key])}"));
                url += $"?{queryString}";
            }

            return url;
        }


        // Hàm hỗ trợ thêm headers vào request
        private void AddHeaders(HttpRequestMessage request, Dictionary<string, string> headers = null)
        {
            // Thêm default headers
            foreach (var header in _defaultHeaders)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            // Thêm headers riêng cho request này
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

}
