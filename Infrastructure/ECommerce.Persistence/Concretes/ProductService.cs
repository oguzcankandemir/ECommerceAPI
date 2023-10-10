using ECommerce.Application.Abstractions;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Concretes
{
    public class ProductService : IProductService
    {
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
    }
}
