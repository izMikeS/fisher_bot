using fisher_bot.Models;
using System;

namespace fisher_bot.Controllers
{
    public class ConsoleCommandsController
    {
        private string consoleCommand;
        public void StartReceiving()
        {
            Console.Write($"Бот успішно завантажений. Телеграм псевдонім - @{BotSettings.Name}\n" +
                           "Список доступних команд - /help\n");
            while(true)
            {
                consoleCommand = Console.ReadLine();
                if(consoleCommand == "/quit") break; // TODO: нормально реализовать
                else if(consoleCommand == "/help")
                {
                    Console.Write("Список команд:\n" +
                                  "/logs [on/off] - ввімкнути/вимкнути логування;\n" +
                                  "/quit - вихід;\n");
                }
                else if (consoleCommand.Contains("/logs"))
                {
                    string[] data = consoleCommand.Split(' ');
                    if(data == null || data.Length > 2 || data.Length < 2
                      || (data[1] != "on" && data[1] != "off"))
                    {
                        Console.WriteLine("Неправильний синтаксис команди. Див. /help");
                        continue;
                    }

                    if (data[1] == "off")
                    {
                        BotSettings.Logging = false;
                        Console.WriteLine("Логування вимкнуто.");
                    }
                    else 
                    {
                        Console.WriteLine("Логування ввімкнуто.");
                        BotSettings.Logging = true;
                    }
                }
                else
                {
                    Console.WriteLine("Такої команди не існує. Див. /help");
                }
            }
        }
    }
}
