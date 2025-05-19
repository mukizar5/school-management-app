using AutoMapper;
using SchoolManagementSystem.Api.Dtos.Guardian;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class GuardianProfile: Profile
{
    public GuardianProfile()
    {
        CreateMap<Guardian, GuardianDto>();
        CreateMap<CreateGuardianDto, Guardian>();
        CreateMap<UpdateGuardianDto, Guardian>();
    }
}