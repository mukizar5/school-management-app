using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Lesson;

public class CreateLessonDto
{
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public Guid TeacherId { get; set; }
    [Required]
    public Guid CurriculumLevelClassId { get; set; }
    [Required]
    public Guid SubjectId { get; set; }

}