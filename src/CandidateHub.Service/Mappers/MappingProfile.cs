using AutoMapper;
using CandidateHub.Service.DTOs;
using CandidateHub.Domain.Entities;

namespace CandidateHub.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<Candidate, CandidateForCreationDto>().ReverseMap();
    }
}

