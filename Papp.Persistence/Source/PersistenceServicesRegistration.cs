using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PappDbContext>(options => options.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection")
        ));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
