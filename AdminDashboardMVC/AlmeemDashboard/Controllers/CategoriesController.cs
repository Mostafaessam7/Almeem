using AlmeemDashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlmeemDashboard.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7051/api/");
        }

        // GET: Categories/Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Categories");

            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<IReadOnlyList<Category>>();
                return View(categories);
            }
            return View(new List<Category>());
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var response = await _httpClient.PostAsJsonAsync("Categories", category);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Failed to create the category. Please try again.");
            return View(category);
        }

        // GET: Categories/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                var category = await response.Content.ReadFromJsonAsync<Category>();
                return View(category);
            }

            return NotFound();
        }

        // POST: Categories/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var response = await _httpClient.PutAsJsonAsync($"Categories/{id}", category);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Failed to update the category. Please try again.");
            return View(category);
        }

        // POST: Categories/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"Categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Failed to delete the category. Please try again.");
            return RedirectToAction(nameof(Index)); // Return to Index with a failure message.
        }


    }
}
