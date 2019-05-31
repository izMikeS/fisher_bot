using System;
namespace FishingForecast.Models
{
    public class Range
    {
        public readonly double rangeStart;
        public readonly double rangeEnd;

        public Range(double rangeStart, double rangeEnd)
        {
            this.rangeStart = rangeStart;
            this.rangeEnd = rangeEnd;
        }
        public bool IsRange(double number)
        {
            return Math.Round(number, 1) >= Math.Round(rangeStart, 1)
                && Math.Round(number, 1) <= Math.Round(rangeEnd, 1);
          
        }
        public override string ToString()
        {
            return $"[{rangeStart} - {rangeEnd}]";
        }
    }
}
