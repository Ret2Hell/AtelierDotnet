using Microsoft.EntityFrameworkCore;
using TP2.Interfaces;

namespace TP2.Repositories;

public class GenericRepository<T>(DbContext context) : IGenericRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IEnumerable<T?>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T? entity)
    {
        if (entity != null) await _dbSet.AddAsync(entity);
    }

    public void Update(T? entity)
    {
        if (entity != null) _dbSet.Update(entity);
    }

    public void Delete(T? entity)
    {
        if (entity != null) _dbSet.Remove(entity);
    }
}