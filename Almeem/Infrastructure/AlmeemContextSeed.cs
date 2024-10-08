using Core.Context;
using Core.Entities;
using System.Text.Json;

namespace Infrastructure
{
    public class AlmeemContextSeed
    {
        public static async Task SeedAsync(AlmeemContext context)
        {
            try
            {
                if (context.Categories != null && !context.Categories.Any())
                {
                    var categoriesData = await File.ReadAllTextAsync("../Infrastructure/SeedData/Categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                    if (categories == null) return;
                    
                    await context.Categories.AddRangeAsync(categories);

                    await context.SaveChangesAsync();
                }

                if (context.ProductSizes != null && !context.ProductSizes.Any())
                {
                    var productSizesData = await File.ReadAllTextAsync("../Infrastructure/SeedData/Sizes.json");
                    var productSizes = JsonSerializer.Deserialize<List<ProductSize>>(productSizesData);

                    if (productSizes == null) return;
                    
                    await context.ProductSizes.AddRangeAsync(productSizes);

                    await context.SaveChangesAsync();
                }

                if (context.ProductColors != null && !context.ProductColors.Any())
                {
                    var productColorsData = await File.ReadAllTextAsync("../Infrastructure/SeedData/Colors.json");
                    var productColors = JsonSerializer.Deserialize<List<ProductColor>>(productColorsData);

                    if (productColors == null) return;
                    
                    await context.ProductColors.AddRangeAsync(productColors);

                    await context.SaveChangesAsync();
                }

                if (context.Products != null && !context.Products.Any())
                {
                    var productsData = await File.ReadAllTextAsync("../Infrastructure/SeedData/Products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products == null) return;

                    await context.Products.AddRangeAsync(products);

                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
