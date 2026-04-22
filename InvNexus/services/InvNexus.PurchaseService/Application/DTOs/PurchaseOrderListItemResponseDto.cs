namespace InvNexus.PurchaseService.Application.DTOs;

public class PurchaseOrderListItemResponseDto
{
    public Guid Id { get; set; }
    public string PurchaseNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
