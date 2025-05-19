using AutoMapper;
using SchoolManagementSystem.Api.Dtos.CurriculumLevel;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class CurriculumLevelProfile: Profile
{
    public CurriculumLevelProfile()
    {
        CreateMap<CurriculumLevel, CurriculumLevelDto>();
        CreateMap<CreateCurriculumLevelDto, CurriculumLevel>();
        CreateMap<UpdateCurriculumLevelDto, CurriculumLevel>();
    }
}

