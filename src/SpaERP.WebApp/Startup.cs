using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SpaERP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.Logging;

namespace SpaERP.WebApp
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    opt.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                });

            services.AddDbContext<DataDbContext>(options =>
            {
                options.UseNpgsql(
                    // Reads connection string from configuration
                    Configuration
                        .GetConnectionString("Main")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, ILogger<Startup> logger)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataDbContext>();
                try
                {
                    dbContext.Database.OpenConnection();
                    logger.LogInformation("Database connection established successfully.");
                    dbContext.Database.CloseConnection();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to establish database connection.");
                }
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}