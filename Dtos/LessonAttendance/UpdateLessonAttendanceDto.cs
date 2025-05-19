using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.LessonAttendance;

public class UpdateLessonAttendanceDto: CreateLessonAttendanceDto
{
    [Required]
    public Guid Id { get; set; }
}
