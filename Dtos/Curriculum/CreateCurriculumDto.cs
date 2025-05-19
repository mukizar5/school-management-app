using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Curriculum;

public class CreateCurriculumDto
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public Guid CurriculumGoverningBodyId { get; set; }
}
