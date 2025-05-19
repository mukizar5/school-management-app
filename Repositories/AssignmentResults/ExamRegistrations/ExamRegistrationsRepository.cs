using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Repositories.Generic;

namespace SchoolManagementSystem.Api.Repositories.ExamRegistrations;

public class ExamRegistrationRepository : GenericRepository<ExamRegistration>, IExamRegistrationRepository
{
    private readonly ApplicationDbContext _context;

    public ExamRegistrationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}


