using InvNexus.InventoryService.Domain.Entities;

namespace InvNexus.InventoryService.Application.Interfaces;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken);
    Task<IReadOnlyList<Product>> GetAllWithStockAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdWithStockAsync(Guid productId, CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
    void Update(Product product);
    void Remove(Product product);
}
