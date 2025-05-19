using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Student
{
    public record StudentDto
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }      // Date of Birth
        public string Gender { get; set; }             // Gender (Enum)
        public DateTime EnrollmentDate { get; set; }   // Date of Enrollment
        public string? Status { get; set; }
        public DateTime? ExitDate { get; set; }
        public NameIdPair? Guardian { get; set; }
        public string? GuardianRelationship { get; set; }
        public int MaleStudents { get; set; }
        public int FemaleStudents { get; set; }
        public string? CurriculumLevelClass { get; set; }
    }
}