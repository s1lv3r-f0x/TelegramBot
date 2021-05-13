using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Telegram_Bot
{
    public class MessageReader
    {
        private readonly Dictionary<string, string> commandsList = new Dictionary<string, string>
        {
            {"/start", @"C:\Users\yatin\source\repos\TelegramBot\Information\start.txt"},
            {"функционал бота", @"C:\Users\yatin\source\repos\TelegramBot\Information\функционал бота.txt"},
            {"показать базу данных", @"C:\Users\yatin\source\repos\TelegramBot\Information\показать базу данных.txt"},
            {"написать разработчику бота", @"C:\Users\yatin\source\repos\TelegramBot\Information\написать разработчику бота.txt"}
        };

        public string Message;

        public MessageReader(string command) => Message = commandsList.ContainsKey(command.ToLower()) ? 
            ReadMessageFromFiles(commandsList[command]) : "Неизвестная команда, возможно я добавлю ее чуть позже.";

        private string ReadMessageFromFiles(string path)
        {
            using (var rd = new StreamReader(path))
            {
                return rd.ReadToEnd();
            }
        }
    }
}
