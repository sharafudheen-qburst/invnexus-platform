using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Queries.GetProducts;

namespace InvNexus.InventoryService.Application.Interfaces;

public interface IGetProductsQueryHandler
{
    Task<IReadOnlyList<ProductResponseDto>> HandleAsync(GetProductsQuery query, CancellationToken cancellationToken);
}
