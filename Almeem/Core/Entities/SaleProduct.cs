namespace Core.Entities
{
    public class SaleProduct : BaseEntity
    {
        public int SaleId { get; set; }
        public Sale Sale { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}