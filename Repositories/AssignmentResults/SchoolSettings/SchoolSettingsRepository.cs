using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;

namespace SchoolManagementSystem.Api.Repositories.SchoolSettings;

public class SchoolSettingsRepository : GenericRepository<SchoolSetting>, ISchoolSettingsRepository
{
    private readonly ApplicationDbContext _context;
    public SchoolSettingsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}