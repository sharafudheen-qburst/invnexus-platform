using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Mediator;

namespace InvNexus.InventoryService.Application.Commands.UpdateProduct;

public record UpdateProductCommand(Guid ProductId, string Name, decimal Price, bool IsActive) : ICommand<ProductResponseDto?>;
