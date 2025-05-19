using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.Curriculums;

public interface ICurriculumsRepository:IGenericRepository<Curriculum>
{
    Task<IQueryable<Curriculum>> SearchAsync(SearchCurriculums searchObject);
}
