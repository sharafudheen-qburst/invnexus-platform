using System.ComponentModel.DataAnnotations;

namespace InvNexus.InventoryService.Application.DTOs;

public class CreateProductRequestDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal Price { get; set; }

    public bool IsActive { get; set; } = true;
}
