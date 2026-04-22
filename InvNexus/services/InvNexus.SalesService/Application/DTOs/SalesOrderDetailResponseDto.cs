namespace InvNexus.SalesService.Application.DTOs;

public class SalesOrderDetailResponseDto
{
    public Guid Id { get; set; }
    public string SalesNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<SalesOrderItemResponseDto> Items { get; set; } = [];
}

public class SalesOrderItemResponseDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
