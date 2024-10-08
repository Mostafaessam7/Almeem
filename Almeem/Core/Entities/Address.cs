namespace Core.Entities
{
    public class Address : BaseEntity
    {
        public string FullName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int? UserId { get; set; }  // Nullable for guest checkout
        public User User { get; set; }
    }
}