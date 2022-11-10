using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Papp.Persistence.Context;
using Papp.Persistence.UnitOfWork;

namespace Papp.Persistence;

public static class Startup
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration, string? connectionStringEntry = null)
    {
        services.AddDbContext<PappDbContext>(options => options.UseNpgsql(
            configuration.GetConnectionString(connectionStringEntry ?? "DefaultConnection")
        ));
        services.AddScoped<IPappDbUnitOfWork, PappDbUnitOfWork>();
        return services;
    }
}
