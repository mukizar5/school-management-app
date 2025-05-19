using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.CurriculumLevelClasses;

public class CurriculumLevelClassesRepository:GenericRepository<CurriculumLevelClass>,ICurriculumLevelClassesRepository
{
    private readonly ApplicationDbContext _context;

    public CurriculumLevelClassesRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IQueryable<CurriculumLevelClass>> SearchAsync(SearchCurriculumLevelClasses searchObject)
    {
        {
            IQueryable<CurriculumLevelClass> query = _context.CurriculumLevelClasses
            .Where(q => !q.IsDeleted)
            .Include(q => q.CurriculumLevel);


            if (searchObject.Id.HasValue)
            {
                query = query.Where(q => q.Id == searchObject.Id);
            }
            return query
                    .OrderByDescending(d => d.DateUpdated)
                    .ThenByDescending(d => d.DateCreated)
                    .AsNoTracking();
        }
    }
}