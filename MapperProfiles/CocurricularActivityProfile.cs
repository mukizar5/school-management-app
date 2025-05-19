using AutoMapper;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Dtos.CocurricularActivity;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class CocurricularActivityProfile: Profile
{
    public CocurricularActivityProfile()
    {
        CreateMap<CocurricularActivity, CocurricularActivityDto>();
        CreateMap<CreateCocurricularActivityDto, CocurricularActivity>();
        CreateMap<UpdateCocurricularActivityDto, CocurricularActivity>();
    }
}