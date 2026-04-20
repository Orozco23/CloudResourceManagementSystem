using CloudResourceManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudResourceManagementSystem.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<ManagedDatabase> ManagedDatabases { get; set; }

        public DbSet<VirtualMachine> VirtualMachines { get; set; }
    }
}
