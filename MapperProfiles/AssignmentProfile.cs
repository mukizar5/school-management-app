using AutoMapper;
using SchoolManagementSystem.Api.Dtos.Assignment;
using SchoolManagementSystem.Api.Models;
namespace SchoolManagementSystem.Api.MapperProfiles;

public class AssignmentProfile: Profile
{
    public AssignmentProfile()
    {
        CreateMap<CreateAssignmentDto, Assignment>();
        CreateMap<UpdateAssignmentDto, Assignment>();
        CreateMap<Assignment, AssignmentDto>();
    }
}