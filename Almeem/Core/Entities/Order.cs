namespace Core.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public int? UserId { get; set; }  // Nullable for guest checkout
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string GuestEmail { get; set; }  // For guest checkout
        public string GuestPhoneNumber { get; set; }  // For guest checkout
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
        public PaymentInfo PaymentInfo { get; set; }

        // Added for Discounts and Coupons
        public int? DiscountId { get; set; }  // Nullable for orders without a discount
        public Discount Discount { get; set; }

        // Added for Delivery Tracking
        public Delivery Delivery { get; set; }
    }
}
