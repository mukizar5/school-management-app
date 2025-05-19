using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.ExamType;

public record UpdateExamTypeDto:CreateExamTypeDto
{
    [Required]
    public Guid Id { get; set; }
}