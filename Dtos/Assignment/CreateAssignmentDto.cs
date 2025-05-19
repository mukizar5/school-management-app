using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Assignment;

public class CreateAssignmentDto
{
    [Required]
    public string DocumentName { get; set; }
    [Required]
    public string DocumentType { get; set; }
    [Required]
    public int DocumentSize { get; set; }
    [Required]
    public Guid TeacherId { get; set; }
    [Required]
    public Guid SubjectId { get; set; }
}