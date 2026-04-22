using CloudResourceManagementSystem.DTOs;
using CloudResourceManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace CloudResourceManagementSystem.Services
{
    public class ResourceService : IResourcesService
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
            List<ResourceResponseDto> resources = new List<ResourceResponseDto>();
            foreach (var virtualMachine in virtualMachines)
            {
                resources.Add(new ResourceResponseDto
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
                resources.Add(new ResourceResponseDto
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
            return resources;
        }

        public async Task<List<ResourceResponseDto>> Sort(string sortBy)
        {
            var resources = await GetAll();
            if (string.IsNullOrEmpty(sortBy))
                return resources;
            bool desc = sortBy.StartsWith("-");
            string propertyName = desc ? sortBy.Substring(1) : sortBy;
            var propertyInfo = typeof(ResourceResponseDto).GetProperty(propertyName,
                System.Reflection.BindingFlags.IgnoreCase |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance);
            if (propertyInfo == null)
                throw new Exception($"{propertyName} is not a valid property to sort by.");

            resources.Sort((a, b) =>
            {
                var valueA = propertyInfo.GetValue(a);
                var valueB = propertyInfo.GetValue(b);
                int comparisonResult = Comparer<object>.Default.Compare(valueA, valueB);
                return desc ? -comparisonResult : comparisonResult;
            });

            return resources;

        }

        public async Task<List<ResourceResponseDto>> Filter(string property, string input)
        {
            var resources = await GetAll();
            var filteredResources = resources.Where(x =>
            {
                var propertyInfo = typeof(ResourceResponseDto).GetProperty(property,
                System.Reflection.BindingFlags.IgnoreCase |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance);
                if (propertyInfo == null)
                    throw new Exception($"{property} is not a valid property to filter by.");
                var value = propertyInfo.GetValue(x, null) as string;

                return value != null && value.Contains(input, StringComparison.OrdinalIgnoreCase);
            }).ToList();

            return filteredResources;
        }
    }
}
