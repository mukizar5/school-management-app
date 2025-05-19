using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.ExamResult;

public record UpdateExamResultDto:CreateExamResultDto
{
    [Required]
    public Guid Id { get; set; }
}