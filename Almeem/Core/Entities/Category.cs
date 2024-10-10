namespace Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public Sale Sale { get; set; }
    }
}