
namespace SchoolManagementSystem.Api.Models;

public class CurriculumLevelClass : BaseModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid CurriculumLevelId { get; set; }
    public CurriculumLevel? CurriculumLevel { get; set; }
    public ICollection<ClassTeacher>? Teachers { get; set; }
}
