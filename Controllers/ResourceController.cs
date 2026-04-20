using CloudResourceManagementSystem.DTOs;
using CloudResourceManagementSystem.Interfaces;
using CloudResourceManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudResourceManagementSystem.Controllers
{
    [ApiController]
    [Route("api/resources")]
    public class ResourceController : ControllerBase
    {
        private readonly IManagedDatabaseService _managedDatabaseService;
        private readonly IVirtualMachineService _virtualMachineService;
        private readonly IMonthlyBillable _monthlyBillable;

        public ResourceController(IManagedDatabaseService managedDatabaseService, IVirtualMachineService virtualMachineService, IMonthlyBillable monthlyBillable)
        {
            _managedDatabaseService = managedDatabaseService;
            _virtualMachineService = virtualMachineService;
            _monthlyBillable = monthlyBillable;
        }

        [HttpPost("managed-databases")]
        public async Task<IActionResult> CreateManagedDatabase(ManagedDatabaseRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _managedDatabaseService.Create(dto);
            return Ok();
        }

        [HttpPost("virtual-machines")]
        public async Task<IActionResult> CreateVirtualMachine(VirtualMachineRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _virtualMachineService.Create(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var virtualMachines = await _virtualMachineService.GetAll();
            var managedDatabases = await _managedDatabaseService.GetAll();
            List<ResourceResponseDto> result = new List<ResourceResponseDto>();
            foreach (var virtualMachine in virtualMachines) {
                result.Add(new ResourceResponseDto {
                    Id = virtualMachine.Id,
                    ResourceName = virtualMachine.ResourceName,
                    Region = virtualMachine.Region,
                    BaseHourlyRate = virtualMachine.BaseHourlyRate,
                    Premium = virtualMachine.Premium,
                    name = virtualMachine.name,
                    CpuCores = virtualMachine.CpuCores,
                    RamMemoryGb = virtualMachine.RamMemoryGb,
                    MonthlyCost = _monthlyBillable.CalculateEstimatedMonthlyCost(730, virtualMachine.BaseHourlyRate, virtualMachine.Premium, virtualMachine.CpuCores, virtualMachine.Region)
                });
            }
            foreach (var managedDatabase in managedDatabases)
            {
                result.Add(new ResourceResponseDto
                {
                    Id = managedDatabase.Id,
                    ResourceName = managedDatabase.ResourceName,
                    Region = managedDatabase.Region,
                    BaseHourlyRate = managedDatabase.BaseHourlyRate,
                    Premium = managedDatabase.Premium,
                    name = managedDatabase.name,
                    DatabaseEngine = managedDatabase.DatabaseEngine,
                    StorageCapacityGb = managedDatabase.StorageCapacityGb,
                    MonthlyCost = _monthlyBillable.CalculateEstimatedMonthlyCost(730, managedDatabase.BaseHourlyRate, managedDatabase.Premium, managedDatabase.StorageCapacityGb, managedDatabase.Region)
                });
            }
            return Ok(result);
        }

        [HttpGet("virtual-machines")]
        public async Task<IActionResult> GetAllVirtualMachines()
        {
            var virtualMachines = await _virtualMachineService.GetAll();
            return Ok(virtualMachines);
        }

        [HttpGet("managed-databases")]
        public async Task<IActionResult> GetAllManagedDatabases()
        {
            var managedDatabases = await _managedDatabaseService.GetAll();
            return Ok(managedDatabases);
        }
    }
}
