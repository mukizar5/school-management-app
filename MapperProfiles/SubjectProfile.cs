using AutoMapper;
using SchoolManagementSystem.Api.Dtos.Subject;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class SubjectProfile: Profile
{
    public SubjectProfile()
    {
        CreateMap<Subject, SubjectDto>();
        CreateMap<CreateSubjectDto, Subject>();
        CreateMap<UpdateSubjectDto, Subject>();
    }
}

