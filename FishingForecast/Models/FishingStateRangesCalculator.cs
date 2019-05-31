using System;

namespace FishingForecast.Models
{
    public class FishingStateRangesCalculator
    {
        public static readonly Range tLowActivity;
        public static readonly Range tOptimalConditions;
        public static readonly Range tNotOptimalConditions;
        public static readonly Range tCriticalConditions;

        public readonly Range pHCriticalConditions; // Higher limit of critical conditionspHCriticalConditions
        public readonly Range pPredatorsThanHerbivores;
        public readonly Range pNorm;
        public readonly Range pHerbivoresThanPredators;
        public readonly Range pLCriticalConditions; // Lower limit of critical conditions

        static FishingStateRangesCalculator()
        {
            tLowActivity = new Range(Int32.MinValue, 17);
            tOptimalConditions = new Range(18, 25);
            tNotOptimalConditions = new Range(26, 35);
            tCriticalConditions = new Range(36, Int32.MaxValue);
        }

        public FishingStateRangesCalculator(double cityAstergdem)
        {
            double currentStaticPressure = 760 - cityAstergdem / 10.4;
            double firstTempNum = currentStaticPressure * 0.035;
            double secondTempNum = currentStaticPressure * 0.075;

            pHCriticalConditions = new Range(currentStaticPressure + secondTempNum + 0.1,
                                                 Int32.MaxValue);
            pPredatorsThanHerbivores = new Range(currentStaticPressure + firstTempNum + 0.1,
                                                    currentStaticPressure + secondTempNum);
            pNorm = new Range(currentStaticPressure - firstTempNum + 0.1,
                               currentStaticPressure + firstTempNum);
            pHerbivoresThanPredators = new Range(currentStaticPressure - secondTempNum + 0.1,
                                                    currentStaticPressure - firstTempNum);
            pLCriticalConditions = new Range(Int32.MinValue,
                                                 currentStaticPressure - secondTempNum);
        }
    }
}
