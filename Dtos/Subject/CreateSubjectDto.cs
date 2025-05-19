using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Subject;

public class CreateSubjectDto
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
}
