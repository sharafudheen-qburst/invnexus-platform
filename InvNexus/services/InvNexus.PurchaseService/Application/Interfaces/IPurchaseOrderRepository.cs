using InvNexus.PurchaseService.Domain.Entities;

namespace InvNexus.PurchaseService.Application.Interfaces;

public interface IPurchaseOrderRepository
{
    Task AddAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken);
    Task<IReadOnlyList<PurchaseOrder>> GetAllWithItemsAsync(CancellationToken cancellationToken);
    Task<PurchaseOrder?> GetByIdWithItemsAsync(Guid purchaseOrderId, CancellationToken cancellationToken);
    Task<PurchaseOrder?> GetByIdAsync(Guid purchaseOrderId, CancellationToken cancellationToken);
    Task<int> GetCountAsync(CancellationToken cancellationToken);
    void Update(PurchaseOrder purchaseOrder);
}
