using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Enums;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.Students;

public class StudentsRepository : GenericRepository<Student>, IStudentsRepository
{
    private readonly ApplicationDbContext _context;

    public StudentsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IQueryable<Student>> SearchAsync(SearchStudents searchObject)
    {
        IQueryable<Student> query = _context.Students.Where(q => !q.IsDeleted)
                                        .Include(q => q.Guardian)
                                        .Include(q => q.CurriculumLevelClass);

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

        if (searchObject.classIds != null && searchObject.classIds.Any())
        {
            query = query.Where(q => q.CurriculumLevelClassId != null && searchObject.classIds.Contains(q.CurriculumLevelClassId.Value));
        }

        return query
                    .OrderByDescending(q => q.DateUpdated)
                    .ThenByDescending(q => q.DateCreated)
                    .AsNoTracking();
    }

    public async Task<int> CountByGenderAsync(Gender gender)
    {
        return await _context.Students.CountAsync(e => !e.IsDeleted && e.Gender == gender);
    }
}