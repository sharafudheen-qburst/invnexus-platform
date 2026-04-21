using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Interfaces;
using InvNexus.InventoryService.Application.Mediator;

namespace InvNexus.InventoryService.Application.Queries.GetStockByProductId;

public class GetStockByProductIdQueryHandler(IStockRepository stockRepository) :    
    IQueryHandler<GetStockByProductIdQuery, StockResponseDto?>
{
    public async Task<StockResponseDto?> HandleAsync(GetStockByProductIdQuery query, CancellationToken cancellationToken)
    {
        var stock = await stockRepository.GetByProductIdAsync(query.ProductId, cancellationToken);
        if (stock is null)
        {
            return null;
        }

        return new StockResponseDto
        {
            ProductId = stock.ProductId,
            Quantity = stock.Quantity
        };
    }
}
