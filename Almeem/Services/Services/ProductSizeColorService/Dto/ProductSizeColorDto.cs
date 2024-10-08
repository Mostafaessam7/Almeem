namespace Services.Services.ProductSizeColorService.Dto
{
    public class ProductSizeColorDto
    {
        public required string Size { get; set; }
        public required string Color { get; set; }
        public string ProductName { get; set; }
        public int StockQuantity { get; set; }

    }
}
