using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace fisher_bot.Models
{
    public static class BotLogs
    {
        private const string path = "logs.txt";
        public static async Task LogAsync(string msg)
        {
            if (!BotSettings.Logging) return;
            await Task.Run(() => Console.WriteLine($"[{DateTime.Now}]: {msg}"));
            if (!File.Exists(path))
            {
                await File.WriteAllTextAsync(path, $"[{DateTime.Now}]: {msg}");
                return;
            }
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                await sw.WriteLineAsync($"[{DateTime.Now}]: {msg}");
            }
        }
    }
}
