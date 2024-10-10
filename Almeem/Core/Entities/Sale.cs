namespace Core.Entities
{
    public class Sale : BaseEntity
    {
        public string Name { get; set; }  // e.g., "Summer Sale"
        public decimal DiscountPercentage { get; set; }  // Percentage discount (e.g., 15%)
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsGlobal { get; set; }  // If true, applies to all products

        // Navigation properties for Many-to-Many relationships
        // Change it one-to-many
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}