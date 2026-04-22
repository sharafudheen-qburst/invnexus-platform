using InvNexus.PurchaseService.Application.DTOs;
using InvNexus.PurchaseService.Application.Mediator;

namespace InvNexus.PurchaseService.Application.Commands.CreatePurchaseOrder;

public record CreatePurchaseOrderCommand(IReadOnlyList<CreatePurchaseOrderItemInput> Items) : ICommand<PurchaseOrderActionResponseDto>;

public record CreatePurchaseOrderItemInput(Guid ProductId, int Quantity, decimal UnitPrice);
