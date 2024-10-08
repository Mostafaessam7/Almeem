namespace Core.Entities
{
    public class Delivery : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string DeliveryStatus { get; set; } // e.g., "Pending", "Shipped", "Delivered"
        public DateTime? DeliveryDate { get; set; } // Optional date of delivery
        public string TrackingNumber { get; set; } // For tracking delivery
        public string DeliveryService { get; set; } // e.g., "DHL", "FedEx"
    }
}