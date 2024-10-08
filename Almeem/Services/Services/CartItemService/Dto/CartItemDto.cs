using Core.Entities;
using Services.Services.ProductSizeColorService.Dto;

namespace Services.Services.CartItemService.Dto
{
    public class CartItemDto
    {
        public ProductSizeColorDto ProductSizeColorDto { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
