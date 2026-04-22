using InvNexus.SalesService.Application.DTOs;
using InvNexus.SalesService.Application.Mediator;

namespace InvNexus.SalesService.Application.Commands.CreateSalesOrder;

public record CreateSalesOrderCommand(IReadOnlyList<CreateSalesOrderItemInput> Items) : ICommand<SalesOrderActionResponseDto>;

public record CreateSalesOrderItemInput(Guid ProductId, int Quantity, decimal UnitPrice);
