using InvNexus.SalesService.Application.DTOs;
using InvNexus.SalesService.Application.Mediator;

namespace InvNexus.SalesService.Application.Commands.CompleteSalesOrder;

public record CompleteSalesOrderCommand(Guid SalesOrderId) : ICommand<SalesOrderActionResponseDto?>;
