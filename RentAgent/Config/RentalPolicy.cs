namespace RentAgent.Config;

public static class RentalPolicy
{
    public const int DefaultRentalDays = 14;
    public const decimal PenaltyPerDay = 5.00m;
    public const decimal MaxPenalty = 200.00m;
    
    public static decimal CalculatePenalty(int overdueDays)
    {
        if (overdueDays <= 0) return 0m;

        var penalty = overdueDays * PenaltyPerDay;
        return Math.Min(penalty, MaxPenalty);
    }
}