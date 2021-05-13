using System;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
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
            var subscriber = new Subscriber
                {Id = (int) e.Message.Chat.Id, Name = e.Message.Chat.FirstName, IsSubscribing = false};
            var messageReader = new MessageReader(e.Message.Text);
            using (var db = new Subscribers())
            {
                db.Database.Migrate();
                db.TSubscriber.Add(subscriber);
                var msg = e.Message;
                if (msg.Text == "/subscribe")
                {
                    Subscribed(e, subscriber, true);
                    return;
                }
                if (msg.Text == "/unsubscribe")
                {
                    Subscribed(e, subscriber, false);
                    return;
                }
            }
            await telegramBot.SendTextMessageAsync(e.Message.Chat.Id, messageReader.Message);
        }

        private static async void Subscribed(Telegram.Bot.Args.MessageEventArgs e, Subscriber subs, bool isSubscriber)
        {
            using (var db = new Subscribers())
            {
                subs.IsSubscribing = isSubscriber;
                db.TSubscriber.Update(subs);
                await telegramBot.SendTextMessageAsync(e.Message.Chat.Id, isSubscriber ? "Вы подписались!" : "Вы отписались(");
                Console.WriteLine(isSubscriber ? "Кто-то подписался" : "Кто-то отписался");
                db.SaveChanges();
            }
        }
    }
}

