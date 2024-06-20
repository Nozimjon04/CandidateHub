using Moq;
using AutoMapper;
using System.Linq.Expressions;
using CandidateHub.Service.DTOs;
using CandidateHub.Domain.Enums;
using CandidateHub.Domain.Entities;
using CandidateHub.Service.Services;
using CandidateHub.Data.IRepositories;

namespace CandidateHub.Tests.Services;

public class CandidateServiceTests
{
    private readonly Mock<IMapper> mapperMock;
    private readonly CandidateService candidateService;
    private readonly Mock<IRepository<Candidate>> candidateRepositoryMock;

    public CandidateServiceTests()
    {
        // Mocking IMapper
        mapperMock = new Mock<IMapper>();

        // Mocking IRepository<Candidate>
        candidateRepositoryMock = new Mock<IRepository<Candidate>>();

        candidateService = new CandidateService(candidateRepositoryMock.Object, mapperMock.Object);

    }

    [Fact]
    public async Task AddAsync_ShouldUpdateCandidate_WhenCandidateExists()
    {
        // Arrange
        var candidateDto = new CandidateForCreationDto
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            CallTimeInterval = ECallTimeInterval.Morning
        };

        var candidate = new Candidate()
        {
            Id = 1,
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            CallTimeInterval = ECallTimeInterval.Morning
        };

        candidateRepositoryMock.Setup(repo => repo.SelectAsync(It.IsAny<Expression<Func<Candidate, bool>>>(), default))
                                .ReturnsAsync(candidate);

        mapperMock.Setup(mapper => mapper.Map(It.IsAny<CandidateForCreationDto>(), It.IsAny<Candidate>())).
            Callback((CandidateForCreationDto src, Candidate dest) =>
            {
                dest.FirstName = src.FirstName;
                dest.LastName = src.LastName;
                dest.Email = src.Email;
                dest.CallTimeInterval = src.CallTimeInterval;
            });

        // Act
        var result = await candidateService.AddAsync(candidateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(candidate.Email, result.Email);
        Assert.Equal(candidate.FirstName, result.FirstName);
        Assert.Equal(candidate.LastName, result.LastName);
        Assert.Equal(candidate.CallTimeInterval, result.CallTimeInterval);

        candidateRepositoryMock.Verify(repo => repo.SaveChangeAsync(It.IsAny<CancellationToken>()), Times.Once);
        candidateRepositoryMock.Verify(repo => repo.SelectAsync(It.IsAny<Expression<Func<Candidate, bool>>>(), default), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateCandidate_WhenIsNotAvailable()
    {
        // Arrange
        var candidate = new Candidate
        {
            Id = 1,
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            CallTimeInterval = ECallTimeInterval.EarlyAfternoon
        };

        var candidateDto = new CandidateForCreationDto
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            CallTimeInterval = ECallTimeInterval.EarlyAfternoon // Example enum value
        };


        candidateRepositoryMock.Setup(repo => repo.SelectAsync(It.IsAny<Expression<Func<Candidate, bool>>>(), default))
                                .Returns(Task.FromResult<Candidate>(null));

        candidateRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Candidate>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(candidate);

        mapperMock.Setup(mapper => mapper.Map<Candidate>(candidateDto))
            .Returns(new Candidate()
            {
                FirstName = candidateDto.FirstName,
                LastName = candidateDto.LastName,
                Email = candidateDto.Email,
                CallTimeInterval = candidateDto.CallTimeInterval
            });

        // Act
        var result = await candidateService.AddAsync(candidateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id); // Id should remain the same
        Assert.Equal(candidate.Email, result.Email);
        Assert.Equal(candidate.FirstName, result.FirstName);
        Assert.Equal(candidate.LastName, result.LastName);
        Assert.Equal(candidate.CallTimeInterval, result.CallTimeInterval); // Check enum value

        candidateRepositoryMock.Verify(repo => repo.SaveChangeAsync(It.IsAny<CancellationToken>()), Times.Once);
        candidateRepositoryMock.Verify(repo => repo.InsertAsync(It.IsAny<Candidate>(), It.IsAny<CancellationToken>()), Times.Once);
        candidateRepositoryMock.Verify(repo => repo.SelectAsync(It.IsAny<Expression<Func<Candidate, bool>>>(), default), Times.Once);
    }
}


