using Microsoft.Extensions.DependencyInjection;

namespace Code.Kata._9.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKataNineDatabase(this IServiceCollection services)
    {
        services.AddEntityFrameworkInMemoryDatabase();
        services.AddDbContext<ApiDbContext>();

        services.SeedDummyData();
        
        return services;
    }

    private static void SeedDummyData(this IServiceCollection services)
    {
        var dbContext = services.BuildServiceProvider().GetService<ApiDbContext>();
        if (dbContext is null)
        {
            Console.WriteLine("Failed to locate DB Context when attempting to seed data...");
        }
        
        //Seed initial data for demonstration purposes
    }
}