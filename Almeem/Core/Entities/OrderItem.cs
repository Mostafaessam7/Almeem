namespace Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductSizeColorId { get; set; }
        public ProductSizeColor ProductSizeColor { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }  // Price at the time of order
                                            // Added for Discounts and Coupons
        public int? DiscountId { get; set; }  // Nullable for items without a discount
        public Discount Discount { get; set; }
    }
}