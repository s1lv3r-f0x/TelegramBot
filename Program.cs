using System;
using Telegram.Bot;


namespace Telegram_Bot
{
    class Program
    {
        private static string token = "1053517895:AAFHsQ4KiyD7AmX8YD1OhiWf-bw3H5IsXgs";
        private static TelegramBotClient telegramBot;
        static void Main(string[] args)
        {
            telegramBot = new TelegramBotClient(token);
            telegramBot.StartReceiving();
            telegramBot.OnMessage += TelegramBot_OnMessage;
            Console.ReadKey();
            telegramBot.StopReceiving();
        }

        private static async void TelegramBot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg != null)
            {
                Console.WriteLine($@"Пришло сообщение {msg.Text}");
                await telegramBot.SendTextMessageAsync(msg.Chat.Id, msg.Text); 
            }
        }
    }
}
