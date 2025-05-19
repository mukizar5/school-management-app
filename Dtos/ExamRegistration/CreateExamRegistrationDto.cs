using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.ExamRegistration;

public record CreateExamRegistrationDto
{
    [Required]
    public Guid StudentId { get; set; }
    [Required]
    public Guid ExamId { get; set; }
    [Required]
    public Guid ExamType { get; set; }
    public bool HasDone { get; set; }

    public string? Comment {get; set;}
    
}