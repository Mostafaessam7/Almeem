using AutoMapper;
using Core.Entities;

namespace Services.Services.CategoryService.Dto
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
