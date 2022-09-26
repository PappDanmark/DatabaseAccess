using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration, string? connectionStringEntry = null)
    {
        services.AddDbContext<PappDbContext>(options => options.UseNpgsql(
            configuration.GetConnectionString(connectionStringEntry ?? "DefaultConnection")
        ));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
