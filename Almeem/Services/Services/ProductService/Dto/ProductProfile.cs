using AutoMapper;
using Core.Entities;

namespace Services.Services.ProductService.Dto
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ProductSizeColorDto, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.ProductSizeColors, opt => opt.Ignore());
        }
    }
}
