using InvNexus.SalesService.Application.DTOs;
using InvNexus.SalesService.Application.Events;
using InvNexus.SalesService.Application.Interfaces;
using InvNexus.SalesService.Application.Mediator;
using InvNexus.SalesService.Domain.Constants;

namespace InvNexus.SalesService.Application.Commands.CompleteSalesOrder;

public class CompleteSalesOrderCommandHandler(
    ISalesOrderRepository salesOrderRepository,
    IIntegrationEventPublisher integrationEventPublisher,
    IUnitOfWork unitOfWork) : ICommandHandler<CompleteSalesOrderCommand, SalesOrderActionResponseDto?>
{
    public async Task<SalesOrderActionResponseDto?> HandleAsync(CompleteSalesOrderCommand command, CancellationToken cancellationToken)
    {
        var salesOrder = await salesOrderRepository.GetByIdAsync(command.SalesOrderId, cancellationToken);
        if (salesOrder is null)
        {
            return null;
        }

        if (!string.Equals(salesOrder.Status, SalesOrderStatuses.Created, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Sales order can only be completed from Created status.");
        }

        salesOrder.Status = SalesOrderStatuses.Completed;
        salesOrderRepository.Update(salesOrder);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await integrationEventPublisher.PublishSalesCompletedAsync(
            new SalesCompletedEvent(salesOrder.Id, salesOrder.SalesNumber, DateTime.UtcNow),
            cancellationToken);

        return new SalesOrderActionResponseDto
        {
            Id = salesOrder.Id,
            SalesNumber = salesOrder.SalesNumber,
            Status = salesOrder.Status
        };
    }
}
