using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.CurriculumLevelClasses;

public interface ICurriculumLevelClassesRepository:IGenericRepository<CurriculumLevelClass>
{
    Task<IQueryable<CurriculumLevelClass>> SearchAsync(SearchCurriculumLevelClasses searchObject);

}
