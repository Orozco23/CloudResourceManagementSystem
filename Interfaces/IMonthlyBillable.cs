namespace CloudResourceManagementSystem.Interfaces
{
    public interface IMonthlyBillable
    {
        decimal CalculateEstimatedMonthlyCost(int activeHours); 

    }
}
