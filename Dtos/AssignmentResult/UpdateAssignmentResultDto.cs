using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.AssignmentResult;

public class UpdateAssignmentResultDto: CreateAssignmentResultDto
{
    [Required]
    public Guid Id { get; set; }
}