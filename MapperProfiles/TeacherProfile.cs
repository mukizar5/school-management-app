using AutoMapper;
using SchoolManagementSystem.Api.Dtos;
using SchoolManagementSystem.Api.Dtos.Teacher;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class TeacherProfile: Profile
{
    public TeacherProfile()
    {
        CreateMap<Teacher, TeacherDto>()
        .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.Subjects.Select(s => new NameIdPair { Id = s.SubjectId, Name = s.Subject.Name })))
        .ForMember(dest => dest.Classes, opt => opt.MapFrom(src => src.Classes.Select(c => new NameIdPair { Id = c.ClassId, Name = c.Class.Name })));
        CreateMap<CreateTeacherDto, Teacher>();
        CreateMap<UpdateTeacherDto, Teacher>();
    }
}

