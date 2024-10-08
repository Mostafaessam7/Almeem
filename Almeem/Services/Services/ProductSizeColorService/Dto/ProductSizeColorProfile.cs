using AutoMapper;
using Core.Entities;

namespace Services.Services.ProductSizeColorService.Dto
{
    public class ProductSizeColorProfile : Profile
    {
        public ProductSizeColorProfile()
        {
            CreateMap<ProductSizeColor, ProductSizeColorDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.NameInEnglish))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.ProductSize.Size))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.ProductColor.Name));
        }
    }
}
