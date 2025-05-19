namespace SchoolManagementSystem.Api.Dtos.Lesson;

public class LessonDto
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Guid TeacherId { get; set; }
    public Guid CurriculumLevelClassId { get; set; }
    public Guid SubjectId { get; set; }
}
