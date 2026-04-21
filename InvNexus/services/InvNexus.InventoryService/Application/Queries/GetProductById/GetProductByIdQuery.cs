using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Mediator;

namespace InvNexus.InventoryService.Application.Queries.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<ProductResponseDto?>;
