using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.Exams;

public interface IExamRepository:IGenericRepository<Exam>
{
    Task<IQueryable<Exam>> SearchAsync(SearchExams searchObject);
}
