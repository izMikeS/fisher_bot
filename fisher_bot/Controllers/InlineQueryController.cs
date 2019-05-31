using fisher_bot.Models;
using FishingForecast.Controllers;
using FishingForecast.Models;
using OpenWeatherMapAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.InlineQueryResults;

namespace fisher_bot.Controllers
{
    public class InlineQueryController
    {
        public async void Client_OnInlineQuery(object sender, Telegram.Bot.Args.InlineQueryEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(e.InlineQuery.Query)) return;
            var inlineQuery = e.InlineQuery;
            var client = Bot.Get();

            var weatherClient = new OpenWeatherAPI("TODO: YourApiKey");
            var query = weatherClient.Query(inlineQuery.Query);
            await BotLogs.LogAsync($"Користувачем {inlineQuery.From.FirstName} ({inlineQuery.From.Id}) створено запит \"{inlineQuery.Query}\"");
            if (query == null)
            {
                try
                {
                    await client.AnswerInlineQueryAsync(inlineQuery.Id, new List<InlineQueryResultArticle> {
                    new InlineQueryResultArticle("Error:cityNotFound", "Помилка запиту...",
                    new InputTextMessageContent("Технічні неполадки. Спробуйте пізніше або перевірте коректність своїх дій."))
                    });
                   await BotLogs.LogAsync($"Помилка. На запит \"{inlineQuery.Query}\" у OpenWeatherAPI відповіді не знайдено.");
                }
                catch(Exception ex)
                {
                    await BotLogs.LogAsync($"Помилка при відправленні повідомлення: {ex.Message}");
                }
                return;
            }

            var city = query.ThisQueryResult.City;

            var weathers = query.ThisQueryResult.List;
            List<FishingForecast.Models.Weather> weathersList = new List<FishingForecast.Models.Weather>();

            foreach (var weather in weathers)
            {
                weathersList.Add(new FishingForecast.Models.Weather(
                                 weather.Main.PressureAsmmHg,
                                 weather.Main.TempAsCelsium));
            }

            var astergdemClient = new GeoNamesAPI.GeoNamesAPI();

            var astergdemQuery = astergdemClient.Query(city.Coord.Lat, city.Coord.Lon);

            if (astergdemQuery == null)
            {
                try
                {
                    await client.AnswerInlineQueryAsync(inlineQuery.Id, new List<InlineQueryResultArticle> {
                    new InlineQueryResultArticle("Error:cityNotFound", "Помилка запиту...",
                    new InputTextMessageContent("Технічні неполадки. Спробуйте пізніше або перевірте коректність своїх дій."))
                    });
                    await BotLogs.LogAsync($"Помилка. На запит \"{inlineQuery.Query}\" у GeoNamesAPI відповіді не знайдено.");
                }
                catch (Exception ex)
                {
                    await BotLogs.LogAsync($"Помилка при відправленні повідомлення: {ex.Message}");
                }
                return;
            }

            var weatherController = new WeatherController(
                    weathersList.ToArray(),
                    new FishingStateRangesCalculator(astergdemQuery.ThisQueryResult.Astergdem));

            var fishingResults = weatherController.CalculateResults();
            var actualDate = DateTime.Parse(weathers.Last().Dt_txt);
            try
            {
                await client.AnswerInlineQueryAsync(inlineQuery.Id, new List<InlineQueryResultArticle> {
                    new InlineQueryResultArticle(city.Name + DateTime.Now, $"{city.Name}, {city.Country}",
                    new InputTextMessageContent(
                    $"Риболовля у {city.Name}, {city.Country}.\n" +
                    $"Прогноз на {actualDate.ToShortDateString()}:\n" +
                    $"{fishingResults.ToString()}\n" +
                    $"{new string('-', 43)}\n"+
                    $"provided by @{BotSettings.Name} {DateTime.Now.Year}"))
                    });
                await BotLogs.LogAsync($"Успіх. На запит \"{inlineQuery.Query}\" від {inlineQuery.From.FirstName} ({inlineQuery.From.Id}) надіслано відповідь.");
            }
            catch (Exception ex)
            {
                await BotLogs.LogAsync($"Помилка при відправленні повідомлення: {ex.Message}");
            }
        }
    }
}
