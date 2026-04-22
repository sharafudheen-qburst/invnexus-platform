using InvNexus.PurchaseService.Application.DTOs;
using InvNexus.PurchaseService.Application.Events;
using InvNexus.PurchaseService.Application.Interfaces;
using InvNexus.PurchaseService.Application.Mediator;
using InvNexus.PurchaseService.Domain.Constants;

namespace InvNexus.PurchaseService.Application.Commands.ReceiveGoods;

public class ReceiveGoodsCommandHandler(
    IPurchaseOrderRepository purchaseOrderRepository,
    IIntegrationEventPublisher integrationEventPublisher,
    IUnitOfWork unitOfWork) : ICommandHandler<ReceiveGoodsCommand, PurchaseOrderActionResponseDto?>
{
    public async Task<PurchaseOrderActionResponseDto?> HandleAsync(ReceiveGoodsCommand command, CancellationToken cancellationToken)
    {
        var purchaseOrder = await purchaseOrderRepository.GetByIdAsync(command.PurchaseOrderId, cancellationToken);
        if (purchaseOrder is null)
        {
            return null;
        }

        if (!string.Equals(purchaseOrder.Status, PurchaseOrderStatuses.Created, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Goods can only be received for purchase orders in Created status.");
        }

        purchaseOrder.Status = PurchaseOrderStatuses.Received;
        purchaseOrderRepository.Update(purchaseOrder);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await integrationEventPublisher.PublishGoodsReceivedAsync(
            new GoodsReceivedEvent(purchaseOrder.Id, purchaseOrder.PurchaseNumber, DateTime.UtcNow),
            cancellationToken);

        return new PurchaseOrderActionResponseDto
        {
            Id = purchaseOrder.Id,
            PurchaseNumber = purchaseOrder.PurchaseNumber,
            Status = purchaseOrder.Status
        };
    }
}
