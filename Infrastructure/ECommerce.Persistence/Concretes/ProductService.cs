using ECommerce.Application.Abstractions;
using ECommerce.Application.Repositories.Products;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IQRCodeService _qrCodeService;
        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qrCodeService)
        {
            _productReadRepository = productReadRepository;
            _qrCodeService = qrCodeService;
        }
        public List<Product> GetProducts()
      => new() 
      {
       new(){Id=Guid.NewGuid(),Name = "Product 1",Price = 100, Stock=150},
       new(){Id=Guid.NewGuid(),Name = "Product 2",Price = 200, Stock=150},
       new(){Id=Guid.NewGuid(),Name = "Product 3",Price = 300, Stock=150},
       new(){Id=Guid.NewGuid(),Name = "Product 4",Price = 400, Stock=150},
       new(){Id=Guid.NewGuid(),Name = "Product 5",Price = 500, Stock=150},
       new(){Id=Guid.NewGuid(),Name = "Product 6",Price = 600, Stock=150}
      };

        public async Task<byte[]> QrCodeToProductAsync(string productId)
        {
            Product product = await _productReadRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Product not found");

            var plainObject = new
            {
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.CreatedDate
            };
            string plainText = JsonSerializer.Serialize(plainObject);

            return _qrCodeService.GenerateQRCode(plainText);
        }
    }
}
