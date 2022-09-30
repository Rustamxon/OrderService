using OrderService.Domain.Commons;

namespace OrderService.Domain.Entities.Users;

public class Address : Auditable
{
    public string City { get; set; }
    public string Street { get; set; }
    public string Home { get; set; }
    public string Landmark { get; set; }
}
