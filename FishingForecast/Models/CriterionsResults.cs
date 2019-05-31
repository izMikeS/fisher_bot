namespace FishingForecast.Models
{
    public enum Criterion1Results
    {
        CriticalConditions = 1,
        HerbivoresThanPredators,
        NormalHerbivoresAndPredators,
        PredatorsThanHerbivores
    }
    public enum Criterion2Results
    {
        Normal = 1,
        FivePercentDifference
    }
    public enum Criterion3Results
    {
        LowActivity = 1,
        OptimalConditions,
        NotOptimalConditions,
        CriticalConditions
    }
}
