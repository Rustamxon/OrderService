
using OrderService.Data.Contexts;
using OrderService.Data.IRepositories;
using OrderService.Domain.Entities.Commons;
using OrderService.Domain.Entities.Orders;
using OrderService.Domain.Entities.Porducts;
using OrderService.Domain.Entities.Users;

namespace OrderService.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IGenericRepo<User> Users { get; }
    public IGenericRepo<Order> Orders { get; }
    public IGenericRepo<ProductCategory> ProductCategories { get; }
    public IGenericRepo<Product> Products { get; }
    public IGenericRepo<Payment> Payments { get; }
    public IGenericRepo<Attachment> Attachments { get; }


    public AppDbContext context;
    public UnitOfWork(AppDbContext context)
    {
        this.context = context;

        ProductCategories = new GenericRepo<ProductCategory>(context);
        Products = new GenericRepo<Product>(context);
        Orders = new GenericRepo<Order>(context);
        Payments = new GenericRepo<Payment>(context);
        Users = new GenericRepo<User>(context);
        Attachments = new GenericRepo<Attachment>(context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask SaveChangesAsync()
    {
        await this.context.SaveChangesAsync();
    }
}
