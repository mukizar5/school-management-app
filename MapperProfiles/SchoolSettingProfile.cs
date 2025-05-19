using AutoMapper;
using SchoolManagementSystem.Api.Dtos.SchoolSetting;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class SchoolSettingProfile : Profile
{
    public SchoolSettingProfile()
    {
        CreateMap<SchoolSetting, SchoolSettingDto>();
        CreateMap<CreateSchoolSettingDto, SchoolSetting>();
        CreateMap<UpdateSchoolSettingDto, SchoolSetting>();
    }
}