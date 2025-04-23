using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Repository;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Service
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging")); 
                loggingBuilder.AddConsole(); 
                                             
            });

            services.AddScoped<IUsers, UsersRepository>();
            services.AddScoped<IRespuestaR, RespuestaRRepository>();
            services.AddScoped<IReserva, ReservaRepository>();
            services.AddScoped<IReseña, ReseñaRepository>();
            services.AddScoped<IReglaLocal, ReglaLocalRepository>();
            services.AddScoped<INotificacion, NotificacionRepository>();
            services.AddScoped<ILocal, LocalRepository>();
            services.AddScoped<IDisponibilidadLocal, DisponibilidadLocalRepository>();
            services.AddScoped<IComodidadLocal, ComodidadLocalRepository>();
            services.AddScoped<ICategoriaLocal, CategoriaLocalRepository>();
            services.AddScoped<IAddService, AddServiceRepository>();

            return services;
        }
    }
}
