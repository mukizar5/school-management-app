using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.SearchObjects;
using SchoolManagementSystem.Api.Enums;
using Microsoft.EntityFrameworkCore;
namespace SchoolManagementSystem.Api.Repositories.Guardians;

public class GuardiansRepository: GenericRepository<Guardian>, IGuardiansRepository
{
    private readonly ApplicationDbContext _context;
    public GuardiansRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IQueryable<Guardian>> SearchAsync(SearchGuardians searchObject)
    {
        IQueryable<Guardian> query = _context.Guardians.Where(q => !q.IsDeleted);

        if (!string.IsNullOrWhiteSpace(searchObject.SearchTerm))
        {
            var searchTerm = searchObject.SearchTerm.ToLower();
            query = query.Where(q => q.FirstName.ToLower().Contains(searchTerm) || q.LastName.ToLower().Contains(searchTerm));
        }
        if (searchObject.Gender != null)
        {
            if (Enum.TryParse<Gender>(searchObject.Gender, out var genderEnum))
            {
                query = query.Where(q => q.Gender == genderEnum);
            }
            else
            {
                throw new Exception("Invalid gender value provided. Please use 'Male' or 'Female'.");
            }
        }

        if (searchObject.StudentIds != null && searchObject.StudentIds.Any())
        {
            query = query.Where(q => q.Students != null && q.Students.Any(ts => searchObject.StudentIds.Contains(ts.Id)));
        }


        return query
                    .OrderByDescending(q => q.DateUpdated)
                    .ThenByDescending(q => q.DateCreated)
                    .AsNoTracking();
    }
}