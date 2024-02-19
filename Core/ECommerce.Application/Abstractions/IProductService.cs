using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Task<byte[]> QrCodeToProductAsync(string productId);
    }
}
