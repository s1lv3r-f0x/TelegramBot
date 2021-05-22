using Microsoft.EntityFrameworkCore;

namespace Telegram_Bot
{
    public class Subscribers : DbContext
    {
        public DbSet<Subscriber> TSubscriber { get; set; }

        //public Subscribers() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=C:\Users\yatin\source\repos\TelegramBot\Subscribers.db");
        }
    }

    public class Subscriber
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsSubscribing { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
