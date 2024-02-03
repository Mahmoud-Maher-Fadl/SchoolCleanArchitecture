namespace Domain.common;

public interface IBaseAdminRepository<T> : IBaseRepository<T> where T : BaseEntity
{
}