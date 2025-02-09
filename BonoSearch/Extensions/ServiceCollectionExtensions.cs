using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BonoSearch.Interfaces;
using BonoSearch.Repositories;
using BonoSearch.Data;
using BonoSearch.Services;

namespace BonoSearch.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMovieServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<ISearchService, SearchService>();
        services.AddNpgsql<MovieDbContext>(configuration.GetConnectionString("DefaultConnection"));
        
        return services;
    }
} 