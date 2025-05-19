namespace SchoolManagementSystem.Api.Dtos.CurriculumLevelClass;

public class CurriculumLevelClassDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public NameIdPair CurriculumLevel { get; set; }
}
