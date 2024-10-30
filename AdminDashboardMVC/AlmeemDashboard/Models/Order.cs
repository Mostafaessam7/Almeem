namespace AlmeemDashboard.Models
{
    public class Order
    {

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string BuyerEmail { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public PaymentSummary PaymentSummary { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; }
    }
    }
