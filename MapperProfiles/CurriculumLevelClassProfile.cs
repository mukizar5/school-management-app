using AutoMapper;
using SchoolManagementSystem.Api.Dtos.CurriculumLevelClass;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class CurriculumLevelClassProfile: Profile
{
    public CurriculumLevelClassProfile()
    {
        CreateMap<CurriculumLevelClass, CurriculumLevelClassDto>();
        CreateMap<CreateCurriculumLevelClassDto, CurriculumLevelClass>();
        CreateMap<UpdateCurriculumLevelClassDto, CurriculumLevelClass>();
    }
}

