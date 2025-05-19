namespace SchoolManagementSystem.Api.Models;

public class ExamRegistration : BaseModel
{
    public Guid StudentId { get; set; }
    public Guid ExamId { get; set; }
    public Guid ExamType { get; set; }
    public bool HasDone { get; set; }
    public string? Comment { get; set; }
    
}

