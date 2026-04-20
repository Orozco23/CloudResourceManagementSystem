using CloudResourceManagementSystem.Data;
using CloudResourceManagementSystem.DTOs;
using CloudResourceManagementSystem.Interfaces;
using CloudResourceManagementSystem.Models;

namespace CloudResourceManagementSystem.Services
{
    public class ManagedDatabaseService : IMonthlyBillable, IManagedDatabaseService
    {
        private readonly AppDbContext _context;

        public ManagedDatabaseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(ManagedDatabaseRequestDto dto)
        {
            //Create Managed Dabatase Resource
            var mdb = new ManagedDatabase
            {
                ResourceName = dto.ResourceName,
                Region = dto.Region,
                BaseHourlyRate = dto.BaseHourlyRate,
                Premium = dto.Premium.ToString().ToUpper() == "PREMIUM" ? true : false,
                name = dto.name,
                DatabaseEngine = dto.DatabaseEngine,
                StorageCapacityGb = dto.StorageCapacityGb
            };

            _context.ManagedDatabases.Add(mdb);
            await _context.SaveChangesAsync();
        }
        public decimal CalculateEstimatedMonthlyCost(int activeHours)
        {

            throw new NotImplementedException();
        }
    }
}
