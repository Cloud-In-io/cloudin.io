namespace CloudIn.Core.ApplicationDomain.Contracts.Repositories;

public interface IBaseRepository<T>
{
    Task AddAsync(T entity);

    Task<T?> GetByIdAsync(Guid Id);

    Task<IEnumerable<T>> GetAllAsync();

    IEnumerable<T> GetAll();

    Task RemoveAsync(T entity);

    Task<bool> SaveChangesAsync();
}
