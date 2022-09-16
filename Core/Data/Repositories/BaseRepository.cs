using Microsoft.EntityFrameworkCore;
using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;

namespace CloudIn.Core.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : IBaseEntity, new()
{
    protected readonly DataContext _context;

    public BaseRepository(DataContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.AddAsync(entity);
    }

    public IEnumerable<T> GetAll()
    {
        var allEntities = _context.Set<T>();

        return allEntities;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var allEntities = await _context.Set<T>().ToListAsync();

        return allEntities;
    }

    public async Task<T?> GetByIdAsync(Guid Id)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == Id);

        return entity;
    }

    public Task RemoveAsync(T entity)
    {
        _context.Remove(entity);

        return Task.CompletedTask;
    }

    public async Task<bool> SaveChangesAsync()
    {
        var changedRows = await _context.SaveChangesAsync();

        return (changedRows > 0);
    }
}
