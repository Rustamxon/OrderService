
using OrderService.Domain.Entities.Commons;
using OrderService.Domain.Entities.Orders;
using OrderService.Domain.Entities.Porducts;
using OrderService.Domain.Entities.Users;

namespace OrderService.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepo<ProductCategory> ProductCategories { get; }
    IGenericRepo<Product> Products { get; }
    IGenericRepo<Order> Orders { get; }
    IGenericRepo<Payment> Payments { get; }
    IGenericRepo<User> Users { get; }
    IGenericRepo<Attachment> Attachments { get; }

    ValueTask SaveChangesAsync();
}
