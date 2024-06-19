using CandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CandidateHub.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
    {
        
    }
    public DbSet<Candidate> Candidates { get; set; }
}
