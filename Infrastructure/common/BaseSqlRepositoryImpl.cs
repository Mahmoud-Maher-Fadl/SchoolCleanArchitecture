using System.Linq.Expressions;
using Domain.common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.common;

public abstract class BaseSqlRepositoryImpl<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly DbSet<T> table;

    protected BaseSqlRepositoryImpl(DbSet<T> table)
    {
        this.table = table;
    }

    public async Task<Result<T>> Add(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await table.AddAsync(entity, cancellationToken);
            return result.Entity.AsSuccessResult();
        }
        catch (Exception e)
        {
            return e.Message.AsFailureResult<T>();
        }
    }

    public Task<Result<T>> Update(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            entity.UpdateDate = DateTime.Now;
            var result = table.Update(entity);
            return Task.FromResult(result.Entity.AsSuccessResult());
        }
        catch (Exception e)
        {
            return Task.FromResult(e.Message.AsFailureResult<T>());
        }
    }

    public Task<Result<T>> Delete(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = table.Remove(entity);
            return Task.FromResult(result.Entity.AsSuccessResult());
        }
        catch (Exception e)
        {
            return Task.FromResult(e.Message.AsFailureResult<T>());
        }
    }

    public async Task<Result<T[]>> AddRange(T[] entities,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await table.AddRangeAsync(entities, cancellationToken);
            return entities.AsSuccessResult();
        }
        catch (Exception e)
        {
            return e.Message.AsFailureResult<T[]>();
        }
    }

    public Task<Result<T[]>> UpdateRange(T[] entities,
        CancellationToken cancellationToken = default)
    {
        try
        {
            entities.ToList().ForEach(x => x.UpdateDate = DateTime.Now);
            table.UpdateRange(entities);
            return Task.FromResult(entities.AsSuccessResult());
        }
        catch (Exception e)
        {
            return Task.FromResult(e.Message.AsFailureResult<T[]>());
        }
    }

    public async Task<Result<T>> DeleteById(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await table.FindAsync(id);
            if (entity == null)
                return $"{typeof(T).Name} not found".AsFailureResult<T>();
            var result = table.Remove(entity);
            return result.Entity.AsSuccessResult();
        }
        catch (Exception e)
        {
            return e.Message.AsFailureResult<T>();
        }
    }

    public Task<Result> DeleteBy(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = table.Where(expression);
            table.RemoveRange(entities);
            return Task.FromResult(new Result { IsSuccess = true });
        }
        catch (Exception e)
        {
            return Task.FromResult(new Result { IsSuccess = false, Error = new[] { e.Message } });
        }
    }
}