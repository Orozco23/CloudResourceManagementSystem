using System.ComponentModel.DataAnnotations;

namespace CloudResourceManagementSystem.DTOs
{
    public class ManagedDatabaseRequestDto
    {
        [Required]
        public string ResourceName { get; set; }

        [Required]
        public string Region { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Base Hourly Rate must be greater than 0")]
        public decimal BaseHourlyRate { get; set; }
        public string Premium { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string DatabaseEngine { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Storage Capacity must be greater than 0")]
        public int StorageCapacityGb { get; set; }
    }
}
