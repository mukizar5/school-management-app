using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Repositories.Guardians;

public interface IGuardiansRepository: IGenericRepository<Guardian>
{
    Task<IQueryable<Guardian>> SearchAsync(SearchGuardians searchObject);
}