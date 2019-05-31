using fisher_bot.Controllers;
using fisher_bot.Models;
using System;
using Telegram.Bot;

namespace fisher_bot
{
    class Program
    {
        private static TelegramBotClient client;
        private static InlineQueryController inlineQueryController;
        private static ConsoleCommandsController consoleCommandsController;
        static void Main(string[] args)
        {
            client = Bot.Get();
            inlineQueryController = new InlineQueryController();
            consoleCommandsController = new ConsoleCommandsController();

            client.OnReceiveError += async (obj, excArgs) => 
            await BotLogs.LogAsync($"Виникла помилка: {excArgs.ApiRequestException.Message}.");
            client.OnReceiveGeneralError += async (obj, excArgs) =>
            await BotLogs.LogAsync($"!!! Виникла критична помилка: {excArgs.Exception.Message}.");

            client.OnInlineQuery += inlineQueryController.Client_OnInlineQuery;
            client.StartReceiving();


            consoleCommandsController.StartReceiving();
        }


    }
}
