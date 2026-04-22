using InvNexus.SalesService.Application.DTOs;
using InvNexus.SalesService.Application.Mediator;

namespace InvNexus.SalesService.Application.Queries.GetSalesOrders;

public record GetSalesOrdersQuery : IQuery<IReadOnlyList<SalesOrderListItemResponseDto>>;
