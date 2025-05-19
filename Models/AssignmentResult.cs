namespace SchoolManagementSystem.Api.Models;

public class AssignmentResult: BaseModel
{
    public string Result { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
}