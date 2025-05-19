using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;
using SchoolManagementSystem.Api.Enums;

namespace SchoolManagementSystem.Api.Repositories.Teachers;

public interface ITeachersRepository:IGenericRepository<Teacher>
{
    Task<IQueryable<Teacher>> SearchAsync(SearchTeachers searchObject);
    Task<int> CountByGenderAsync(Gender gender);
}
