using System.ComponentModel.DataAnnotations;

namespace AlmeemDashboard.Models
{
    public class CreateProduct
    {

        [Required]
        public string NameEn { get; set; } = string.Empty;

        [Required]
        public string NameAr { get; set; } = string.Empty;

        [Required]
        public string DescriptionEn { get; set; } = string.Empty;

        [Required]
        public string DescriptionAr { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public bool IsNewArrival { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public int CategoryId { get; set; }

        public List<ProductImage> Images { get; set; } = new();

        public List<ProductVariant> Variants { get; set; } = new();


        public List<IFormFile> ImagesForm { get; set; } = new List<IFormFile>();
        public int MainImageIndex { get; set; }
    }
}
