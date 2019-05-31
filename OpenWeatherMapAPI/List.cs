namespace OpenWeatherMapAPI
{

    public class List
    {
        public int Dt { get; set; }
        public Main Main { get; set; }
        public System.Collections.Generic.List<Weather> Weather { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public Sys Sys { get; set; }
        public string Dt_txt { get; set; }
    }

}
