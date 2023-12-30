using ECommerce.Application.Abstractions;
using ECommerce.Application.Repositories.Products;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

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
            await _productWriteRepository.AddAsync(new() { Name = "C Product", Price = 1.500F, Stock = 10, CreatedData = DateTime.UtcNow });
            await _productWriteRepository.SaveAsync();
            //*Tracking Test Done
            //Product p = await _productReadRepository.GetByIdAsync("", true);
            //p.Name = "Person";
            //await _productWriteRepository.SaveAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
