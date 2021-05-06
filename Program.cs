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
            using (var db = new Subscribers())
            {
                db.Database.Migrate();
                db.TSubscriber.Add(subscriber);

                var msg = e.Message;
                if (msg.Text == "/subscribe")
                {
                    subscriber.IsSubscribing = true;
                    db.TSubscriber.Update(subscriber);
                    await telegramBot.SendTextMessageAsync(e.Message.Chat.Id, "Вы подписались!");
                    Console.WriteLine("Кто-то подписался!");
                    db.SaveChanges();
                }

                if (msg.Text == "/unsubscribe")
                {
                    subscriber.IsSubscribing = false;
                    db.TSubscriber.Update(subscriber);
                    await telegramBot.SendTextMessageAsync(e.Message.Chat.Id, "Вы отписались!");
                    Console.WriteLine("Кто-то отписался!");
                    db.SaveChanges();

                }

                if (msg.Text == @"\database")
                {
                    
                    try
                    {
                        await telegramBot.SendTextMessageAsync(e.Message.Chat.Id,
                                db.TSubscriber.OrderBy(b => b.Name).First().ToString());
                    }
                    catch (Exception exception)
                    { 
                        Console.WriteLine(exception); 
                        await telegramBot.SendTextMessageAsync(e.Message.Chat.Id, "Пока тут пусто");
                    }
                }
            }
        }
    }
}

