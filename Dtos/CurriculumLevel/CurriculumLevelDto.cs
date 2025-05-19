namespace SchoolManagementSystem.Api.Dtos.CurriculumLevel;

public class CurriculumLevelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public NameIdPair Curriculum { get; set; }
}
