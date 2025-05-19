using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Models;

public class Exam : BaseModel
{
    public string ExamType { get; set; }
    [Required]
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
    public string DocumentName { get; set; }

    public string DocumentType { get; set; }
    public long DocumentSize { get; set; }
    
}

