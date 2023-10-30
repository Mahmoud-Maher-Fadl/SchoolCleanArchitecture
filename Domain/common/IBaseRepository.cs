using System.Linq.Expressions;

namespace Domain.common;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<Result<T>> Add(T entity, CancellationToken cancellationToken = default);

    Task<Result<T>> Update(T entity, CancellationToken cancellationToken = default);

    Task<Result<T>> Delete(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includedProperties);
    
    Task<Result<T>> DeleteById(string id, CancellationToken cancellationToken = default);

    Task<Result<T[]>> AddRange(T[] entities, CancellationToken cancellationToken = default);

    Task<Result<T[]>> UpdateRange(T[] entities, CancellationToken cancellationToken = default);

    Task<Result> DeleteBy(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}