namespace Core.Entities
{
    public class Cart
    {
        //public int? UserId { get; set; }  // Nullable for guest users
        //public User User { get; set; }
        public string Id { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}