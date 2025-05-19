namespace SchoolManagementSystem.Api.Repositories.AssignmentResults;

using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Repositories.Generic;


public class AssignmentResultsRepository: GenericRepository<AssignmentResult>, IAssignmentResultsRepository
{
    private readonly ApplicationDbContext _context;
    public AssignmentResultsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}