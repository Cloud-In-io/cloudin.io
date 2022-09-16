using Microsoft.EntityFrameworkCore;

namespace CloudIn.Core.WebApi.Common.Extensions;

public static class DatabaseExtension
{
    public static async Task<WebApplication> ApplyMigrationsAsync<TDbContext>(
        this WebApplication app
    ) where TDbContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        try
        {
            Console.WriteLine("Migrating...");
            await dbContext.Database.MigrateAsync();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Migration fail: {ex.Message}");
        }

        return app;
    }
}
