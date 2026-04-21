using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Interfaces;
using InvNexus.InventoryService.Application.Mediator;

namespace InvNexus.InventoryService.Application.Queries.GetProducts;

public class GetProductsQueryHandler(IProductRepository productRepository) :
    IGetProductsQueryHandler,
    IQueryHandler<GetProductsQuery, IReadOnlyList<ProductResponseDto>>
{
    public async Task<IReadOnlyList<ProductResponseDto>> HandleAsync(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllWithStockAsync(cancellationToken);

        return products
            .Select(product => new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                IsActive = product.IsActive,
                Quantity = product.Stock?.Quantity ?? 0
            })
            .ToList();
    }
}
