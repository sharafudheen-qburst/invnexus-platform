namespace InvNexus.InventoryService.Application.Commands.UpdateStock;

using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Mediator;

public record UpdateStockCommand(Guid ProductId, int Quantity) : ICommand<StockResponseDto?>;
