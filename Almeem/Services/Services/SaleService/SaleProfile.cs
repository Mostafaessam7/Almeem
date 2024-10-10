using AutoMapper;
using Core.Entities;


namespace Services.Services.SaleService
{
    public class SaleProfile : Profile
    {
        public SaleProfile() 
        {
            // Mapping from Sale entity to SaleDto
            CreateMap<Sale, SaleDto>();
                
            // Mapping from SaleDto back to Sale entity if needed
            CreateMap<SaleDto, Sale>();

        }
    }
}
