namespace OpenWeatherMapAPI
{
    public class OpenWeatherAPI
    {
        private string openWeatherAPIKey;

        public OpenWeatherAPI(string apiKey)
        {
            openWeatherAPIKey = apiKey;
        }

        public void UpdateAPIKey(string apiKey)
        {
            openWeatherAPIKey = apiKey;
        }

        public Query Query(string queryStr)
        {
            Query newQuery = new Query(openWeatherAPIKey, queryStr);
            if(newQuery.IsValidQuery)
            return newQuery;

            return null;
        }
    }
}
