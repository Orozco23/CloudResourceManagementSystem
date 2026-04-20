using CloudResourceManagementSystem.Data;
using CloudResourceManagementSystem.DTOs;
using CloudResourceManagementSystem.Interfaces;
using CloudResourceManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                Premium = dto.name.ToUpper().Contains("PREMIUM") ? true : false,
                name = dto.name,
                CpuCores = dto.CpuCores,
                RamMemoryGb = dto.RamMemoryGb
            };

            _context.VirtualMachines.Add(vm);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VirtualMachineResponseDto>> GetAll()
        {
            var vms = await _context.VirtualMachines.ToListAsync();

            return vms.Select(vm => new VirtualMachineResponseDto
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
    }
}
