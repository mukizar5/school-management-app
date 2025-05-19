using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.ExamResult;

public record ExamResultDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid StudentId { get; set; }
    public int Results { get; set; }
    [Required]
    public Guid ExamId { get; set; }
    
    
}
