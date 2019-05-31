namespace GeoNamesAPI
{
    public class Query
    {
        public bool IsValidQuery { get; }
        public QueryResult ThisQueryResult { get; }

        public Query(string apiKey, double lat, double lng)
        {
            try
            {
                ThisQueryResult = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryResult>(new System.Net.WebClient()
               .DownloadString($"http://api.geonames.org/astergdemJSON?lat={lat}&lng={lng}&username={apiKey}".Replace(",", ".")));
                IsValidQuery = true;
            }
            catch
            {
                IsValidQuery = false;
            }

        }

    }
}
