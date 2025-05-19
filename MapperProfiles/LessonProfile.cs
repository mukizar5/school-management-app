using AutoMapper;
using SchoolManagementSystem.Api.Dtos.Lesson;
using SchoolManagementSystem.Api.Models;
namespace SchoolManagementSystem.Api.MapperProfiles;

public class LessonProfile: Profile
{
    public LessonProfile()
    {
        CreateMap<CreateLessonDto, Lesson>();
        CreateMap<UpdateLessonDto, Lesson>();
        CreateMap<Lesson, LessonDto>();
    }
}