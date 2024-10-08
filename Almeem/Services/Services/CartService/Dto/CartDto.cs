using Core.Entities;

namespace Services.Services.CartService.Dto
{
    public class CartDto
    {
        public decimal ShippingPrice { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
