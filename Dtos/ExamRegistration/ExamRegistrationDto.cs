using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.ExamRegistration;

public record ExamRegistrationDto
{
    [Required]
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid ExamId { get; set; }
    public Guid ExamType { get; set; }
    public bool HasDone { get; set; }

    public string? Comment { get; set; }
    
    
}
