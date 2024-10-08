using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class ProductSizeColor : BaseEntity
    {
        [ForeignKey(nameof(ProductSize))]
        public int ProductSizeId { get; set; }
        public ProductSize ProductSize { get; set; }


        [ForeignKey(nameof(ProductColor))]
        public int ProductColorId { get; set; }
        public ProductColor ProductColor { get; set; }


        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int StockQuantity { get; set; }
    }
}