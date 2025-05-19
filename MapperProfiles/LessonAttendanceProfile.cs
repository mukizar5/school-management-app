using AutoMapper;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Dtos.LessonAttendance;

namespace SchoolManagementSystem.Api.MapperProfiles;

public class LessonAttendanceProfile : Profile
{
    public LessonAttendanceProfile()
    {
        CreateMap<LessonAttendance, LessonAttendanceDto>();
        CreateMap<CreateLessonAttendanceDto, LessonAttendance>();
        CreateMap<UpdateLessonAttendanceDto, LessonAttendance>();
    }
}
