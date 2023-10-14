using ECommerce.Application.Abstractions;
using ECommerce.Application.Repositories.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async void GetProducts()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new() {Id = Guid.NewGuid(),Name="Product 1",Price = 100,CreatedData = DateTime.UtcNow,Stock=10},
                new() {Id = Guid.NewGuid(),Name="Product 2",Price = 200,CreatedData = DateTime.UtcNow,Stock=20},
                new() {Id = Guid.NewGuid(),Name="Product 3",Price = 300,CreatedData = DateTime.UtcNow,Stock=30},
            });
            await _productWriteRepository.SaveAsync();
        }
    }
}
