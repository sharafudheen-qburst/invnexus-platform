using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Interfaces;
using InvNexus.InventoryService.Application.Mediator;

namespace InvNexus.InventoryService.Application.Commands.UpdateStock;

public class UpdateStockCommandHandler(
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork) :
    ICommandHandler<UpdateStockCommand, StockResponseDto?>
{
    public async Task<StockResponseDto?> HandleAsync(UpdateStockCommand command, CancellationToken cancellationToken)
    {
        if (command.Quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative.");
        }

        var stock = await stockRepository.GetByProductIdAsync(command.ProductId, cancellationToken);
        if (stock is null)
        {
            return null;
        }

        stock.Quantity = command.Quantity;
        stockRepository.Update(stock);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new StockResponseDto
        {
            ProductId = stock.ProductId,
            Quantity = stock.Quantity
        };
    }
}
