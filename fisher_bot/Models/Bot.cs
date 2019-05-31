using System;
using Telegram.Bot;

namespace fisher_bot.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;

        public static TelegramBotClient Get()
        {

            if (client != null)
            {
                return client;
            }

                client = new TelegramBotClient(BotSettings.Key);
                client.SetWebhookAsync("");

            return client;
        }
    }
}
