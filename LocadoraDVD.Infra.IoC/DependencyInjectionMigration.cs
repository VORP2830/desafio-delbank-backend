using LocadoraDVD.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LocadoraDVD.Infra.IoC;

public static class DependencyInjectionMigration
{
    public static IServiceCollection AddMigrations(this IServiceCollection service)
    {
        using (var serviceProvider = service.BuildServiceProvider())
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MySQLContext>();
            dbContext.Database.Migrate();
        }
        return service;
    }
}