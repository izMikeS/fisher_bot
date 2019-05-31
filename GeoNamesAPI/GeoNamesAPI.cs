namespace GeoNamesAPI
{
    public class GeoNamesAPI
    {
        private string openWeatherAPIKey;

        public GeoNamesAPI(string apiKey)
        {
            openWeatherAPIKey = apiKey;
        }
        public GeoNamesAPI()
        {
            openWeatherAPIKey = "demo";
        }

        public void UpdateAPIKey(string apiKey)
        {
            openWeatherAPIKey = apiKey;
        }

        public Query Query(double lat, double lng)
        {
            Query newQuery = new Query(openWeatherAPIKey, lat, lng);
            if(newQuery.IsValidQuery)
            return newQuery;

            return null;
        }
    }
}
