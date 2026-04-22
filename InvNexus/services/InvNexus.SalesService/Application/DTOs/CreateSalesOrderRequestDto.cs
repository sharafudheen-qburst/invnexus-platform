using System.ComponentModel.DataAnnotations;

namespace InvNexus.SalesService.Application.DTOs;

public class CreateSalesOrderRequestDto
{
    [Required]
    [MinLength(1)]
    public List<CreateSalesOrderItemRequestDto> Items { get; set; } = [];
}

public class CreateSalesOrderItemRequestDto
{
    [Required]
    public Guid ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal UnitPrice { get; set; }
}
