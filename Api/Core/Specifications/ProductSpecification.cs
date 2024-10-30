using Core.Entities;

namespace Core.Specifications;



public class ProductsWithCategorySpecification : BaseSpecification<Product>
{
    public ProductsWithCategorySpecification(ProductSpecParams productParams)
        : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.NameEn.ToLower().Contains(productParams.Search) || x.NameAr.ToLower().Contains(productParams.Search)) &&
            (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId) &&
            (!productParams.CategoryNames.Any() || productParams.CategoryNames.Contains(x.Category.NameEn) || productParams.CategoryNames.Contains(x.Category.NameAr)) &&
            (!productParams.IsNewArrival.HasValue || x.IsNewArrival == productParams.IsNewArrival) &&
            (!productParams.IsActive.HasValue || x.IsActive == productParams.IsActive)
        )
    {
        AddInclude(x => x.Category);
        AddInclude(x => x.Images);
        AddInclude(x => x.Variants);

        ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(n => n.NameEn);
                    break;
            }
        }
        else
        {
            AddOrderBy(n => n.NameEn);
        }
    }
}
public class ProductWithDetailsSpecification : BaseSpecification<Product>
{
    public ProductWithDetailsSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Category);
        AddInclude(x => x.Images);
        AddInclude(x => x.Variants);
    }
}
