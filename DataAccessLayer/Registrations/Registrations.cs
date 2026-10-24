using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class Registrations
    {
        public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("AQUI_LA_CADENA_DE_CONEXION");
            }, ServiceLifetime.Scoped);

            var context = services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
            //context.Database.EnsureCreated();

            //Inyeccion de dependencia de los servicios
            services.AddSingleton<ITossedCoinStore, TossedCoinStore>();

            return services;
        }
    }
}
