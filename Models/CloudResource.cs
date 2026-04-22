namespace CloudResourceManagementSystem.Models
{
    public abstract class CloudResource
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ResourceName { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public decimal BaseHourlyRate { get; set; } = decimal.Zero;
        public bool Premium { get; set; } = false;
    }
}
