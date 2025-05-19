using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Data;

namespace SchoolManagementSystem.Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task UpdateDatabaseAsync(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            if (context != null)
            {
                await context.Database.MigrateAsync();
            }
        }
    }
}