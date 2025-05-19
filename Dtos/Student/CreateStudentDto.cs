using System.ComponentModel.DataAnnotations;
using SchoolManagementSystem.Api.Enums;

namespace SchoolManagementSystem.Api.Dtos.Student
{
    public record CreateStudentDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }          // First Name

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }           // Last Name

        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateOnly DateOfBirth { get; set; }      // Date of Birth

        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("(Male|Female)", ErrorMessage = "Gender must be either 'Male' or 'Female'")]
        public string Gender { get; set; }             // Gender (Enum)
        [Required(ErrorMessage = "EnrollmentDate is required")]
        public DateTime EnrollmentDate { get; set; }   // Date of Enrollment
        [Required]
        public Guid GuardianId { get; set; }
        [Required]
        public string GuardianRelationship { get; set; }
        
        public Guid? CurriculumLevelClassId { get; set; }
    }
}