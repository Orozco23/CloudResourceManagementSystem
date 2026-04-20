using CloudResourceManagementSystem.Data;
using CloudResourceManagementSystem.DTOs;
using CloudResourceManagementSystem.Interfaces;
using CloudResourceManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudResourceManagementSystem.Services
{
    public class VirtualMachineService: IVirtualMachineService, IMonthlyBillable
    {
        private readonly AppDbContext _context;

        public VirtualMachineService(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(VirtualMachineRequestDTO dto)
        {   
            //Create Managed Dabatase Resource
            var virtualMachine = new VirtualMachine
            {
                ResourceName = dto.ResourceName,
                Region = dto.Region,
                BaseHourlyRate = dto.BaseHourlyRate,
                Premium = dto.name.ToUpper().Contains("PREMIUM") ? true : false,
                name = dto.name,
                CpuCores = dto.CpuCores,
                RamMemoryGb = dto.RamMemoryGb
            };

            _context.VirtualMachines.Add(virtualMachine);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VirtualMachineResponseDto>> GetAll()
        {
            var virtualMachines = await _context.VirtualMachines.ToListAsync();

            return virtualMachines.Select(vm => new VirtualMachineResponseDto
            {
                Id = vm.Id,
                ResourceName = vm.ResourceName,
                Region = vm.Region,
                BaseHourlyRate = vm.BaseHourlyRate,
                Premium = vm.Premium,
                name = vm.name,
                CpuCores = vm.CpuCores,
                RamMemoryGb = vm.RamMemoryGb
            }).ToList();
        }

        public decimal CalculateEstimatedMonthlyCost(int activeHours, decimal baseHourlyRate, Boolean premium, int attribute, string region)
        {
            int coreCost = attribute * 10; // Storage cost per GB, 10 dolares per core
            decimal hourlyRate = baseHourlyRate + coreCost;
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
