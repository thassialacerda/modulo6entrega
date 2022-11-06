using Agencia.Context;
using Agencia.Services;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace AgenciaWebApi
{
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connetionString = Configuration.GetConnectionString("ConexaoMySql");
                options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
            });
            //options.UseMySql(Configuration.GetConnectionString("ConexaoMySql")));

            services.AddScoped<IClienteService, ClientesService>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:3000");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
        }
    }

    public interface IStartup
    {
        IConfiguration Configuration { get; }

        void Configure(WebApplication app, IWebHostEnvironment environment);

        void ConfigureServices(IServiceCollection services);

    }

    public static class StartupExtensions
    {
        public static WebApplicationBuilder UseStartup<TStatup>(this WebApplicationBuilder WebAppBuilder) where TStatup : IStartup
        {
            var startup = Activator.CreateInstance(typeof(TStatup), WebAppBuilder.Configuration) as IStartup;
            if (startup == null) throw new ArgumentException("Classe Startup.cs inválida");

            startup.ConfigureServices(WebAppBuilder.Services);

            var app = WebAppBuilder.Build();

            startup.Configure(app, app.Environment);

            app.Run();

            return WebAppBuilder;
        }
    }
}