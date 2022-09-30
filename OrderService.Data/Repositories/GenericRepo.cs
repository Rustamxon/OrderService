
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Contexts;
using OrderService.Data.IRepositories;
using OrderService.Domain.Commons;
using System.Linq.Expressions;

namespace OrderService.Data.Repositories;

#pragma warning disable
public class GenericRepo<T> : IGenericRepo<T> where T : Auditable
{
    private readonly AppDbContext context;
    private readonly DbSet<T> dbSet;

    public GenericRepo(AppDbContext orderServiceContext)
    {
        this.context = orderServiceContext;
        this.dbSet = context.Set<T>();
    }

    public async ValueTask<T> CreateAsync(T entity) =>
        (await dbSet.AddAsync(entity)).Entity;

    public async ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await dbSet.FirstOrDefaultAsync(expression);

        if (entity == null)
            return false;

        dbSet.Remove(entity);

        return true;
    }

    public async ValueTask<IEnumerable<T>> GetAllAsync() =>
        await dbSet.ToListAsync();

    public async ValueTask<T> GetAsync(Expression<Func<T, bool>> expression) =>
        await dbSet.FirstOrDefaultAsync(expression);

    public async ValueTask<T> UpdateAsync(T entity) =>
        dbSet.Update(entity).Entity;

    public IQueryable<T> Where(Expression<Func<T, bool>> expression = null) =>
        expression is null ? dbSet : dbSet.Where(expression);

}
