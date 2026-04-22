namespace InvNexus.PurchaseService.Domain.Entities;

public class PurchaseOrderItem
{
    public Guid Id { get; set; }
    public Guid PurchaseOrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public PurchaseOrder? PurchaseOrder { get; set; }
}
