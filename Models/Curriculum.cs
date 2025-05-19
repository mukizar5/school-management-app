
namespace SchoolManagementSystem.Api.Models;

public class Curriculum : BaseModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid CurriculumGoverningBodyId { get; set; }
    public CurriculumGoverningBody? CurriculumGoverningBody { get; set; }
}
