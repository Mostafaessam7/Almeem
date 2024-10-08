using AutoMapper;
using Core.Entities;

namespace Services.Services.SizeService.Dto
{
    public class SizeProfile : Profile
    {
        public SizeProfile()
        {
            CreateMap<ProductSize, SizeDto>().ReverseMap();
        }
    }
}
