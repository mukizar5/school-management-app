using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;
using SchoolManagementSystem.Api.Enums;

namespace SchoolManagementSystem.Api.Repositories.Students;

public interface IStudentsRepository:IGenericRepository<Student>
{
    Task<IQueryable<Student>> SearchAsync(SearchStudents searchObject);
    Task<int> CountByGenderAsync(Gender gender);

}
