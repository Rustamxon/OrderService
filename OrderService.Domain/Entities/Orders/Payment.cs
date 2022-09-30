using OrderService.Domain.Commons;
using OrderService.Domain.Enums;

namespace OrderService.Domain.Entities.Orders;

public class Payment : Auditable
{
    public decimal PaidPrice { get; set; }
    public PaymentType PaymentType { get; set; }

    public long OrderId { get; set; }
    public Order Order { get; set; }
}
