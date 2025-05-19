
namespace SchoolManagementSystem.Api.Models;

public class CurriculumLevel : BaseModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid CurriculumId { get; set; }
    public Curriculum? Curriculum { get; set; }
}
