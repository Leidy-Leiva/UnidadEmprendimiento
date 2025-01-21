using Api.UnidadEmprendimiento.Data.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Api.UnidadEmprendimiento.Data 
{
    public static class ServiceExtensions
 {
     public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
     {
         services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(
                 configuration.GetConnectionString("ConnectionDB"),
                 sqlOptions =>
                 {
                     sqlOptions.EnableRetryOnFailure();
                 }
             ));
     }

 }
}