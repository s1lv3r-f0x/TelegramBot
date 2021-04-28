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
        }

        private static async void TelegramBot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text == "/subscribe")
            {
                using (var db = new Subscribers())
                {
                    if (db.SubscriberID is null)
                    {
                        db.SubscriberID.Add(msg.Chat.Id);
                        db.SubscriberName.Add(msg.Chat.Username);
                        db.Subscribe.Add(true);
                        await telegramBot.SendTextMessageAsync(msg.Chat.Id, "Вы успепшно подписались на рассылку!");
                        db.SaveChanges();
                    }
                    if (db.SubscriberID.Contains(msg.Chat.Id))
                        await telegramBot.SendTextMessageAsync(msg.Chat.Id, "Вы уже подписаны!");
                    else
                    {
                        await telegramBot.SendTextMessageAsync(msg.Chat.Id, "Фигня какая-то!");
                    }
                }
                
            }

            if (msg.Text == "/dbname")
            {
                using (var db = new Subscribers())
                {
                    foreach (var name in db.SubscriberName.ToArray())
                    {
                        await telegramBot.SendTextMessageAsync(msg.Chat.Id, name);
                    }
                }
            }
        }
    }
}
