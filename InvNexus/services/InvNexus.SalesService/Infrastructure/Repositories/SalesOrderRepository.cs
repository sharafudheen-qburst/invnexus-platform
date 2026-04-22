using InvNexus.SalesService.Application.Interfaces;
using InvNexus.SalesService.Domain.Entities;
using InvNexus.SalesService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvNexus.SalesService.Infrastructure.Repositories;

public class SalesOrderRepository(SalesDbContext dbContext) : ISalesOrderRepository
{
    public async Task AddAsync(SalesOrder salesOrder, CancellationToken cancellationToken)
    {
        await dbContext.SalesOrders.AddAsync(salesOrder, cancellationToken);
    }

    public async Task<IReadOnlyList<SalesOrder>> GetAllWithItemsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SalesOrders
            .Include(salesOrder => salesOrder.Items)
            .AsNoTracking()
            .OrderByDescending(salesOrder => salesOrder.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<SalesOrder?> GetByIdWithItemsAsync(Guid salesOrderId, CancellationToken cancellationToken)
    {
        return await dbContext.SalesOrders
            .Include(salesOrder => salesOrder.Items)
            .AsNoTracking()
            .SingleOrDefaultAsync(salesOrder => salesOrder.Id == salesOrderId, cancellationToken);
    }

    public async Task<SalesOrder?> GetByIdAsync(Guid salesOrderId, CancellationToken cancellationToken)
    {
        return await dbContext.SalesOrders
            .SingleOrDefaultAsync(salesOrder => salesOrder.Id == salesOrderId, cancellationToken);
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SalesOrders.CountAsync(cancellationToken);
    }

    public void Update(SalesOrder salesOrder)
    {
        dbContext.SalesOrders.Update(salesOrder);
    }
}
