using SchoolManagementSystem.Api.Enums;

namespace SchoolManagementSystem.Api.Models
{
    public class BaseProfile : BaseModel

    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string? ProfilePicture { get; set; }
        public string? EmailAddress { get; set; } // Email address of the student/contact
        public string? PhoneNumber { get; set; } // Phone number
        public string? Address { get; set; } // Physical address
        public string? City { get; set; } // City where the student/contact resides
        public string? Country { get; set; } // Country
    }
}