using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Interfaces;
using InvNexus.InventoryService.Application.Mediator;
using InvNexus.InventoryService.Domain.Entities;

namespace InvNexus.InventoryService.Application.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork) :
    ICommandHandler<CreateProductCommand, ProductResponseDto>
{
    public async Task<ProductResponseDto> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new ArgumentException("Product name is required.");
        }

        if (command.Price < 0)
        {
            throw new ArgumentException("Price cannot be negative.");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name.Trim(),
            Price = command.Price,
            IsActive = command.IsActive
        };

        var stock = new Stock
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            Quantity = 0
        };

        await productRepository.AddAsync(product, cancellationToken);
        await stockRepository.AddAsync(stock, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            IsActive = product.IsActive,
            Quantity = stock.Quantity
        };
    }
}
