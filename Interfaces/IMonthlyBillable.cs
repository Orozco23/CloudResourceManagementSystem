namespace CloudResourceManagementSystem.Interfaces
{
    public interface IMonthlyBillable
    {
        decimal CalculateEstimatedMonthlyCost(int activeHours, decimal baseHourlyRate, Boolean premium, int attribute, string region); 

    }
}
