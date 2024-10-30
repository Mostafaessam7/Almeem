namespace Core.Entities;

public class Product : BaseEntity
{
    public required string NameEn { get; set; }
    public required string NameAr { get; set; }
    public required string DescriptionEn { get; set; }
    public required string DescriptionAr { get; set; }
    public decimal Price { get; set; }
    public bool IsNewArrival { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<ProductImage> Images { get; set; } = new();
    public List<ProductVariant> Variants { get; set; } = new();
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int TotalQuantityInStock => Variants.Sum(v => v.QuantityInStock);
}

public class Category : BaseEntity
{
    public required string NameEn { get; set; }
    public required string NameAr { get; set; }
    public List<Product> Products { get; set; } = new();
}

public class ProductImage : BaseEntity
{
    public required string Url { get; set; }
    public bool IsMain { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}

public class ProductVariant : BaseEntity
{
    public required string Size { get; set; }
    public required string Color { get; set; }
    public int QuantityInStock { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}

public enum CategoryType
{
    Blouse = 1,
    Cardigan = 2,
    Dresses = 3,
    Skirts = 4,
    Sale = 5  // Added for future use
}