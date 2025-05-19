using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.CurriculumGoverningBody;

public record CreateCurriculumGoverningBodyDto
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public string Abbreviation { get; set; }
    public string? Description { get; set; }
}