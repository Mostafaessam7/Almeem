using AlmeemDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AlmeemDashboard.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(HttpClient httpClient, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7051/api/");
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            // Fetch products
            var response = await _httpClient.GetAsync("Products");
            var resp = await _httpClient.GetAsync($"Categories/count");

            if (response.IsSuccessStatusCode && resp.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<Pagination<Product>>();
                var count = await resp.Content.ReadFromJsonAsync<int>();
                TempData["CategoryCount"] = count;
                return View(products);
            }

            return View(new Pagination<Product>());
        }
    }

}
