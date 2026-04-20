using CloudResourceManagementSystem.Data;
using CloudResourceManagementSystem.DTOs;
using CloudResourceManagementSystem.Interfaces;
using CloudResourceManagementSystem.Models;

namespace CloudResourceManagementSystem.Services
{
    public class VirtualMachineService: IVirtualMachineService
    {
        private readonly AppDbContext _context;

        public VirtualMachineService(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(VirtualMachineRequestDTO dto)
        {   
            //Create Managed Dabatase Resource
            var vm = new VirtualMachine
            {
                ResourceName = dto.ResourceName,
                Region = dto.Region,
                BaseHourlyRate = dto.BaseHourlyRate,
                Premium = dto.Premium.ToString().ToUpper() == "PREMIUM" ? true : false,
                name = dto.name,
                CpuCores = dto.CpuCores,
                RamMemoryGb = dto.RamMemoryGb
            };

            _context.VirtualMachines.Add(vm);
            await _context.SaveChangesAsync();
        }
    }
}
