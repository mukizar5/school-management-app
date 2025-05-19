using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Repositories.Generic;

namespace SchoolManagementSystem.Api.Repositories.ExamTypes;

public class ExamTypeRepository : GenericRepository<ExamType>, IExamTypeRepository
{
    private readonly ApplicationDbContext _context;

    public ExamTypeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
