using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Interfaces;
using InvNexus.InventoryService.Application.Mediator;

namespace InvNexus.InventoryService.Application.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IQueryHandler<GetProductByIdQuery, ProductResponseDto?>
{
    public async Task<ProductResponseDto?> HandleAsync(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdWithStockAsync(query.ProductId, cancellationToken);
        if (product is null)
        {
            return null;
        }

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            IsActive = product.IsActive,
            Quantity = product.Stock?.Quantity ?? 0
        };
    }
}
