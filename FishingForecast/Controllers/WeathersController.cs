using FishingForecast.Models;
using System;
using System.Linq;

namespace FishingForecast.Controllers
{
    public class WeatherController
    {
        private readonly Weather[] weathers;
        private readonly FishingStateRangesCalculator rangesCalculator;
        public WeatherController(Weather[] weathers, FishingStateRangesCalculator rangesCalculator)
        {
            this.weathers = weathers;
            this.rangesCalculator = rangesCalculator;
        }
        public FishingResult CalculateResults()
        {
            int averagePressure = (int)Math.Round(weathers.Average(w => w.Pressure));
            int averageTemperature = (int)Math.Round(weathers.Average(w => w.Temperature));

            Criterion1Results tempC1Result = AnalyseCriterion1(averagePressure);

            Criterion2Results tempC2Result = AnalyseCriterion2(weathers);

            Criterion3Results tempC3Result = AnalyseCriterion3(averageTemperature);

            return new FishingResult(tempC1Result, tempC2Result, tempC3Result);
        }
        private Criterion3Results AnalyseCriterion3(int averageTemperature)
        {
            Criterion3Results tempC3Result = 0;

            if (FishingStateRangesCalculator.tLowActivity.IsRange(averageTemperature))
                tempC3Result = Criterion3Results.LowActivity;
            else if (FishingStateRangesCalculator.tOptimalConditions.IsRange(averageTemperature))
                tempC3Result = Criterion3Results.OptimalConditions;
            else if (FishingStateRangesCalculator.tNotOptimalConditions.IsRange(averageTemperature))
                tempC3Result = Criterion3Results.NotOptimalConditions;
            else
                tempC3Result = Criterion3Results.CriticalConditions;

            return tempC3Result;

        }
        private Criterion2Results AnalyseCriterion2(Weather[] weathers)
        {
            Criterion2Results tempC2Result = 0;
            for (int i = 0; i < weathers.Length - 1; i++)
            {
                double a = weathers[i].Pressure;
                double b = weathers[i + 1].Pressure;
                double res = Math.Abs(a - b) / a * 100;

                if (res > 5)
                {
                    tempC2Result = Criterion2Results.FivePercentDifference;
                    break;
                }
            }
            if (tempC2Result == 0)
                tempC2Result = Criterion2Results.Normal;

            return tempC2Result;
        }
        private Criterion1Results AnalyseCriterion1(int averagePressure)
        {
            Criterion1Results tempC1Result = 0;

            if (rangesCalculator.pLCriticalConditions.IsRange(averagePressure)
             || rangesCalculator.pHCriticalConditions.IsRange(averagePressure))
                tempC1Result = Criterion1Results.CriticalConditions;
            else if (rangesCalculator.pPredatorsThanHerbivores.IsRange(averagePressure))
                tempC1Result = Criterion1Results.PredatorsThanHerbivores;
            else if (rangesCalculator.pNorm.IsRange(averagePressure))
                tempC1Result = Criterion1Results.NormalHerbivoresAndPredators;
            else if (rangesCalculator.pHerbivoresThanPredators.IsRange(averagePressure))
                tempC1Result = Criterion1Results.HerbivoresThanPredators;

            return tempC1Result;
        }

    }
}
