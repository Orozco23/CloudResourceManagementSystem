namespace CloudResourceManagementSystem.Models
{
    public class ManagedDatabase: CloudResource
    {
        public string name { get; set; } = string.Empty;
        public string DatabaseEngine { get; set; } = string.Empty;
        public int StorageCapacityGb { get; set; } = 0;
    }
}
