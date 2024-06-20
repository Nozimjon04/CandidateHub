using CandidateHub.Service.Mappers;
using CandidateHub.Data.IRepositories;
using CandidateHub.Data.Repositories;
using CandidateHub.Service.Interfaces;
using CandidateHub.Service.Services;
using CandidateHub.Api.Helpers;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CandidateHub.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<ICandidateService, CandidateService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new ConfigureApiUrlName()));
        });

    }


    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

        });
    }
}
