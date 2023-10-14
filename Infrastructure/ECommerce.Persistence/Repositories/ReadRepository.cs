using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Common;
using ECommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ECommerceAPIDbContext _context;

        public ReadRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
        => Table;
        public async Task<T> GetById(string id)
          => await Table.FindAsync(Guid.Parse(id));
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
        => await Table.FirstOrDefaultAsync(method);
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        => Table.Where(method);
    }
}
