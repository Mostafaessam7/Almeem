using AlmeemDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace AlmeemDashboard.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7051/api/");
        }

        // GET: Product/GetProducts
        public async Task<ActionResult<Pagination<Order>>> Getorders()
        {
            var response = await _httpClient.GetAsync($"Admin/orders");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Pagination<Order>>();
                return View(data);
            }
            return View(new Pagination<Order>());
        }

        // GET: Product/GetProduct
        public async Task<ActionResult<Order>> Getorder(int id)
        {
            var response = await _httpClient.GetAsync($"Admin/orders/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Order>();
                return View(data);
            }
            return NotFound();
        }


    }
}
