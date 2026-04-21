namespace InvNexus.InventoryService.Application.Queries.GetStockByProductId;

using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Mediator;

public record GetStockByProductIdQuery(Guid ProductId) : IQuery<StockResponseDto?>;
