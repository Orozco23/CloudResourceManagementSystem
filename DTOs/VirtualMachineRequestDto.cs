using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CloudResourceManagementSystem.DTOs
{
    public class VirtualMachineRequestDTO
    {

        [Required]
        public string ResourceName { get; set; }

        [Required]
        public string Region { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Base Hourly Rate must be greater than 0")]
        public decimal BaseHourlyRate { get; set; }

        [Required]
        public string name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Cpu Cores must be greater than 0")]
        public int CpuCores { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Ram Memory must be greater than 0")]
        public int RamMemoryGb { get; set; }
    }
}
