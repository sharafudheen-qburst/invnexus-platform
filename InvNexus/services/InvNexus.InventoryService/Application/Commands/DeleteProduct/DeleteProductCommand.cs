using InvNexus.InventoryService.Application.Mediator;

namespace InvNexus.InventoryService.Application.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<bool>;
