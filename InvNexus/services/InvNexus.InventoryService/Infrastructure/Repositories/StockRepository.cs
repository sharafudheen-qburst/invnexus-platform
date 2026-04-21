using InvNexus.InventoryService.Application.Interfaces;
using InvNexus.InventoryService.Domain.Entities;
using InvNexus.InventoryService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvNexus.InventoryService.Infrastructure.Repositories;

public class StockRepository(InventoryDbContext dbContext) : IStockRepository
{
    public async Task AddAsync(Stock stock, CancellationToken cancellationToken)
    {
        await dbContext.Stocks.AddAsync(stock, cancellationToken);
    }

    public async Task<Stock?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        return await dbContext.Stocks
            .AsNoTracking()
            .SingleOrDefaultAsync(stock => stock.ProductId == productId, cancellationToken);
    }

    public void Update(Stock stock)
    {
        dbContext.Stocks.Update(stock);
    }
}
