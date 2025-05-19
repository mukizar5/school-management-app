using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.Curriculums;

public class CurriculumsRepository:GenericRepository<Curriculum>,ICurriculumsRepository
{
    private readonly ApplicationDbContext _context;

    public CurriculumsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IQueryable<Curriculum>> SearchAsync(SearchCurriculums searchObject)
    {
        {
            IQueryable<Curriculum> query = _context.Curriculums
            .Where(q => !q.IsDeleted)
            .Include(q => q.CurriculumGoverningBody);


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