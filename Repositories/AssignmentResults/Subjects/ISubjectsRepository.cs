using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.Subjects;

public interface ISubjectsRepository : IGenericRepository<Subject>
{
    Task<IQueryable<Subject>> SearchAsync(SearchSubjects searchObject);
}
