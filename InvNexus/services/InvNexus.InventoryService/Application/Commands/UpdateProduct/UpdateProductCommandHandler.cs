using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Interfaces;
using InvNexus.InventoryService.Application.Mediator;

namespace InvNexus.InventoryService.Application.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProductCommand, ProductResponseDto?>
{
    public async Task<ProductResponseDto?> HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new ArgumentException("Product name is required.");
        }

        if (command.Price < 0)
        {
            throw new ArgumentException("Price cannot be negative.");
        }

        var product = await productRepository.GetByIdAsync(command.ProductId, cancellationToken);
        if (product is null)
        {
            return null;
        }

        product.Name = command.Name.Trim();
        product.Price = command.Price;
        product.IsActive = command.IsActive;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var productWithStock = await productRepository.GetByIdWithStockAsync(command.ProductId, cancellationToken);
        if (productWithStock is null)
        {
            return null;
        }

        return new ProductResponseDto
        {
            Id = productWithStock.Id,
            Name = productWithStock.Name,
            Price = productWithStock.Price,
            IsActive = productWithStock.IsActive,
            Quantity = productWithStock.Stock?.Quantity ?? 0
        };
    }
}
