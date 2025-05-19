using AutoMapper;
using SchoolManagementSystem.Api.Dtos.Exam;
using SchoolManagementSystem.Api.Models;
namespace SchoolManagementSystem.Api.MapperProfiles;

public class ExamProfile: Profile
{
    public ExamProfile()
    {
        
        CreateMap<CreateExamDto, Exam>();
        CreateMap<UpdateExamDto, Exam>();
        CreateMap<Exam, ExamDto>();
    }
}

