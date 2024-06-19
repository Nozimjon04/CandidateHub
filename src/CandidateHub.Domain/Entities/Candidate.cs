using CandidateHub.Domain.Enums;
using CandidateHub.Domain.Commons;

namespace CandidateHub.Domain.Entities;

public class Candidate : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Comment { get; set; }
    public string GitHubUrl { get; set; }
    public string PhoneNumber { get; set; }
    public string LinkedInUrl { get; set; }
    public ECallTimeInterval CallTimeInterval { get; set; }
}
