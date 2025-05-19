using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Exam;

public record UpdateExamDto:CreateExamDto
{
    [Required]
    public Guid Id { get; set; }
}