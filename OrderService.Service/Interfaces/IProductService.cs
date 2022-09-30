using OrderService.Domain.Configuration;
using OrderService.Domain.Entities.Porducts;
using OrderService.Service.DTOs.Commons;
using System.Linq.Expressions;

namespace OrderService.Service.Interfaces.IProductServices;

public interface IProductService
{
    ValueTask<Product> CreateAsync(ProductForCreationDto model);
    ValueTask<Product> UpdateAsync(long id, ProductForCreationDto model);
    ValueTask<bool> DeleteAsync(Expression<Func<Product, bool>> expression);
    ValueTask<Product> GetAsync(Expression<Func<Product, bool>> expression);
    IEnumerable<Product> GetAll(
        PaginationParams @params,
        Expression<Func<Product, bool>> expression = null);
}
