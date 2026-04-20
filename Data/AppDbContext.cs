using Microsoft.EntityFrameworkCore;

namespace CloudResourceManagementSystem.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    }
}
