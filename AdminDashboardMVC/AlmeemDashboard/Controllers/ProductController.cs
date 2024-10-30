using AlmeemDashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlmeemDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(HttpClient httpClient, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7051/api/");    
            _webHostEnvironment = webHostEnvironment;

        }

        // GET: Product/GetProducts
        public async Task<ActionResult<Pagination<Product>>> GetProducts()
        {
            var response = await _httpClient.GetAsync($"Products");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Pagination<Product>>();
                return View(data);
            }
            return View(new Pagination<Product>());
        }


        // GET: Product/GetProduct
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var response = await _httpClient.GetAsync($"Products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Product>();
                return View(data);
            }
            return NotFound();
        }


        // GET: Product/Create
        public async Task<IActionResult> CreateProduct()
        {
            // Fetch categories for the dropdown
            var response = await _httpClient.GetAsync("Products/categories");
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<IReadOnlyList<Category>>();
                ViewBag.Categories = categories;

            }
            return View(new CreateProduct());
        }


        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProduct model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await FetchCategories();
                return View(model);
            }

            var productToCreate = new Product
            {
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                DescriptionEn = model.DescriptionEn,
                DescriptionAr = model.DescriptionAr,
                Price = model.Price,
                IsNewArrival = model.IsNewArrival,
                IsActive = model.IsActive,
                CategoryId = model.CategoryId,
                Variants = model.Variants.Select(v => new ProductVariant
                {
                    Size = v.Size,
                    Color = v.Color,
                    QuantityInStock = v.QuantityInStock
                }).ToList(),
                Images = new List<ProductImage>()
            };

            if (model.ImagesForm != null && model.ImagesForm.Any())
            {
                for (int i = 0; i < model.ImagesForm.Count; i++)
                {
                    var file = model.ImagesForm[i];

                    if (file != null && file.Length > 0)
                    {
                        var imageUrl = await UploadImage(file);
                        productToCreate.Images.Add(new ProductImage
                        {
                            Url = imageUrl,
                            IsMain = (i == model.MainImageIndex)
                        });
                    }
                }
            }

            var response = await _httpClient.PostAsJsonAsync("Products", productToCreate);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetProducts", "Product");
            }

            ModelState.AddModelError("", "Failed to create the product. Please try again.");
            ViewBag.Categories = await FetchCategories();
            return View(model);
        }

        // GET: Product/Update/{id}
        public async Task<IActionResult> UpdateProduct(int id)
        {
            // Fetch product details for update
            var response = await _httpClient.GetAsync($"Products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<Product>();
                var updateModel = new Product
                {
                    Id = product.Id,
                    NameEn = product.NameEn,
                    NameAr = product.NameAr,
                    DescriptionEn = product.DescriptionEn,
                    DescriptionAr = product.DescriptionAr,
                    Price = product.Price,
                    IsNewArrival = product.IsNewArrival,
                    IsActive = product.IsActive,
                    CategoryId = product.CategoryId,
                    Variants = product.Variants.ToList(),
                    Images = product.Images.ToList()
                };

                ViewBag.Categories = await FetchCategories();
                return View(updateModel);
            }

            return NotFound();
        }

        // POST: Product/Update/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(int id, Product model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await FetchCategories();
                return View(model);
            }

            var productToUpdate = new Product
            {
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                DescriptionEn = model.DescriptionEn,
                DescriptionAr = model.DescriptionAr,
                Price = model.Price,
                IsNewArrival = model.IsNewArrival,
                IsActive = model.IsActive,
                CategoryId = model.CategoryId,
                Variants = model.Variants.Select(v => new ProductVariant
                {
                    Size = v.Size,
                    Color = v.Color,
                    QuantityInStock = v.QuantityInStock
                }).ToList(),
                Images = model.Images
            };

            if (model.ImagesForm != null && model.ImagesForm.Any())
            {
                for (int i = 0; i < model.ImagesForm.Count; i++)
                {
                    var file = model.ImagesForm[i];

                    if (file != null && file.Length > 0)
                    {
                        var imageUrl = await UploadImage(file);
                        productToUpdate.Images.Add(new ProductImage
                        {
                            Url = imageUrl,
                            IsMain = (i == model.MainImageIndex)
                        });
                    }
                }
            }

            // Handle existing images
            for (int i = 0; i < productToUpdate.Images.Count; i++)
            {
                productToUpdate.Images[i].IsMain = (i == model.MainImageIndex);
            }

            var response = await _httpClient.PutAsJsonAsync($"Products/{id}", productToUpdate);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetProducts", "Product");
            }

            ModelState.AddModelError("", "Failed to update the product. Please try again.");
            ViewBag.Categories = await FetchCategories();
            return View(model);
        }

        private async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, @"images");
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            var fileName = $"{file.FileName}";
            var filePath = Path.Combine(imageDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the full URL from the API, instead of a relative path
            //var baseUrl = "https://localhost:7051";
            return $"/images/{fileName}";
        }





        private async Task<IReadOnlyList<Category>> FetchCategories()
        {
            var response = await _httpClient.GetAsync("Products/categories");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IReadOnlyList<Category>>();
            }
            return new List<Category>();
        }
    }
}
