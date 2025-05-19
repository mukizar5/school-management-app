using AutoMapper;
using SchoolManagementSystem.Api.Dtos;
using SchoolManagementSystem.Api.Dtos.Student;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class StudentProfile: Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.Guardian, opt => opt.MapFrom(src => new NameIdPair { Id = src.Guardian.Id, Name = src.Guardian.FirstName + " " + src.Guardian.LastName }))
            .ForMember(dest => dest.CurriculumLevelClass, opt => opt.MapFrom(src => src.CurriculumLevelClass.Name ?? ""));
        CreateMap<CreateStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>();
    }
}

