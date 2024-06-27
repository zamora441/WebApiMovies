using Microsoft.EntityFrameworkCore;

namespace WebApiMovies.Data.Configuration
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using(var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    appContext.Database.Migrate();
                }
            }

            return host;
        }
    }
}
