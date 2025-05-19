using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.ExamType;

public record CreateExamTypeDto
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public string Abbreviation { get; set; }
    public string? Description { get; set; }
}