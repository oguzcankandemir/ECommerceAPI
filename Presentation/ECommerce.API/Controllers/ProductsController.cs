using ECommerce.Application.Abstractions;
using ECommerce.Application.Repositories.Customers;
using ECommerce.Application.Repositories.Orders;
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
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IOrderReadRepository _orderReadRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderReadRepository orderReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderReadRepository = orderReadRepository;
            _customerWriteRepository = customerWriteRepository;
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

        //SaveChangeAsync Interceptor
        //[HttpGet]
        //public async Task GetBank()
        //{
        //    var customerId = Guid.NewGuid();
        //    await _customerWriteRepository.AddAsync(new() { Id = customerId, Name = "bla" });
        //    await _orderReadRepository.AddAsync(new() { Description = "C Product", Address = "İstanbul", CustomerId = customerId });
        //    await _orderReadRepository.AddAsync(new() { Description = "d Product", Address = "Kırklareli", CustomerId = customerId });
        //    await _orderReadRepository.SaveAsync();
        //}
    }
}
