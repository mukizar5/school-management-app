namespace SchoolManagementSystem.Api.Models;

public class Assignment : BaseModel
{
    public string DocumentName { get; set; }
    public string DocumentType { get; set; }
    public int DocumentSize { get; set; }
    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
    public List<AssignmentResult>? AssignmentResults { get; set; }
}