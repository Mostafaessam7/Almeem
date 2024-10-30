using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class ProductDto
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
    public List<ProductImageDto> Images { get; set; } = new();
    public List<ProductVariantDto> Variants { get; set; } = new();
    public int CategoryId { get; set; }
    public string CategoryNameEn { get; set; } = string.Empty;
    public string CategoryNameAr { get; set; } = string.Empty;
}

public class CreateProductDto
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

    public List<ProductImageDto> Images { get; set; } = new();

    public List<ProductVariantDto> Variants { get; set; } = new();
}

public class ProductImageDto
{
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; }
}

public class ProductVariantDto
{
    [Required]
    public string Size { get; set; } = string.Empty;

    [Required]
    public string Color { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock must be non-negative")]
    public int QuantityInStock { get; set; }
}

public class CategoryDto
{
    public int Id { get; set; }
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
}
