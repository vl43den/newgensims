using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace WebApp.Services
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UserApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetUserDataAsync()
        {
            var apiUrl = _configuration["UserApiSettings:BaseUrl"] + "/api/users";
            var token = _configuration["UserApiSettings:JwtToken"];
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
