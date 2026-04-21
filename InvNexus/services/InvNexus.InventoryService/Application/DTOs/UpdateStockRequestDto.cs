using System.ComponentModel.DataAnnotations;

namespace InvNexus.InventoryService.Application.DTOs;

public class UpdateStockRequestDto
{
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
}
