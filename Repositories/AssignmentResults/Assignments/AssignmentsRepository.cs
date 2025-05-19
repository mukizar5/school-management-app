namespace SchoolManagementSystem.Api.Repositories.Assignments;

using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;

public class AssignmentsRepository: GenericRepository<Assignment>, IAssignmentsRepository
{
    private readonly ApplicationDbContext _context;
    public AssignmentsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}