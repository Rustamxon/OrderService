using OrderService.Service.DTOs.Commons;

namespace OrderService.Service.DTOs.Orders;

public class OrderForCreationDto : ProductForCreationDto
{
    public decimal TotalPrice { get; set; }
    public bool IsPaid { get; set; }
    public long AddressId { get; set; }
    public long UserId { get; set; }

    public ICollection<OrderProductForCreationDto> OrderProducts { get; set; }
}
