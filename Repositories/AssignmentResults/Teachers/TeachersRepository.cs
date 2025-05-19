using SchoolManagementSystem.Api.Data;
using SchoolManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.SearchObjects;
using SchoolManagementSystem.Api.Enums;

namespace SchoolManagementSystem.Api.Repositories.Teachers;

public class TeachersRepository : GenericRepository<Teacher>, ITeachersRepository
{
    private readonly ApplicationDbContext _context;

    public TeachersRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IQueryable<Teacher>> SearchAsync(SearchTeachers searchObject)
    {
        IQueryable<Teacher> query = _context.Teachers.Where(q => !q.IsDeleted)
                                            .Include(q => q.Subjects)
                                            .ThenInclude(q => q.Subject)
                                            .Include(q => q.Classes)
                                            .ThenInclude(q => q.Class);

        if (searchObject.Id != null)
        {
            query = query.Where(q => q.Id == searchObject.Id);
        }
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
        if (searchObject.MarriageStatus != null)
        {
            if (Enum.TryParse<MarriageStatus>(searchObject.MarriageStatus, out var marriageStatusEnum))
            {
                query = query.Where(q => q.MarriageStatus == marriageStatusEnum);
            }
            else
            {
                throw new Exception("Invalid marriage status value provided. Please use 'Married', 'Single', 'Divorced' or 'Widowed'.");
            }
        }

        if (searchObject.ClassIds != null && searchObject.ClassIds.Any())
        {
            query = query.Where(q => q.Classes != null && q.Classes.Any(ct => searchObject.ClassIds.Contains(ct.ClassId)));
        }

        if (searchObject.SubjectIds != null && searchObject.SubjectIds.Any())
        {
            query = query.Where(q => q.Subjects != null && q.Subjects.Any(ts => searchObject.SubjectIds.Contains(ts.SubjectId)));
        }

        if (searchObject.MinSalary.HasValue)
        {
            query = query.Where(q => q.Salary >= searchObject.MinSalary.Value);
        }
        if (searchObject.MaxSalary.HasValue)
        {
            query = query.Where(q => q.Salary <= searchObject.MaxSalary.Value);
        }

        if (searchObject.MinEmploymentYears.HasValue)
        {
            var minEmploymentDate = DateTime.UtcNow.AddYears(-searchObject.MinEmploymentYears.Value);
            query = query.Where(q => q.EmploymentDate <= minEmploymentDate);
        }
        if (searchObject.MaxEmploymentYears.HasValue)
        {
            var maxEmploymentDate = DateTime.UtcNow.AddYears(-searchObject.MaxEmploymentYears.Value);
            query = query.Where(q => q.EmploymentDate >= maxEmploymentDate);
        }


        return query
                    .OrderByDescending(q => q.DateUpdated)
                    .ThenByDescending(q => q.DateCreated);
    }

    public async Task<int> CountByGenderAsync(Gender gender)
    {
        return await _context.Teachers.CountAsync(e => !e.IsDeleted && e.Gender == gender);
    }
}
