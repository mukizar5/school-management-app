using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.LessonAttendance;

public class CreateLessonAttendanceDto
{
    [Required]
    public Guid LessonId { get; set; }
    [Required]
    public Guid StudentId { get; set; }
    [Required]
    public bool IsPresent { get; set; }
}
