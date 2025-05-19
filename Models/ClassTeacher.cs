namespace SchoolManagementSystem.Api.Models;

public class ClassTeacher
{
    public Guid ClassId {get; set;}
    public CurriculumLevelClass? Class {get; set;}
    public Guid TeacherId {get; set;}
    public Teacher? Teacher {get; set;}
}
