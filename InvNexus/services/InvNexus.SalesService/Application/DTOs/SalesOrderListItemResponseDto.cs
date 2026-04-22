namespace InvNexus.SalesService.Application.DTOs;

public class SalesOrderListItemResponseDto
{
    public Guid Id { get; set; }
    public string SalesNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
