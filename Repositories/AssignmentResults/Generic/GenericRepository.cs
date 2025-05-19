using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Enums;
using SchoolManagementSystem.Api.Helpers;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.Repositories.Generic;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entities;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<int> CountAsync()
    {
        return await _entities.CountAsync(e => !e.IsDeleted);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _entities.SingleOrDefaultAsync(s => s.Id == id);

        if (entity == null) throw new ArgumentException($"Entity with id {id} not found");
        entity.IsDeleted = true;

        await UpdateAsync(entity);

    }
    public async Task DeleteRangeAsync(List<Guid> ids)
    {
        var entitiesToDelete = await _entities
            .Where(d => ids.Contains(d.Id) && !d.IsDeleted)
            .ToListAsync();

        if (entitiesToDelete.Any())
        {
            foreach (var entity in entitiesToDelete)
            {
                entity.IsDeleted = true; // Soft delete
            }

            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException($"No entities found with ids {string.Join(", ", ids)}");
        }
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        IQueryable<T> query = _entities
                              .Where(q => !q.IsDeleted);

        var source = query
            .OrderByDescending(q => q.DateCreated)
            .ThenBy(q => q.DateUpdated);

        return await source.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var entity = await _entities
            .Where(q => !q.IsDeleted)
            .FirstOrDefaultAsync(s => s.Id == id);

        return entity;
    }

    public async Task<T> InsertAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;

    }

    public async Task<T> UpdateAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.DateUpdated = DateTime.UtcNow;

        _entities.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}