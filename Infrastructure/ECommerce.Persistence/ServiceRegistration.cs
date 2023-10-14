using ECommerce.Application.Abstractions;
using ECommerce.Application.Repositories.Customers;
using ECommerce.Application.Repositories.Orders;
using ECommerce.Application.Repositories.Products;
using ECommerce.Persistence.Concretes;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.Repositories.Customers;
using ECommerce.Persistence.Repositories.Orders;
using ECommerce.Persistence.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString),ServiceLifetime.Singleton);
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
