using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Exam;

public record ExamDto
{
    [Required]
    public Guid Id { get; set; }
  
    public string ExamType { get; set; }
    public string DocumentName { get; set; }
    public string DocumentType { get; set; }
    public long DocumentSize { get; set; }

    public NameIdPair Subject { get; set; }
    
}
