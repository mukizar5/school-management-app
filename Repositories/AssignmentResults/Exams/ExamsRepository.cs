using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.Exams;

public class ExamRepository : GenericRepository<Exam>, IExamRepository
{
    private readonly ApplicationDbContext _context;

    public ExamRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IQueryable<Exam>> SearchAsync(SearchExams searchObject)
    {
        {
        IQueryable<Exam> query = _context.Exams
            .Where(q => !q.IsDeleted)
            .Include(q => q.Subject);

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
