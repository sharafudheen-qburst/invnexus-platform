using InvNexus.SalesService.Application.DTOs;
using InvNexus.SalesService.Application.Interfaces;
using InvNexus.SalesService.Application.Mediator;

namespace InvNexus.SalesService.Application.Queries.GetSalesOrders;

public class GetSalesOrdersQueryHandler(ISalesOrderRepository salesOrderRepository)
    : IQueryHandler<GetSalesOrdersQuery, IReadOnlyList<SalesOrderListItemResponseDto>>
{
    public async Task<IReadOnlyList<SalesOrderListItemResponseDto>> HandleAsync(GetSalesOrdersQuery query, CancellationToken cancellationToken)
    {
        var salesOrders = await salesOrderRepository.GetAllWithItemsAsync(cancellationToken);

        return salesOrders
            .Select(order => new SalesOrderListItemResponseDto
            {
                Id = order.Id,
                SalesNumber = order.SalesNumber,
                Status = order.Status,
                CreatedAt = order.CreatedAt
            })
            .ToList();
    }
}
