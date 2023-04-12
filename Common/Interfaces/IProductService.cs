using Billify.API.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billify.API.Common.Interfaces
{
    public interface IProductService
    {
        Task<int> CreateProductAsync(ProductDto new_prd);
        Task<int> UpdateProductAsync(ProductDto update_prd);
        Task DeleteProductAsync(int id);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<ProductDto>> GetAllProductsAsync();
    }
}
