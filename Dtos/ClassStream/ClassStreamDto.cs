using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.ClassStream;

public class ClassStreamDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
}