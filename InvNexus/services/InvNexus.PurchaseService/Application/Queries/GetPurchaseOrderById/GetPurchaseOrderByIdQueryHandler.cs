using InvNexus.PurchaseService.Application.DTOs;
using InvNexus.PurchaseService.Application.Interfaces;
using InvNexus.PurchaseService.Application.Mediator;

namespace InvNexus.PurchaseService.Application.Queries.GetPurchaseOrderById;

public class GetPurchaseOrderByIdQueryHandler(IPurchaseOrderRepository purchaseOrderRepository)
    : IQueryHandler<GetPurchaseOrderByIdQuery, PurchaseOrderDetailResponseDto?>
{
    public async Task<PurchaseOrderDetailResponseDto?> HandleAsync(GetPurchaseOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var purchaseOrder = await purchaseOrderRepository.GetByIdWithItemsAsync(query.PurchaseOrderId, cancellationToken);
        if (purchaseOrder is null)
        {
            return null;
        }

        return new PurchaseOrderDetailResponseDto
        {
            Id = purchaseOrder.Id,
            PurchaseNumber = purchaseOrder.PurchaseNumber,
            Status = purchaseOrder.Status,
            CreatedAt = purchaseOrder.CreatedAt,
            Items = purchaseOrder.Items
                .Select(item => new PurchaseOrderItemResponseDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                })
                .ToList()
        };
    }
}
