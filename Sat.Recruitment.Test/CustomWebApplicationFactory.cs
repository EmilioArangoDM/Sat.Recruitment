using Sat.Recruitment.Api;
using Sat.Recruitment.Infrastructure.Persistence;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Sat.Recruitment.Application.Common.Interfaces;

namespace Sat.Recruitment.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public IApplicationDbContext dbService = null;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                configurationBuilder.AddConfiguration(integrationConfig);
            });

            builder.ConfigureServices((builder, services) =>
            {
                services.AddScoped<IApplicationDbContext, ApplicationDbContextCSV>();
            });

            builder.Configure((app) =>
            {
                app.UseDeveloperExceptionPage();

                // Initialise and seed database
                using var scope = app.ApplicationServices.CreateScope();
                dbService = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                var initialiser = new ApplicationDbContextSeederCSV(dbService);
                initialiser.InitialiseAsync().Wait();
                initialiser.SeedAsync().Wait();
            });
        }
    }
}
