using SchoolManagementSystem.Api.Repositories.Generic;
using SchoolManagementSystem.Api.Services.Dashboard;
using SchoolManagementSystem.Api.Services.Generic;

namespace SchoolManagementSystem.Api.Extensions;

public static class ServiceCollectionExtensions
{
        
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.Scan(scan => scan
                
            .FromAssemblyOf<Program>()
            
            .AddClasses(classes => classes.AssignableTo(typeof(IGenericRepository<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(IGenericService<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        return services;
    }

}

