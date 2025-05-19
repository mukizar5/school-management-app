using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.CurriculumLevels;

public interface ICurriculumLevelsRepository:IGenericRepository<CurriculumLevel>
{
    Task<IQueryable<CurriculumLevel>> SearchAsync(SearchCurriculumLevels searchObject);
}
