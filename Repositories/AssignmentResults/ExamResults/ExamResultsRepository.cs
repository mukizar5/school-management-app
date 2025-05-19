using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Repositories.Generic;

namespace SchoolManagementSystem.Api.Repositories.ExamResults;

public class ExamResultRepository : GenericRepository<ExamResult>, IExamResultRepository
{
    private readonly ApplicationDbContext _context;

    public ExamResultRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}


