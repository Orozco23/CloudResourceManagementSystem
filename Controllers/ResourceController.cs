using CloudResourceManagementSystem.DTOs;
using CloudResourceManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudResourceManagementSystem.Controllers
{
    [ApiController]
    [Route("api/resources")]
    public class ResourceController : ControllerBase
    {
        private readonly IManagedDatabaseService _managedDatabaseService;
        private readonly IVirtualMachineService _virtualMachineService;

        public ResourceController(IManagedDatabaseService managedDatabaseService, IVirtualMachineService virtualMachineService)
        {
            _managedDatabaseService = managedDatabaseService;
            _virtualMachineService = virtualMachineService;
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
    }
}
