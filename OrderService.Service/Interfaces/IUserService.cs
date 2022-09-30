using OrderService.Domain.Configuration;
using OrderService.Domain.Entities.Users;
using OrderService.Service.DTOs.Customers;
using System.Linq.Expressions;

namespace OrderService.Service.Interfaces.CustomerService;

public interface IUserService
{
    ValueTask<User> CreateAsync(UserForCreationDto model);
    ValueTask<User> UpdateAsync(long id, UserForCreationDto model);
    ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
    ValueTask<User> GetAsync(Expression<Func<User, bool>> expression);
    IEnumerable<User> GetAll(
        PaginationParams @params,
        Expression<Func<User, bool>> expression = null);
}
