using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Repositories.Generic;

namespace SchoolManagementSystem.Api.Repositories.CurriculumGoverningBodies;

public class CurriculumGoverningBodyRepository:GenericRepository<CurriculumGoverningBody>,ICurriculumGoverningBodyRepository 
{
    private readonly ApplicationDbContext _context;

    public CurriculumGoverningBodyRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
