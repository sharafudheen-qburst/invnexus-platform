using InvNexus.InventoryService.Domain.Entities;

namespace InvNexus.InventoryService.Application.Interfaces;

public interface IStockRepository
{
    Task AddAsync(Stock stock, CancellationToken cancellationToken);
    Task<Stock?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken);
    void Update(Stock stock);
}
