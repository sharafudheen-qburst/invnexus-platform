using InvNexus.PurchaseService.Application.Interfaces;
using InvNexus.PurchaseService.Domain.Entities;
using InvNexus.PurchaseService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvNexus.PurchaseService.Infrastructure.Repositories;

public class PurchaseOrderRepository(PurchaseDbContext dbContext) : IPurchaseOrderRepository
{
    public async Task AddAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken)
    {
        await dbContext.PurchaseOrders.AddAsync(purchaseOrder, cancellationToken);
    }

    public async Task<IReadOnlyList<PurchaseOrder>> GetAllWithItemsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.PurchaseOrders
            .Include(purchaseOrder => purchaseOrder.Items)
            .AsNoTracking()
            .OrderByDescending(purchaseOrder => purchaseOrder.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<PurchaseOrder?> GetByIdWithItemsAsync(Guid purchaseOrderId, CancellationToken cancellationToken)
    {
        return await dbContext.PurchaseOrders
            .Include(purchaseOrder => purchaseOrder.Items)
            .AsNoTracking()
            .SingleOrDefaultAsync(purchaseOrder => purchaseOrder.Id == purchaseOrderId, cancellationToken);
    }

    public async Task<PurchaseOrder?> GetByIdAsync(Guid purchaseOrderId, CancellationToken cancellationToken)
    {
        return await dbContext.PurchaseOrders
            .SingleOrDefaultAsync(purchaseOrder => purchaseOrder.Id == purchaseOrderId, cancellationToken);
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.PurchaseOrders.CountAsync(cancellationToken);
    }

    public void Update(PurchaseOrder purchaseOrder)
    {
        dbContext.PurchaseOrders.Update(purchaseOrder);
    }
}
