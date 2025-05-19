using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Lesson;

public class UpdateLessonDto: CreateLessonDto
{
    [Required]
    public Guid Id { get; set; }
}