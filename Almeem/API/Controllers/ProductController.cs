using Microsoft.AspNetCore.Mvc;
using Services.Services.ProductService;
using Services.Services.ProductService.Dto;

namespace Api.Controllers
{
    public class ProductController(IProductService service) : BaseController
    {
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts()
            => Ok(await service.GetAsync());

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await service.GetByIdAsync(id);

            return product == null ? NotFound() : product;
        }

        [HttpGet("SearchByProductName{input}")]
        public async Task<ActionResult<ProductDto>> SearchByNameAsync(string input)
            => Ok(await service.SearchByName(input));

        [HttpGet("GetNewArrival")]
        public async Task<ActionResult<ProductDto>> GetNewArrival()
            => Ok(await service.GetNewArrival());

        [HttpGet("FilterByCategory")]
        public async Task<ActionResult<ProductDto>> FilterByCategoryAsync(int categoryId)
            => Ok(await service.FilterByCategory(categoryId));

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto product)
        {
            service.Add(product);
            if (await service.SaveChangesAsync())
            {
                return Ok(product);
            }
            return BadRequest("Problem Creating Product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductDto product)
        {
            if (!ProductExists(id))
                return BadRequest("Can not update this product");

            service.Update(id, product);

            if (await service.SaveChangesAsync())
                return NoContent();

            return BadRequest("Problem Updating product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (ProductExists(id))
            {
                service.Delete(id);

                if (await service.SaveChangesAsync())
                    return NoContent();
                return BadRequest("Problem Deleting Product");
            }
            else
                return BadRequest("Id Not Found");
        }

        private bool ProductExists(int id)
            => service.entityExist(id);
    }
}
