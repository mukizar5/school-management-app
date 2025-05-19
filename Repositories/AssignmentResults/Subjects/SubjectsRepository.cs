using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.Subjects;

public class SubjectsRepository : GenericRepository<Subject>, ISubjectsRepository
{
    private readonly ApplicationDbContext _context;

    public SubjectsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IQueryable<Subject>> SearchAsync(SearchSubjects searchObject)
    {
        IQueryable<Subject> query = _context.Subjects
            .Where(q => !q.IsDeleted);

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
