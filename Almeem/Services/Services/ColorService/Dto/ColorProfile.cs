using AutoMapper;
using Core.Entities;

namespace Services.Services.ColorService.Dto
{
    public class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<ProductColor, ColorDto>().ReverseMap();
        }
    }
}
