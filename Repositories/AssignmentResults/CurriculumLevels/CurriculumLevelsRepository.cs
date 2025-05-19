using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.CurriculumLevels;

public class CurriculumLevelsRepository:GenericRepository<CurriculumLevel>,ICurriculumLevelsRepository
{
    private readonly ApplicationDbContext _context;

    public CurriculumLevelsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IQueryable<CurriculumLevel>> SearchAsync(SearchCurriculumLevels searchObject)
    {
        {
            IQueryable<CurriculumLevel> query = _context.CurriculumLevels
            .Where(q => !q.IsDeleted)
            .Include(q => q.Curriculum);


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