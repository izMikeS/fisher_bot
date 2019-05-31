namespace OpenWeatherMapAPI
{
    public class Query
    {
            public bool IsValidQuery { get; }
            public  QueryResult ThisQueryResult { get; }

        public Query(string apiKey, string queryStr)
        {
            try
            {
                ThisQueryResult = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryResult>(new System.Net.WebClient()
               .DownloadString($"http://api.openweathermap.org/data/2.5/forecast?appid={apiKey}&q={queryStr}"));
                 
                IsValidQuery = true;
            }
            catch
            {
                IsValidQuery = false;
            }
           
        }

    }
}
