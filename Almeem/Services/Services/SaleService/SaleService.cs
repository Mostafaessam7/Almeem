using AutoMapper;
using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using Services.Services.CategoryService;
using Services.Services.CategoryService.Dto;
using System.Collections.Generic;


namespace Services.Services.SaleService
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }


        // Get All
        public async Task<IReadOnlyList<SaleDto>> GetAllAsync()
        {
            var sales = await _saleRepository.GetAsync();
            return _mapper.Map<IReadOnlyList<SaleDto>>(sales);
        }


        // Get Sale by Id
        public async Task<SaleDto?> GetByIdAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            return sale != null ? _mapper.Map<SaleDto>(sale) : null;
        }


        // Get all active sales
        public async Task<IReadOnlyList<SaleDto>> GetActiveSalesAsync()
        {
            var sales = await _saleRepository.GetActiveSalesAsync();
            return _mapper.Map<IReadOnlyList<SaleDto>>(sales);
        }

        // Create a new sale
        public async Task<SaleDto> CreateAsync(SaleDto saleDto)
        {
            var sale = _mapper.Map<Sale>(saleDto);
            sale.Id = 0;
            _saleRepository.Add(sale);
            await _saleRepository.SaveChangesAsync();
            return _mapper.Map<SaleDto>(sale);
        }

        // Update an existing sale
        public async Task<bool> UpdateAsync(SaleDto saleDto)
        {
            var sale = await _saleRepository.GetByIdAsync(saleDto.Id);
            if (sale == null) return false;

            _mapper.Map(saleDto, sale);
            _saleRepository.Update(sale);
            return await _saleRepository.SaveChangesAsync();
        }

        // Delete a sale by Id
        public async Task<bool> DeleteAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null) return false;

            _saleRepository.Delete(sale);
            return await _saleRepository.SaveChangesAsync();
        }



        //public async Task<IReadOnlyList<SaleDto>> GetSalesByCategoryAsync(int categoryId)
        //{
        //    var sales = await _saleRepository.GetSalesByCategoryAsync(categoryId);
        //    return _mapper.Map<IReadOnlyList<SaleDto>>(sales);
        //}

        //public async Task<IReadOnlyList<SaleDto>> GetSalesByProductAsync(int productId)
        //{
        //    var sales = await _saleRepository.GetSalesByProductAsync(productId);
        //    return _mapper.Map<IReadOnlyList<SaleDto>>(sales);
        //}

    }
}
