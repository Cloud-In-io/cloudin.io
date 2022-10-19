using CloudIn.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudIn.Core.WebApi.Common.Extensions;

public static class DatabaseExtension
{
    public static async Task<WebApplication> ApplyMigrationsAsync<TDbContext>(
        this WebApplication app
    ) where TDbContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var dbContext = await scope.ServiceProvider
            .GetRequiredService<IDbContextFactory<TDbContext>>()
            .CreateDbContextAsync();

        try
        {
            Console.WriteLine("Migrating...");
            await dbContext.Database.MigrateAsync();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Migration fail: {ex.Message}");
            throw;
        }
        finally
        {
            await dbContext.DisposeAsync();
        }

        return app;
    }

    public static IServiceCollection AddWithPooledDbContext<TService, TImplementation>(
        this IServiceCollection services
    )
        where TService : class
        where TImplementation : class, TService
    {
        return services.AddTransient<TService>(
            implementationFactory: (provider) =>
            {
                var dbContext = provider
                    .GetRequiredService<IDbContextFactory<DataContext>>()
                    .CreateDbContext();

                return ActivatorUtilities.CreateInstance<TImplementation>(provider, dbContext);
            }
        );
    }
}
