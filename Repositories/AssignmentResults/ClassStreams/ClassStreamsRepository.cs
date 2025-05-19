using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;

namespace SchoolManagementSystem.Api.Repositories.ClassStreams;

public class ClassStreamsRepository: GenericRepository<ClassStream>, IClassStreamsRepository
{
    private readonly ApplicationDbContext _context;
    public ClassStreamsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}

