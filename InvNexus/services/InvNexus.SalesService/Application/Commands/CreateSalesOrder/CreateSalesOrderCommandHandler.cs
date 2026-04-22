using InvNexus.SalesService.Application.DTOs;
using InvNexus.SalesService.Application.Interfaces;
using InvNexus.SalesService.Application.Mediator;
using InvNexus.SalesService.Domain.Constants;
using InvNexus.SalesService.Domain.Entities;

namespace InvNexus.SalesService.Application.Commands.CreateSalesOrder;

public class CreateSalesOrderCommandHandler(
    ISalesOrderRepository salesOrderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateSalesOrderCommand, SalesOrderActionResponseDto>
{
    public async Task<SalesOrderActionResponseDto> HandleAsync(CreateSalesOrderCommand command, CancellationToken cancellationToken)
    {
        if (command.Items.Count == 0)
        {
            throw new ArgumentException("Sales order must contain at least one item.");
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

        var totalOrders = await salesOrderRepository.GetCountAsync(cancellationToken);
        var salesNumber = $"SO-{totalOrders + 1:0000}";

        var salesOrder = new SalesOrder
        {
            Id = Guid.NewGuid(),
            SalesNumber = salesNumber,
            Status = SalesOrderStatuses.Created,
            CreatedAt = DateTime.UtcNow,
            Items = command.Items.Select(item => new SalesOrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList()
        };

        await salesOrderRepository.AddAsync(salesOrder, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new SalesOrderActionResponseDto
        {
            Id = salesOrder.Id,
            SalesNumber = salesOrder.SalesNumber,
            Status = salesOrder.Status
        };
    }
}
