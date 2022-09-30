using OrderService.Domain.Configuration;
using OrderService.Domain.Entities.Porducts;
using OrderService.Service.DTOs;
using System.Linq.Expressions;

namespace OrderService.Service.Interfaces.ICategorieService;

public interface ICategoryService
{
    ValueTask<ProductCategory> CreateAsync(CategoryForCreationDto model);
    ValueTask<ProductCategory> UpdateAsync(long id, CategoryForCreationDto model);
    ValueTask<bool> DeleteAsync(Expression<Func<ProductCategory, bool>> expression);
    ValueTask<ProductCategory> GetAsync(Expression<Func<ProductCategory, bool>> expression);
    IEnumerable<ProductCategory> GetAll(
        PaginationParams @params,
        Expression<Func<ProductCategory, bool>> expression = null);
}
