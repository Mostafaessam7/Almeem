namespace Core.Entities
{
    public class Discount : BaseEntity
    {
        public string Code { get; set; } // Discount or coupon code
        public decimal DiscountAmount { get; set; } // Fixed amount or percentage
        public bool IsPercentage { get; set; } // True if percentage, false if fixed amount
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }

        // Navigation properties
        public List<Order> Orders { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}