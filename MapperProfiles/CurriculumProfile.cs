using AutoMapper;
using SchoolManagementSystem.Api.Dtos.Curriculum;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class CurriculumProfile: Profile
{
    public CurriculumProfile()
    {
        CreateMap<Curriculum, CurriculumDto>();
        CreateMap<CreateCurriculumDto, Curriculum>();
        CreateMap<UpdateCurriculumDto, Curriculum>();
    }
}

