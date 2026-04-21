namespace InvNexus.InventoryService.Application.Queries.GetProducts;

using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Mediator;

public record GetProductsQuery : IQuery<IReadOnlyList<ProductResponseDto>>;
