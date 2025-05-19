using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Exam;

public record CreateExamDto
{
    [Required]
    public string ExamType { get; set; }
    [Required]
    public Guid SubjectId { get; set; }
    [Required]
    public string DocumentName { get; set; }
    [Required]
    public string DocumentType { get; set; }
    public long DocumentSize { get; set; }
    
    
}