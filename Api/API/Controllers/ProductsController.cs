using API.DTOs;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class ProductsController : BaseApiController
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    {
        var spec = new ProductsWithCategorySpecification(productParams);
        var countSpec = new ProductsWithCategorySpecification(productParams);

        var totalItems = await _productRepository.CountAsync(countSpec);
        var products = await _productRepository.ListAsync(spec);

        var data = products.Select(MapProductToDto).ToList();

        return Ok(new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var spec = new ProductWithDetailsSpecification(id);
        var product = await _productRepository.GetEntityWithSpec(spec);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(MapProductToDto(product));
    }
    //[Authorize(Roles ="Admin")]
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        var product = MapCreateDtoToProduct(createProductDto);

        _productRepository.Add(product);
        var result = await _productRepository.SaveAllAsync();

        if (result)
        {
            var createdProductDto = MapProductToDto(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, createdProductDto);
        }

        return BadRequest("Failed to create product");
    }
  //[Authorize(Roles = "Admin")]
  [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> UpdateProduct(int id, [FromBody] CreateProductDto updateProductDto)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        UpdateProductFromDto(product, updateProductDto);

        _productRepository.Update(product);
        var result = await _productRepository.SaveAllAsync();

        if (result)
        {
            var updatedProductDto = MapProductToDto(product);
            return Ok(updatedProductDto);
        }

        return BadRequest("Failed to update product");
    }

  //[Authorize(Roles = "Admin")]

  [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        _productRepository.Remove(product);
        var result = await _productRepository.SaveAllAsync();

        if (result)
        {
            return NoContent();
        }

        return BadRequest("Failed to delete product");
    }

    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
    {
        var categories = await _productRepository.GetCategoriesAsync();
        var categoryDtos = categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            NameEn = category.NameEn,
            NameAr = category.NameAr
        }).ToList();
        return Ok(categoryDtos);
    }

    // Manual mapping methods
    private ProductDto MapProductToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            NameEn = product.NameEn,
            NameAr = product.NameAr,
            DescriptionEn = product.DescriptionEn,
            DescriptionAr = product.DescriptionAr,
            Price = product.Price,
            IsNewArrival = product.IsNewArrival,
            IsActive = product.IsActive,
            CreatedAt = product.CreatedAt,
            Images = product.Images.Select(MapProductImageToDto).ToList(),
            Variants = product.Variants.Select(MapProductVariantToDto).ToList(),
            CategoryId = product.CategoryId,
            CategoryNameEn = product.Category?.NameEn ?? string.Empty,
            CategoryNameAr = product.Category?.NameAr ?? string.Empty
        };
    }

    private Product MapCreateDtoToProduct(CreateProductDto createProductDto)
    {
        return new Product
        {
            NameEn = createProductDto.NameEn,
            NameAr = createProductDto.NameAr,
            DescriptionEn = createProductDto.DescriptionEn,
            DescriptionAr = createProductDto.DescriptionAr,
            Price = createProductDto.Price,
            IsNewArrival = createProductDto.IsNewArrival,
            IsActive = createProductDto.IsActive,
            CategoryId = createProductDto.CategoryId,
            Images = createProductDto.Images.Select(MapProductImageDtoToEntity).ToList(),
            Variants = createProductDto.Variants.Select(MapProductVariantDtoToEntity).ToList()
        };
    }

    private void UpdateProductFromDto(Product product, CreateProductDto updateProductDto)
    {
        product.NameEn = updateProductDto.NameEn;
        product.NameAr = updateProductDto.NameAr;
        product.DescriptionEn = updateProductDto.DescriptionEn;
        product.DescriptionAr = updateProductDto.DescriptionAr;
        product.Price = updateProductDto.Price;
        product.IsNewArrival = updateProductDto.IsNewArrival;
        product.IsActive = updateProductDto.IsActive;
        product.CategoryId = updateProductDto.CategoryId;

        // Update images and variants
        product.Images = updateProductDto.Images.Select(MapProductImageDtoToEntity).ToList();
        product.Variants = updateProductDto.Variants.Select(MapProductVariantDtoToEntity).ToList();
    }

    private ProductImageDto MapProductImageToDto(ProductImage image)
    {
        return new ProductImageDto
        {
            Url = image.Url,
            IsMain = image.IsMain
        };
    }

    private ProductImage MapProductImageDtoToEntity(ProductImageDto imageDto)
    {
        return new ProductImage
        {
            Url = imageDto.Url,
            IsMain = imageDto.IsMain
        };
    }

    private ProductVariantDto MapProductVariantToDto(ProductVariant variant)
    {
        return new ProductVariantDto
        {
            Size = variant.Size,
            Color = variant.Color,
            QuantityInStock = variant.QuantityInStock
        };
    }

    private ProductVariant MapProductVariantDtoToEntity(ProductVariantDto variantDto)
    {
        return new ProductVariant
        {
            Size = variantDto.Size,
            Color = variantDto.Color,
            QuantityInStock = variantDto.QuantityInStock
        };
    }

    private CategoryDto MapCategoryToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            NameEn = category.NameEn,
            NameAr = category.NameAr
        };
    }
}

