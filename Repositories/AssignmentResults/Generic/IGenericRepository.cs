using SchoolManagementSystem.Api.Helpers;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.Repositories.Generic;

public interface IGenericRepository<T> where T :  BaseModel
{
    Task<ICollection<T>> GetAllAsync();
    
    Task<T?> GetByIdAsync(Guid id);
    
    Task<T> InsertAsync(T entity);
    
    Task<T> UpdateAsync(T entity);
    
    Task DeleteAsync(Guid id);
    Task DeleteRangeAsync(List<Guid> ids);
    Task<int> CountAsync();
}