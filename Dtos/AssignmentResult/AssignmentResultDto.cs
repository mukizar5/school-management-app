namespace SchoolManagementSystem.Api.Dtos.AssignmentResult;

public class AssignmentResultDto
{
    public Guid Id { get; set; }
    public string Result { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public Guid AssignmentId { get; set; }
    public Guid StudentId { get; set; }
}