namespace Core.Entities
{
    public class PaymentInfo : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
    }

    public enum PaymentMethod
    {
        CreditCard,
        CashOnDelivery
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed,
        Refunded
    }
}
