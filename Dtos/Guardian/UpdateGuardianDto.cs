using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Guardian;

public class UpdateGuardianDto: CreateGuardianDto
{
    [Required]
    public Guid Id { get; set; }
    public string Status { get; set; } = "Active";
}