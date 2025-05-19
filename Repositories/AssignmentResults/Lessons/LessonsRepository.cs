using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;

namespace SchoolManagementSystem.Api.Repositories.Lessons;

public class LessonsRepository: GenericRepository<Lesson>, ILessonsRepository
{
    private readonly ApplicationDbContext _context;
    public LessonsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}