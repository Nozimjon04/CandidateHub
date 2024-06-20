using CandidateHub.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CandidateHub.Api.Extensions;

public static class DatabaseExtensions
{
    /// <summary>
    /// Automatically updated database based on latest migration
    /// </summary>
    /// <param name="app"></param>
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        db.Database.Migrate();
    }

}
