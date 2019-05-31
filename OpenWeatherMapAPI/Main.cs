namespace OpenWeatherMapAPI
{
    public class Main
    {
        public double Temp { get; set; }
        public double TempAsCelsium => Temp - 273.15;
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public double Pressure { get; set; }
        public double PressureAsmmHg => Pressure * 0.75006375541921;
        public double Sea_level { get; set; }
        public double Grnd_level { get; set; }
        public int Humidity { get; set; }
        public double Temp_kf { get; set; }
    }
}