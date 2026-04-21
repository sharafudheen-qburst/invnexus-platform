using InvNexus.InventoryService.Application.Commands.DeleteProduct;
using InvNexus.InventoryService.Application.Commands.UpdateProduct;
using InvNexus.InventoryService.Application.Commands.CreateProduct;
using InvNexus.InventoryService.Application.DTOs;
using InvNexus.InventoryService.Application.Mediator;
using InvNexus.InventoryService.Application.Queries.GetProductById;
using InvNexus.InventoryService.Application.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace InvNexus.InventoryService.Presentation.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(
    ICommandMediator commandMediator,
    IQueryMediator queryMediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> Create([FromBody] CreateProductRequestDto request, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(request.Name, request.Price, request.IsActive);

        try
        {
            var createdProduct = await commandMediator.SendAsync(command, cancellationToken);
            return Created($"/api/products/{createdProduct.Id}", createdProduct);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await queryMediator.SendAsync(new GetProductsQuery(), cancellationToken);
        return Ok(products);
    }

    [HttpGet("{productId:guid}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(Guid productId, CancellationToken cancellationToken)
    {
        var product = await queryMediator.SendAsync(new GetProductByIdQuery(productId), cancellationToken);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPut("{productId:guid}")]
    public async Task<ActionResult<ProductResponseDto>> Update(
        Guid productId,
        [FromBody] UpdateProductRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(productId, request.Name, request.Price, request.IsActive);

        try
        {
            var updatedProduct = await commandMediator.SendAsync(command, cancellationToken);
            return updatedProduct is null ? NotFound() : Ok(updatedProduct);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> Delete(Guid productId, CancellationToken cancellationToken)
    {
        var isDeleted = await commandMediator.SendAsync(new DeleteProductCommand(productId), cancellationToken);
        return isDeleted ? NoContent() : NotFound();
    }
}
