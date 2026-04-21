namespace InvNexus.InventoryService.Application.Commands.CreateProduct;

using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Mediator;

public record CreateProductCommand(string Name, decimal Price, bool IsActive) : ICommand<ProductResponseDto>;
