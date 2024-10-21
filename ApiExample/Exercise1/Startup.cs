using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Exercise1.Context;
using Exercise1.Services;

namespace Exercise1
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método se usa para agregar los servicios al contenedor de dependencias
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configurar DbContext con SQLite
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // Registrar el servicio ProductService
            services.AddScoped<ProductService>();

            // Habilitar Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // Este método se usa para configurar el middleware en la tubería de solicitud
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
