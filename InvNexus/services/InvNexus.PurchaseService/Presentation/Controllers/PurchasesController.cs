using InvNexus.PurchaseService.Application.Commands.CreatePurchaseOrder;
using InvNexus.PurchaseService.Application.Commands.ReceiveGoods;
using InvNexus.PurchaseService.Application.DTOs;
using InvNexus.PurchaseService.Application.Mediator;
using InvNexus.PurchaseService.Application.Queries.GetPurchaseOrderById;
using InvNexus.PurchaseService.Application.Queries.GetPurchaseOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvNexus.PurchaseService.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/purchases")]
public class PurchasesController(
    ICommandMediator commandMediator,
    IQueryMediator queryMediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PurchaseOrderActionResponseDto>> Create(
        [FromBody] CreatePurchaseOrderRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreatePurchaseOrderCommand(
            request.Items.Select(item => new CreatePurchaseOrderItemInput(item.ProductId, item.Quantity, item.UnitPrice)).ToList());

        try
        {
            var createdPurchaseOrder = await commandMediator.SendAsync(command, cancellationToken);
            return Created($"/api/purchases/{createdPurchaseOrder.Id}", createdPurchaseOrder);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PurchaseOrderListItemResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var purchaseOrders = await queryMediator.SendAsync(new GetPurchaseOrdersQuery(), cancellationToken);
        return Ok(purchaseOrders);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PurchaseOrderDetailResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var purchaseOrder = await queryMediator.SendAsync(new GetPurchaseOrderByIdQuery(id), cancellationToken);
        return purchaseOrder is null ? NotFound() : Ok(purchaseOrder);
    }

    [HttpPost("{id:guid}/receive")]
    public async Task<ActionResult<PurchaseOrderActionResponseDto>> Receive(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var purchaseOrder = await commandMediator.SendAsync(new ReceiveGoodsCommand(id), cancellationToken);
            return purchaseOrder is null ? NotFound() : Ok(purchaseOrder);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
