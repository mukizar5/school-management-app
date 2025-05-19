using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Assignment;

public class UpdateAssignmentDto : CreateAssignmentDto
{
    [Required]
    public Guid Id { get; set; }
}