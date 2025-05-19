using AutoMapper;
using SchoolManagementSystem.Api.Dtos.AssignmentResult;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class AssignmentResultProfile: Profile
{
    public AssignmentResultProfile()
    {
        CreateMap<AssignmentResult, AssignmentResultDto>();
        CreateMap<CreateAssignmentResultDto, AssignmentResult>();
        CreateMap<UpdateAssignmentResultDto, AssignmentResult>();
    }
}