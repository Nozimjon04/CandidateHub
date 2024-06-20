using CandidateHub.Service.DTOs;

namespace CandidateHub.Service.Interfaces;

public interface ICandidateService
{
    public Task<bool> AddAsync(CandidateForCreationDto dto, CancellationToken cancellationToken = default);
}
