namespace AlmeemDashboard.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsNewArrival { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ProductImage> Images { get; set; } = new();
        public List<ProductVariant> Variants { get; set; } = new();
        public int CategoryId { get; set; }
        public string CategoryNameEn { get; set; } = string.Empty;
        public string CategoryNameAr { get; set; } = string.Empty;

        public List<IFormFile> ImagesForm { get; set; } = new List<IFormFile>();
        public int MainImageIndex { get; set; }
    }
}
