using Microsoft.EntityFrameworkCore;
using ProductService.Application.Abstractions.Persistence;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Repositories
{
    public sealed class ProductRepository
        : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AnyAsync(
                    x => x.Name == name,
                    cancellationToken);
        }

        public async Task AddAsync(
            Product product,
            CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(
                product,
                cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);
        }

        public async Task UpdateAsync(
            Product product,
            CancellationToken cancellationToken = default)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync(
                cancellationToken);
        }

        public async Task DeleteAsync(
            Product product,
            CancellationToken cancellationToken = default)
        {
            _context.Products.Remove(product);

            await _context.SaveChangesAsync(
                cancellationToken);
        }
    }
}
