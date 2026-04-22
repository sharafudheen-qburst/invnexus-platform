using InvNexus.PurchaseService.Application.DTOs;
using InvNexus.PurchaseService.Application.Mediator;

namespace InvNexus.PurchaseService.Application.Queries.GetPurchaseOrderById;

public record GetPurchaseOrderByIdQuery(Guid PurchaseOrderId) : IQuery<PurchaseOrderDetailResponseDto?>;
