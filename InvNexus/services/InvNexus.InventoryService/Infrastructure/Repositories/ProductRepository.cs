using InvNexus.InventoryService.Application.Interfaces;
using InvNexus.InventoryService.Domain.Entities;
using InvNexus.InventoryService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvNexus.InventoryService.Infrastructure.Repositories;

public class ProductRepository(InventoryDbContext dbContext) : IProductRepository
{
    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await dbContext.Products.AddAsync(product, cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetAllWithStockAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Include(product => product.Stock)
            .AsNoTracking()
            .OrderBy(product => product.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdWithStockAsync(Guid productId, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Include(product => product.Stock)
            .AsNoTracking()
            .SingleOrDefaultAsync(product => product.Id == productId, cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .SingleOrDefaultAsync(product => product.Id == productId, cancellationToken);
    }

    public void Update(Product product)
    {
        dbContext.Products.Update(product);
    }

    public void Remove(Product product)
    {
        dbContext.Products.Remove(product);
    }
}
