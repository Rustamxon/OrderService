using OrderService.Domain.Commons;
using System.Linq.Expressions;

namespace OrderService.Data.IRepositories;

public interface IGenericRepo<T> where T : Auditable
{
    ValueTask<T> CreateAsync(T entity);
    ValueTask<T> UpdateAsync(T entity);
    ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression);
    ValueTask<T> GetAsync(Expression<Func<T, bool>> expression);
    IQueryable<T> Where(Expression<Func<T, bool>> expression = null);
}
