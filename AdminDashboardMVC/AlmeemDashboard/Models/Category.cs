namespace AlmeemDashboard.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new();
    }
}
