using Microsoft.AspNetCore.Mvc;
using Services.Services.SaleService;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : Controller
    {
          
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<SaleDto?>> GetAll()
        {
            var sale = await _saleService.GetAllAsync();
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto?>> GetSaleById(int id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IReadOnlyList<SaleDto>>> GetActiveSales()
        {
            var activeSales = await _saleService.GetActiveSalesAsync();
            return Ok(activeSales);
        }

        

        // POST: api/sale - Create a new Sale
        [HttpPost]
        public async Task<ActionResult> CreateSale(SaleDto saleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSale = await _saleService.CreateAsync(saleDto);

            return CreatedAtAction(nameof(GetSaleById), new { id = createdSale.Id }, createdSale);
        }

        // PUT: api/sale/{id} - Update an existing Sale
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(int id, SaleDto saleDto)
        {
            if (id != saleDto.Id)
            {
                return BadRequest("Sale ID mismatch");
            }

            var result = await _saleService.UpdateAsync(saleDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();  
        }

        // DELETE: api/sale/{id} - Delete a Sale
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var result = await _saleService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();  
        }



        //[HttpGet("category/{categoryId}")]
        //public async Task<ActionResult<IReadOnlyList<SaleDto>>> GetSalesByCategory(int categoryId)
        //{
        //    var sales = await _saleService.GetSalesByCategoryAsync(categoryId);
        //    return Ok(sales);
        //}

        //[HttpGet("product/{productId}")]
        //public async Task<ActionResult<IReadOnlyList<SaleDto>>> GetSalesByProduct(int productId)
        //{
        //    var sales = await _saleService.GetSalesByProductAsync(productId);
        //    return Ok(sales);
        //}
    }
}
 