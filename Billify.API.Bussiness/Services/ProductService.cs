using AutoMapper;
using Billify.API.Common.Dtos;
using Billify.API.Common.Interfaces;
using Billify.API.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billify.API.Common.Models;

namespace Billify.API.Bussiness.Services
{
    public class ProductService : IProductService
    {
        private IMapper Mapper { get; }
        private IGenericRepository<Product> ProductRepository { get; }
        public ProductService(IMapper mapper, IGenericRepository<Product> productRepository)
        {
            Mapper=mapper;
            ProductRepository=productRepository;
        }

        public async Task<int> CreateProductAsync(ProductDto new_prd)
        {
            var product_entity=Mapper.Map<Product>(new_prd);
            await ProductRepository.InsertAsync(product_entity);
            await ProductRepository.SaveChangesAsync();
            return product_entity.Id;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product_entity = await ProductRepository.GetByIdAsync(id);
            ProductRepository.Delete(product_entity);
            await ProductRepository.SaveChangesAsync();
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products_entity = await ProductRepository.GetAsync(null, null);
            return Mapper.Map<List<ProductDto>>(products_entity);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product_entity = await ProductRepository.GetByIdAsync(id);
            return Mapper.Map<ProductDto>(product_entity);
        }

        public async Task<int> UpdateProductAsync(ProductDto update_prd)
        {
            var product_entity = Mapper.Map<Product>(update_prd);
            ProductRepository.Update(product_entity);
            await ProductRepository.SaveChangesAsync();
            return product_entity.Id;
        }
    }
}
