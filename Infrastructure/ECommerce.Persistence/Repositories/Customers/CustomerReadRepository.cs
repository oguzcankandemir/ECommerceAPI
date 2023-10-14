using ECommerce.Application.Repositories.Customers;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories.Customers
{                                                                 //signature
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
