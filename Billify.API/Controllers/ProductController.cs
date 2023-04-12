using Billify.API.Common.Dtos;
using Billify.API.Common.Interfaces;
using Billify.API.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

namespace Billify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin+","+ UserRoles.Supervisor)]
    public class ProductController : ControllerBase
    {
        private IProductService ProductService { get; }
        private ILogger<ProductController> Logger { get; }

        public ProductController(IProductService productService,
            ILogger<ProductController> logger)
        {
            ProductService = productService;
            Logger = logger;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateProduct(ProductDto ProductCreate)
        {
            var id = await ProductService.CreateProductAsync(ProductCreate);
            return Ok(id);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateProduct(ProductDto ProductUpdate)
        {
            await ProductService.UpdateProductAsync(ProductUpdate);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            await ProductService.DeleteProductAsync(Id);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            using (LogContext.PushProperty("Product Id", id))
            {
                var Product = await ProductService.GetProductByIdAsync(id);
                return Ok(Product);
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetProducts()
        {
            var Products = await ProductService.GetAllProductsAsync();
            return Ok(Products);
        }
    }
}
