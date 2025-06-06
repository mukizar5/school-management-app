using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos;

public class BaseProfileDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Gender { get; set; }
    public string? ProfilePicture { get; set; }
    public string? EmailAddress { get; set; } 
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
}