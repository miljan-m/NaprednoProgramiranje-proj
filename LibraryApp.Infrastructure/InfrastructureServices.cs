using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Infrastructure.Persistance;
using LibraryApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace LibraryApp.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddDbContext<LibraryDBContext>(options =>
        {
            string ConnectionString = configuration.GetConnectionString("DefaultConnection")!;
            options.UseSqlServer(ConnectionString);
        });
        return services;
    }
}