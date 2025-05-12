using Newtonsoft.Json;
using PracticeApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
//using System.Text.Json;
using System.Threading.Tasks;

namespace PracticeApp.Services
{

    public class UnsafeHttpClientHandler : HttpClientHandler
    {
        public UnsafeHttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
        }
    }
    public class UserService
    {
        private readonly HttpClient _httpClient;
        public UserService()
        {
            _httpClient = new HttpClient(new UnsafeHttpClientHandler())
            {
                BaseAddress = new System.Uri("https://10.0.2.2:7040/")
            };
        }

        public async Task AddUser(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/user/register", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task<User> LoginUser(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/user/login", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(responseContent);
        }
    }
}
