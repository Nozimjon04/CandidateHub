using CandidateHub.Data.IRepositories;
using CandidateHub.Data.Repositories;

namespace CandidateHub.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    }
}
