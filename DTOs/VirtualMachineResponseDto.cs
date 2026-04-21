using System.ComponentModel.DataAnnotations;

namespace CloudResourceManagementSystem.DTOs
{
    public class VirtualMachineResponseDto
    {
        public Guid Id { get; set; }
        public string ResourceName { get; set; }
        public string Region { get; set; }
        public decimal BaseHourlyRate { get; set; }
        public bool Premium { get; set; }

        public string name { get; set; }
        public int CpuCores { get; set; }
        public int RamMemoryGb { get; set; }
    }
}
