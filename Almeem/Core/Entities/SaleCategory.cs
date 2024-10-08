namespace Core.Entities
{
    public class SaleCategory : BaseEntity
    {
        public int SaleId { get; set; }
        public Sale Sale { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}