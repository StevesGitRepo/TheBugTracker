using Microsoft.EntityFrameworkCore;
using TheBugTracker.Data;

namespace TheBugTracker.Helpers
{
    public class DataHelper
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            //Service: an instance of db context
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();

            //Migration: the programmitic equivalent to Update-database:
            await dbContextSvc.Database.MigrateAsync();
        }
    }
}