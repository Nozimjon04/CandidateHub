using AutoMapper;
using CandidateHub.Service.DTOs;
using CandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> AddAsync(CandidateForCreationDto dto, CancellationToken cancellationToken = default)
    {

        // Check Candidate existing with Email
        var candidate = await this.candidateRepository.SelectAll()
            .Where(c => c.Email == dto.Email)
            .FirstOrDefaultAsync();

        // if user is exist, user updated
        if (candidate is not null)
        {
            this.mapper.Map(dto, candidate);
            candidate.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            var mappedCandidate = this.mapper.Map<Candidate>(dto);
            mappedCandidate.CreatedAt = DateTime.UtcNow;
            await this.candidateRepository.InsertAsync(mappedCandidate);
        }

        await this.candidateRepository.SaveChangeAsync();
        return true;
    }
}
