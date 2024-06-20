using CandidateHub.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CandidateHub.Service.DTOs;

public class CandidateForCreationDto
{
    [Required(ErrorMessage = "Please enter FirstName")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please enter LastName")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "you must enter the email")]
    [EmailAddress(ErrorMessage = "The email address is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Comment is required")]
    public string Comment { get; set; }
    public string GitHubUrl { get; set; }
    public string PhoneNumber { get; set; }
    public string LinkedInUrl { get; set; }
    public ECallTimeInterval CallTimeInterval { get; set; }
}
