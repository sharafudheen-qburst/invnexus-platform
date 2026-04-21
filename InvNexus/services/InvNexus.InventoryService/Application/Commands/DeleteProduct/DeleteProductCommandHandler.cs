using InvNexus.InventoryService.Application.Interfaces;
using InvNexus.InventoryService.Application.Mediator;

namespace InvNexus.InventoryService.Application.Commands.DeleteProduct;

public class DeleteProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteProductCommand, bool>
{
    public async Task<bool> HandleAsync(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId, cancellationToken);
        if (product is null)
        {
            return false;
        }

        productRepository.Remove(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
