using InvNexus.PurchaseService.Application.DTOs;
using InvNexus.PurchaseService.Application.Interfaces;
using InvNexus.PurchaseService.Application.Mediator;

namespace InvNexus.PurchaseService.Application.Queries.GetPurchaseOrders;

public class GetPurchaseOrdersQueryHandler(IPurchaseOrderRepository purchaseOrderRepository)
    : IQueryHandler<GetPurchaseOrdersQuery, IReadOnlyList<PurchaseOrderListItemResponseDto>>
{
    public async Task<IReadOnlyList<PurchaseOrderListItemResponseDto>> HandleAsync(GetPurchaseOrdersQuery query, CancellationToken cancellationToken)
    {
        var purchaseOrders = await purchaseOrderRepository.GetAllWithItemsAsync(cancellationToken);

        return purchaseOrders
            .Select(order => new PurchaseOrderListItemResponseDto
            {
                Id = order.Id,
                PurchaseNumber = order.PurchaseNumber,
                Status = order.Status,
                CreatedAt = order.CreatedAt
            })
            .ToList();
    }
}
