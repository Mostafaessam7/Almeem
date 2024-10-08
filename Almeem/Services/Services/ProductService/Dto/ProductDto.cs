using Services.Services.ProductSizeColorService.Dto;

namespace Services.Services.ProductService.Dto
{
    public class ProductDto
    {
        public required string NameInEnglish { get; set; }
        public required string NameInArabic { get; set; }
        public required string DescriptionInEnglish { get; set; }
        public required string DescriptionInArabic { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsNewArrival { get; set; }
        public required List<string> ImagesUrl { get; set; }
        public required string CategoryName { get; set; }
        public required List<ProductSizeColorDto> ProductSizeColorDto { get; set; }
    }
}
