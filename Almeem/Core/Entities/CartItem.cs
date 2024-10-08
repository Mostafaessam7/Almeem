namespace Core.Entities
{
    public class CartItem : BaseEntity
    {
        //public int CartId { get; set; }
        //public Cart Cart { get; set; }
        public int ProductSizeColorId { get; set; }
        public ProductSizeColor ProductSizeColor { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Current price for display
    }
}