using ECommerce.Application.Abstractions;
using ECommerce.Application.Repositories.Products;
using ECommerce.Domain.Entities;
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
        public async Task GetProducts()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new() {Id = Guid.NewGuid(),Name="Product 1",Price = 100,CreatedData = DateTime.UtcNow,Stock=10},
                new() {Id = Guid.NewGuid(),Name="Product 2",Price = 200,CreatedData = DateTime.UtcNow,Stock=20},
                new() {Id = Guid.NewGuid(),Name="Product 3",Price = 300,CreatedData = DateTime.UtcNow,Stock=30},
            });
            var count = await _productWriteRepository.SaveAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetById(id);
            return Ok(product);
        }
    }
}
