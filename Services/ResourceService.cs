using CloudResourceManagementSystem.DTOs;
using CloudResourceManagementSystem.Interfaces;

namespace CloudResourceManagementSystem.Services
{
    public class ResourceService: IResourcesService
    {
        private readonly IManagedDatabaseService _managedDatabaseService;
        private readonly IVirtualMachineService _virtualMachineService;
        private readonly IMonthlyBillable _monthlyBillable;
        public ResourceService(IManagedDatabaseService managedDatabaseService, IVirtualMachineService virtualMachineService, IMonthlyBillable monthlyBillable)
        {
            _managedDatabaseService = managedDatabaseService;
            _virtualMachineService = virtualMachineService;
            _monthlyBillable = monthlyBillable;
        }

        public async Task<List<ResourceResponseDto>> GetAll()
        {
            var virtualMachines = await _virtualMachineService.GetAll();
            var managedDatabases = await _managedDatabaseService.GetAll();
            List<ResourceResponseDto> result = new List<ResourceResponseDto>();
            foreach (var virtualMachine in virtualMachines)
            {
                result.Add(new ResourceResponseDto
                {
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
            return result;
        }
    }
}
