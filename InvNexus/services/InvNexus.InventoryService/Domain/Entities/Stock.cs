namespace InvNexus.InventoryService.Domain.Entities;

public class Stock
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public Product? Product { get; set; }
}
