using InvNexus.SalesService.Application.Commands.CompleteSalesOrder;
using InvNexus.SalesService.Application.Commands.CreateSalesOrder;
using InvNexus.SalesService.Application.DTOs;
using InvNexus.SalesService.Application.Mediator;
using InvNexus.SalesService.Application.Queries.GetSalesOrderById;
using InvNexus.SalesService.Application.Queries.GetSalesOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvNexus.SalesService.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/sales")]
public class SalesController(
    ICommandMediator commandMediator,
    IQueryMediator queryMediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<SalesOrderActionResponseDto>> Create(
        [FromBody] CreateSalesOrderRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateSalesOrderCommand(
            request.Items.Select(item => new CreateSalesOrderItemInput(item.ProductId, item.Quantity, item.UnitPrice)).ToList());

        try
        {
            var createdSalesOrder = await commandMediator.SendAsync(command, cancellationToken);
            return Created($"/api/sales/{createdSalesOrder.Id}", createdSalesOrder);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SalesOrderListItemResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var salesOrders = await queryMediator.SendAsync(new GetSalesOrdersQuery(), cancellationToken);
        return Ok(salesOrders);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SalesOrderDetailResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var salesOrder = await queryMediator.SendAsync(new GetSalesOrderByIdQuery(id), cancellationToken);
        return salesOrder is null ? NotFound() : Ok(salesOrder);
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<ActionResult<SalesOrderActionResponseDto>> Complete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var salesOrder = await commandMediator.SendAsync(new CompleteSalesOrderCommand(id), cancellationToken);
            return salesOrder is null ? NotFound() : Ok(salesOrder);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
