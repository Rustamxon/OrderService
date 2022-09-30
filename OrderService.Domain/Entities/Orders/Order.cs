using OrderService.Domain.Commons;
using OrderService.Domain.Entities.Users;

namespace OrderService.Domain.Entities.Orders;

public class Order : Auditable
{
    public decimal TotalPrice { get; set; }
    public bool IsPaid { get; set; }
    public int AddressId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<OrderProduct> OrderProducts { get; set; }
}

