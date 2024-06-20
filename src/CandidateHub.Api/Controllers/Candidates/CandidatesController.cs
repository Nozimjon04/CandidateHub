using CandidateHub.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using CandidateHub.Service.DTOs;
using CandidateHub.Service.Interfaces;
using CandidateHub.Api.Controllers.Commons;

namespace CandidateHub.Api.Controllers.Candidates;

public class CandidatesController : BaseController
{
    private readonly ICandidateService candidateService;

    public CandidatesController(ICandidateService candidateService)
    {
        this.candidateService = candidateService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CandidateForCreationDto dto)
        => Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = await candidateService.AddAsync(dto)
        });
}
