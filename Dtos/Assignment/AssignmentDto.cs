using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Assignment;

public class AssignmentDto
{
    public Guid Id { get; set; }
    public string DocumentName { get; set; }
    public string DocumentType { get; set; }
    public int DocumentSize { get; set; }
    public Guid TeacherId { get; set; }
    public Guid SubjectId { get; set; }
}