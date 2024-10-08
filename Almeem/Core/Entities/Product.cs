using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public required string NameInEnglish { get; set; }
        public required string NameInArabic { get; set; }
        public required string DescriptionInEnglish { get; set; }
        public required string DescriptionInArabic { get; set; }
        public decimal Price { get; set; }
        public int TotalQuantity { get; set; }
        public bool IsActive { get; set; }
        public bool IsNewArrival { get; set; }
        public required List<string> ImagesUrl { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public required Category Category { get; set; }
        public required List<ProductSizeColor> ProductSizeColors { get; set; }
    }
}
