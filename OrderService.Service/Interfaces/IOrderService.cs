using OrderService.Domain.Configuration;
using OrderService.Domain.Entities.Orders;
using OrderService.Service.DTOs.Orders;
using System.Linq.Expressions;

namespace OrderService.Service.Interfaces.IOrderService;

public interface IOrderService
{
    ValueTask<Order> CreateAsync(OrderForCreationDto model);
    ValueTask<Order> UpdateAsync(long id, OrderForCreationDto model);
    ValueTask<bool> DeleteAsync(Expression<Func<Order, bool>> expression);
    ValueTask<Order> GetAsync(Expression<Func<Order, bool>> expression);
    IEnumerable<Order> GetAll(
        PaginationParams @params,
        Expression<Func<Order, bool>> expression = null);
}
