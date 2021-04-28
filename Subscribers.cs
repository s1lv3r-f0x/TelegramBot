using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Telegram_Bot
{
    public class Subscribers : DbContext
    {
        public List<long> SubscriberID { get; set; }
        public List<string> SubscriberName { get; set; }
        public List<bool> Subscribe { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\Users\yatin\source\repos\TelegramBot\Subscribers.db");
    }
}
