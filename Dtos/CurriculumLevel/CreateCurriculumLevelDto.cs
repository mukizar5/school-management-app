using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.CurriculumLevel;

public class CreateCurriculumLevelDto
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public Guid CurriculumId { get; set; }
}
