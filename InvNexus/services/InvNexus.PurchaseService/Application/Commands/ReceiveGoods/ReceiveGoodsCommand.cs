using InvNexus.PurchaseService.Application.DTOs;
using InvNexus.PurchaseService.Application.Mediator;

namespace InvNexus.PurchaseService.Application.Commands.ReceiveGoods;

public record ReceiveGoodsCommand(Guid PurchaseOrderId) : ICommand<PurchaseOrderActionResponseDto?>;
