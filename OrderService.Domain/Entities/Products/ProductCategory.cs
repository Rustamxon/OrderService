using OrderService.Domain.Commons;

namespace OrderService.Domain.Entities.Porducts;

public class ProductCategory : Auditable
{
    public string Name { get; set; }
}
