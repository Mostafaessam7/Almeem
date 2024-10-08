namespace Core.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<Order> Orders { get; set; }

        // Added for multiple addresses
        public List<Address> Addresses { get; set; }

        // Navigation property for Cart
        public Cart Cart { get; set; }
    }
}