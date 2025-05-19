using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.ClassStream;

public class UpdateClassStreamDto:CreateClassStreamDto
{
    [Required]
    public Guid Id { get; set; }
}