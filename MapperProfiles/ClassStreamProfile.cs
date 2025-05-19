using AutoMapper;
using SchoolManagementSystem.Api.Dtos.ClassStream;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class ClassStreamProfile: Profile
{
    public ClassStreamProfile()
    {
        CreateMap<ClassStream, ClassStreamDto>();
        CreateMap<CreateClassStreamDto, ClassStream>();
        CreateMap<UpdateClassStreamDto, ClassStream>();
    }
}

