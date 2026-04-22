namespace CloudResourceManagementSystem.Models
{
    public class VirtualMachine: CloudResource
    {
        public string name { get; set; } = string.Empty;
        public int CpuCores { get; set; } = 0;
        public int RamMemoryGb { get; set; } = 0;
    }
}
