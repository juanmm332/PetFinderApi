using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer
{
    public static class Registrations
    {
        public static IServiceCollection AddBusinessLogicLayerServices(this IServiceCollection services)
        {
            //Inyeccion de dependencia de los servicios

            services.AddScoped<IUsuarioManager, UsuarioManager>();

            return services;
        }
    }
}
