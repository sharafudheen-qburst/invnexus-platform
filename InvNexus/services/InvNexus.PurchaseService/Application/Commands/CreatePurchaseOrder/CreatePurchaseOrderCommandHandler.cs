using InvNexus.PurchaseService.Application.DTOs;
using InvNexus.PurchaseService.Application.Interfaces;
using InvNexus.PurchaseService.Application.Mediator;
using InvNexus.PurchaseService.Domain.Constants;
using InvNexus.PurchaseService.Domain.Entities;

namespace InvNexus.PurchaseService.Application.Commands.CreatePurchaseOrder;

public class CreatePurchaseOrderCommandHandler(
    IPurchaseOrderRepository purchaseOrderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreatePurchaseOrderCommand, PurchaseOrderActionResponseDto>
{
    public async Task<PurchaseOrderActionResponseDto> HandleAsync(CreatePurchaseOrderCommand command, CancellationToken cancellationToken)
    {
        if (command.Items.Count == 0)
        {
            throw new ArgumentException("Purchase order must contain at least one item.");
        }

        foreach (var item in command.Items)
        {
            if (item.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            if (item.UnitPrice < 0)
            {
                throw new ArgumentException("UnitPrice cannot be negative.");
            }
        }

        var totalOrders = await purchaseOrderRepository.GetCountAsync(cancellationToken);
        var purchaseNumber = $"PO-{totalOrders + 1:0000}";

        var purchaseOrder = new PurchaseOrder
        {
            Id = Guid.NewGuid(),
            PurchaseNumber = purchaseNumber,
            Status = PurchaseOrderStatuses.Created,
            CreatedAt = DateTime.UtcNow,
            Items = command.Items.Select(item => new PurchaseOrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList()
        };

        await purchaseOrderRepository.AddAsync(purchaseOrder, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new PurchaseOrderActionResponseDto
        {
            Id = purchaseOrder.Id,
            PurchaseNumber = purchaseOrder.PurchaseNumber,
            Status = purchaseOrder.Status
        };
    }
}
