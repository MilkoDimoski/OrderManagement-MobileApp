

using Newtonsoft.Json;
using PracticeApp.Services.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PracticeApp.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService()
        {
            _httpClient = new HttpClient(new UnsafeHttpClientHandler())
            {
                BaseAddress = new System.Uri("https://10.0.2.2:7040/")
            };
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByOrderNumber(int orderNumber)
        {
            var response = await _httpClient.GetAsync($"api/order/details/order/{orderNumber}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<OrderDetail>>(json);
        }
        public async Task<Order> GetOrderByOrderNumber(int orderNumber)
        {
            var response = await _httpClient.GetAsync($"api/order/number/{orderNumber}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Order>(json);
        }

        public async Task UpdateOrderDetail(OrderDetail orderDetail)
        {
            var json = JsonConvert.SerializeObject(orderDetail);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/order/details/{orderDetail.OrderDetailId}", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
