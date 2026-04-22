namespace InvNexus.PurchaseService.Application.DTOs;

public class PurchaseOrderDetailResponseDto
{
    public Guid Id { get; set; }
    public string PurchaseNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<PurchaseOrderItemResponseDto> Items { get; set; } = [];
}

public class PurchaseOrderItemResponseDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
