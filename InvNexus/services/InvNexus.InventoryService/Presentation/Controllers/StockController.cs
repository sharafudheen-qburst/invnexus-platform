using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Mediator;
using InvNexus.InventoryService.Application.Queries.GetStockByProductId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvNexus.InventoryService.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/stock")]
public class StockController(
    IQueryMediator queryMediator) : ControllerBase
{
    [HttpGet("{productId:guid}")]
    public async Task<ActionResult<StockResponseDto>> GetByProductId(Guid productId, CancellationToken cancellationToken)
    {
        var stock = await queryMediator.SendAsync(new GetStockByProductIdQuery(productId), cancellationToken);
        return stock is null ? NotFound() : Ok(stock);
    }
}
