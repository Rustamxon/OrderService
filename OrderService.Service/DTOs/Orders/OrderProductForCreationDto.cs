using System.ComponentModel.DataAnnotations;

namespace OrderService.Service.DTOs.Orders;

public class OrderProductForCreationDto
{
    [Required]
    public long Quatity { get; set; }

    [Required]
    public long CategoryId { get; set; }

    [Required]
    public long ProductId { get; set; }
}
