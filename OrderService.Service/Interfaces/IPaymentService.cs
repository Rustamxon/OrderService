using OrderService.Domain.Configuration;
using OrderService.Domain.Entities.Orders;
using OrderService.Service.DTOs.Payments;
using System.Linq.Expressions;

namespace OrderService.Service.Interfaces.IPaymentService;

public interface IPaymentService
{
    ValueTask<Payment> CreateAsync(PaymentForCreationDto model);
    ValueTask<Payment> UpdateAsync(long id, PaymentForCreationDto model);
    ValueTask<bool> DeleteAsync(Expression<Func<Payment, bool>> expression);
    ValueTask<Payment> GetAsync(Expression<Func<Payment, bool>> expression);
    IEnumerable<Payment> GetAll(
        PaginationParams @params,
        Expression<Func<Payment, bool>> expression = null);
}
