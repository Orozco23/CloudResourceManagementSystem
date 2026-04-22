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
        private readonly IResourcesService _resourcesService;

        public ResourceController(IManagedDatabaseService managedDatabaseService, IVirtualMachineService virtualMachineService, IResourcesService resourcesService)
        {
            _managedDatabaseService = managedDatabaseService;
            _virtualMachineService = virtualMachineService;
            _resourcesService = resourcesService;
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
            var result = await _resourcesService.GetAll();
            return Ok(result);
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPagination([FromQuery] int page = 1, [FromQuery] int limit = 5)
        {
            var offset = (page - 1) * limit; 
            var result = await _resourcesService.GetAll();
            result = result.Skip(offset).Take(limit).ToList();
            return Ok(result);
        }

        [HttpGet("sortBy")]
        public async Task<IActionResult> GetSortBy([FromQuery] string sortBy = "name")
        {
            var result = await _resourcesService.Sort(sortBy);
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter([FromQuery] string property = "name", [FromQuery] string input = "name")
        {
            var result = await _resourcesService.Filter(property, input);
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
