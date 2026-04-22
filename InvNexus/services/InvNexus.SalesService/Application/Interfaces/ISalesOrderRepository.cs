using InvNexus.SalesService.Domain.Entities;

namespace InvNexus.SalesService.Application.Interfaces;

public interface ISalesOrderRepository
{
    Task AddAsync(SalesOrder salesOrder, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalesOrder>> GetAllWithItemsAsync(CancellationToken cancellationToken);
    Task<SalesOrder?> GetByIdWithItemsAsync(Guid salesOrderId, CancellationToken cancellationToken);
    Task<SalesOrder?> GetByIdAsync(Guid salesOrderId, CancellationToken cancellationToken);
    Task<int> GetCountAsync(CancellationToken cancellationToken);
    void Update(SalesOrder salesOrder);
}
