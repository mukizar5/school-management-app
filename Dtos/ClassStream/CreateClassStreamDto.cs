using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.ClassStream;

public class CreateClassStreamDto
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
}