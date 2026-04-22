using InvNexus.SalesService.Application.DTOs;
using InvNexus.SalesService.Application.Interfaces;
using InvNexus.SalesService.Application.Mediator;

namespace InvNexus.SalesService.Application.Queries.GetSalesOrderById;

public class GetSalesOrderByIdQueryHandler(ISalesOrderRepository salesOrderRepository)
    : IQueryHandler<GetSalesOrderByIdQuery, SalesOrderDetailResponseDto?>
{
    public async Task<SalesOrderDetailResponseDto?> HandleAsync(GetSalesOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var salesOrder = await salesOrderRepository.GetByIdWithItemsAsync(query.SalesOrderId, cancellationToken);
        if (salesOrder is null)
        {
            return null;
        }

        return new SalesOrderDetailResponseDto
        {
            Id = salesOrder.Id,
            SalesNumber = salesOrder.SalesNumber,
            Status = salesOrder.Status,
            CreatedAt = salesOrder.CreatedAt,
            Items = salesOrder.Items
                .Select(item => new SalesOrderItemResponseDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                })
                .ToList()
        };
    }
}
