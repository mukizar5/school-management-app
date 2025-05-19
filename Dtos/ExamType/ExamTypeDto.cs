using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.ExamType;

public record ExamTypeDto
{
    [Required]
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Abbreviation { get; set; }
    public string? Description { get; set; }
}
