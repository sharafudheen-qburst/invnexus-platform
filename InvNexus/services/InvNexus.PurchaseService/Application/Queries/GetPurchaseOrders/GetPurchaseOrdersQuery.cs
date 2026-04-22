using InvNexus.PurchaseService.Application.DTOs;
using InvNexus.PurchaseService.Application.Mediator;

namespace InvNexus.PurchaseService.Application.Queries.GetPurchaseOrders;

public record GetPurchaseOrdersQuery : IQuery<IReadOnlyList<PurchaseOrderListItemResponseDto>>;
