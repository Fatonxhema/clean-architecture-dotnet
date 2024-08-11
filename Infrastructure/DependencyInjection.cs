using Application.Core.Abstractions.Common;
using Application.Core.Abstractions.Data;
using EventReminder.Infrastructure.Common;
using Infrastructure.Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey);

            services.AddSingleton(new ConnectionString(connectionString));

            services.AddDbContext<EventReminderDbContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);

            services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<EventReminderDbContext>());

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<EventReminderDbContext>());

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IDateTime, MachineDateTime>();

            return services;
        }
    }
}
