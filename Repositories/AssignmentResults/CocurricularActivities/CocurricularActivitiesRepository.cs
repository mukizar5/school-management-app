using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;

namespace SchoolManagementSystem.Api.Repositories.CocurricularActivities;

public class CocurricularActivitiesRepository: GenericRepository<CocurricularActivity>, ICocurricularActivitiesRepository
{
    private readonly ApplicationDbContext _context;
    public CocurricularActivitiesRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}