using ECommerce.Application.Abstractions;
using ECommerce.Application.Repositories.Customers;
using ECommerce.Application.Repositories.Orders;
using ECommerce.Application.Repositories.Products;
using ECommerce.Application.RequestParameters;
using ECommerce.Application.ViewsModels.Products;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            //wwwroot/resource/product-images
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            Random r = new();
            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath, $"{r.Next()}{Path.GetExtension(file.FileName)}");

                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            return Ok();
        }
        //[HttpGet]
        //public async Task GetProducts()
        //{
        //    await _productWriteRepository.AddAsync(new() { Name = "C Product", Price = 1.500F, Stock = 10, CreatedData = DateTime.UtcNow });
        //    await _productWriteRepository.SaveAsync();
        //    //*Tracking Test Done
        //    //Product p = await _productReadRepository.GetByIdAsync("", true);
        //    //p.Name = "Person";
        //    //await _productWriteRepository.SaveAsync();
        //}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    Product product = await _productReadRepository.GetByIdAsync(id);
        //    return Ok(product);
        //}

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
