namespace FishingForecast.Models
{

    public class Weather
    {
        public double Pressure { get; }
        public double Temperature { get; }

        public Weather(double pressure, double temperature)
        {
            this.Pressure = pressure;
            this.Temperature = temperature;
        }

    }

}
