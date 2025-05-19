namespace SchoolManagementSystem.Api.Models;

public class CocurricularActivity: BaseModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<StudentCocurricularActivity>? Students { get; set; }
}
