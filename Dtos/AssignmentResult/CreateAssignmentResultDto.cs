using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.AssignmentResult;

public class CreateAssignmentResultDto
{
    [Required]
    public string Result { get; set; }
    public DateTime? SubmissionDate { get; set; }
    [Required]
    public Guid AssignmentId { get; set; }
    [Required]
    public Guid StudentId { get; set; }
}