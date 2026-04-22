namespace InvNexus.PurchaseService.Domain.Entities;

public class PurchaseOrder
{
    public Guid Id { get; set; }
    public string PurchaseNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ICollection<PurchaseOrderItem> Items { get; set; } = [];
}
