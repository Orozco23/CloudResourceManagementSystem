using System.ComponentModel.DataAnnotations;

namespace CloudResourceManagementSystem.DTOs
{
    public class ManagedDatabaseResponseDto
    {
        public Guid Id { get; set; }
        public string ResourceName { get; set; }
        public string Region { get; set; }
        public decimal BaseHourlyRate { get; set; }
        public bool Premium { get; set; }
        public string name { get; set; }
        public string DatabaseEngine { get; set; }
        public int StorageCapacityGb { get; set; }
    }
}
