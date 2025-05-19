using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Subject;

public class UpdateSubjectDto : CreateSubjectDto
{
    [Required]
    public Guid Id { get; set; }
}
