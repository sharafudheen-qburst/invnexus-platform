namespace InvNexus.SalesService.Domain.Entities;

public class SalesOrder
{
    public Guid Id { get; set; }
    public string SalesNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ICollection<SalesOrderItem> Items { get; set; } = [];
}
