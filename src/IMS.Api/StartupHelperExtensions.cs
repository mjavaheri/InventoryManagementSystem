using IMS.Domain.Entities;
using IMS.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace IMS.Api
{
    public static class StartupHelperExtensions
    {
        public static void CreateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<IMSDbContext>();

                    if (context != null)
                    {
                        context.Database.Migrate();
                        SeedData(context);
                    }
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
        }

        private static void SeedData(IMSDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                var users = new List<User>
                {
                    new User("Ali Javaheri"),
                    new User("Mehdi Javaheri"),
                    new User("Amir Abbas Javaheri")
                };
                dbContext.AddRange(users);
                dbContext.SaveChanges();
            }
        }
    }
}
