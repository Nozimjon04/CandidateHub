using CandidateHub.Domain.Entities;
using CandidateHub.Service.DTOs;

namespace CandidateHub.Service.Interfaces;

public interface ICandidateService
{
    public Task<Candidate> AddAsync(CandidateForCreationDto dto, CancellationToken cancellationToken = default);
}
