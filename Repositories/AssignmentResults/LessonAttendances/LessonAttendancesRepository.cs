using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.Data;

namespace SchoolManagementSystem.Api.Repositories.LessonAttendances;

public class LessonAttendancesRepository: GenericRepository<LessonAttendance>, ILessonAttendancesRepository
{
    private readonly ApplicationDbContext _context;
    public LessonAttendancesRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}