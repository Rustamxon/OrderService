using OrderService.Domain.Commons;
using OrderService.Domain.Entities.Commons;

namespace OrderService.Domain.Entities.Porducts;

public class Product : Auditable
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public long CategoryId { get; set; }
    public ProductCategory Category { get; set; }

    public long? FileId { get; set; }
    public Attachment Attachment { get; set; }
}
