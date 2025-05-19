using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.CurriculumGoverningBody;

public record CurriculumGoverningBodyDto
{
    [Required]
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Abbreviation { get; set; }
    public string? Description { get; set; }
}
