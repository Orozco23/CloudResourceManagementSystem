using CloudResourceManagementSystem.Data;
using CloudResourceManagementSystem.DTOs;
using CloudResourceManagementSystem.Interfaces;
using CloudResourceManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

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
            var managedDatabase = new ManagedDatabase
            {
                ResourceName = dto.ResourceName,
                Region = dto.Region,
                BaseHourlyRate = dto.BaseHourlyRate,
                Premium = dto.name.ToUpper().Contains("PREMIUM") ? true : false,
                name = dto.name,
                DatabaseEngine = dto.DatabaseEngine,
                StorageCapacityGb = dto.StorageCapacityGb
            };

            _context.ManagedDatabases.Add(managedDatabase);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ManagedDatabaseResponseDto>> GetAll()
        {
            var managedDatabases = await _context.ManagedDatabases.ToListAsync();

            return managedDatabases.Select(mdb => new ManagedDatabaseResponseDto
            {
                Id = mdb.Id,
                ResourceName = mdb.ResourceName,
                Region = mdb.Region,
                BaseHourlyRate = mdb.BaseHourlyRate,
                Premium = mdb.Premium,
                name = mdb.name,
                DatabaseEngine = mdb.DatabaseEngine,
                StorageCapacityGb = mdb.StorageCapacityGb
            }).ToList();
        }

        public decimal CalculateEstimatedMonthlyCost(int activeHours, decimal baseHourlyRate, Boolean premium, int attribute, string region)
        {
            decimal storageCost = attribute * 0.1m; // Storage cost per GB, 10 cents per GB
            decimal hourlyRate = baseHourlyRate + storageCost;
            decimal totalCost = activeHours * hourlyRate;
            if (premium) totalCost += 100.00m; // Premium cost
            switch (region.ToUpper())
            {
                case "US-EAST-1":
                    totalCost *= 0.95m; // 5% discount
                    break;
                case "US-EAST-2":
                    totalCost *= 0.97m; // 3% discount
                    break;
                case "US-WEST-1":
                    totalCost *= 1.1m; // 10% increase
                    break;
                case "EU-CENTRAL":
                    totalCost *= 1.15m; // 15% increase
                    break;
                default:
                    totalCost *= 1m; // not change
                    break;
            }
            return totalCost;
        }
    }
}
