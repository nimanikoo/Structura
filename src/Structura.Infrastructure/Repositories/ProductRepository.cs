using Microsoft.EntityFrameworkCore;
using Structura.Application.Interfaces;
using Structura.Domain.Entities;
using Structura.Infrastructure.DbContexts;

namespace Structura.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;
    public ProductRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _db.Products.AddAsync(product, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _db.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
}