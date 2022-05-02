using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoteFushion.Application.Common.Interfaces;
using VoteFushion.Infrastructure.Persistence;
using VoteFushion.Infrastructure.Services;

namespace VoteFushion.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<VoteFushionDbContext>(options =>
                options.UseInMemoryDatabase("VoteFushionDb"));
        }
        else
        {
            services.AddDbContext<VoteFushionDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(VoteFushionDbContext).Assembly.FullName)));
        }

        services.AddScoped<IVoteFushionDbContext>(provider => provider.GetRequiredService<VoteFushionDbContext>());

        services.AddScoped<IDomainEventService, DomainEventService>();


        services.AddTransient<IDateTime, DateTimeService>();


        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
