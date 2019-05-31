namespace OpenWeatherMapAPI
{
    public class QueryResult
    {
        public string Cod { get; set; }
        public double Message { get; set; }
        public int Cnt { get; set; }
        public System.Collections.Generic.List<List> List { get; set; }
        public City City { get; set; }
    }
}
