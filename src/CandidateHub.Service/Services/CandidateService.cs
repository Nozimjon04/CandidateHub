using AutoMapper;
using CandidateHub.Service.DTOs;
using CandidateHub.Domain.Entities;
using CandidateHub.Service.Interfaces;
using CandidateHub.Data.IRepositories;

namespace CandidateHub.Service.Services;

public class CandidateService : ICandidateService
{
    private readonly IMapper mapper;
    private readonly IRepository<Candidate> candidateRepository;

    public CandidateService(IRepository<Candidate> candidateRepository, IMapper mapper)
    {
        this.mapper = mapper;
        this.candidateRepository = candidateRepository;
    }

    public async Task<Candidate> AddAsync(CandidateForCreationDto dto, CancellationToken cancellationToken = default)
    {

        // Check Candidate existing with Email
        var candidate = await this.candidateRepository.SelectAsync(c => c.Email == dto.Email);
        
        // if user is exist, user updated
        if (candidate is not null)
        {
            this.mapper.Map(dto, candidate);
            candidate.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            candidate = this.mapper.Map<Candidate>(dto);
            candidate.CreatedAt = DateTime.UtcNow;
            candidate = await this.candidateRepository.InsertAsync(candidate);
        }

        await this.candidateRepository.SaveChangeAsync();
        return candidate;
    }

}
