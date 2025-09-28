using LSports.DataMapping.Abstractions.Interfaces;
using LSports.DataMapping.Services.Data;
using LSports.DataMapping.Services.Repository;
using LSports.DataMapping.Services.Services;
using LSports.DataMapping.WebApi.Middlewares;
using LSports.Hosting.Http.Core.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LSports.DataMapping.WebApi;

public class Startup : DefaultWebApiStartup
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        // Add Entity Framework
        services.AddDbContext<DataMappingDbContext>(options =>
        {
             var connectionString = Configuration.GetConnectionString("Data");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
        });

        // HTTP Client configuration
        services.AddHttpClient();

        // Add repositories and services
        services.AddScoped<IPeriodMappingRepository, PeriodMappingRepository>();
        services.AddScoped<IPeriodMappingService, PeriodMappingService>();

        // Add CORS
        services.AddCors(options => options.AddPolicy("Default",
            policyBuilder => { policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }));

        services.AddSwaggerGen();
    }

    public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseCors("Default");
        app.UseMiddleware<ExceptionMiddleware>();

        base.Configure(app, env);

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "LSports Data Mapping API v1");
            options.RoutePrefix = string.Empty;
        });

        // Ensure database is created
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DataMappingDbContext>();
            context.Database.EnsureCreated();
        }
    }

    public Startup(IConfiguration configuration) : base(configuration)
    {
    }
}
