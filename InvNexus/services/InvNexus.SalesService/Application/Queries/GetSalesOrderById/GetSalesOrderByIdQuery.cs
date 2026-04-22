using InvNexus.SalesService.Application.DTOs;
using InvNexus.SalesService.Application.Mediator;

namespace InvNexus.SalesService.Application.Queries.GetSalesOrderById;

public record GetSalesOrderByIdQuery(Guid SalesOrderId) : IQuery<SalesOrderDetailResponseDto?>;
