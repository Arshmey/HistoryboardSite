using Microsoft.EntityFrameworkCore;
using NewsSite.Models;

namespace NewsSite.DataAccess
{
    public class MessageDbContext : DbContext
    {

        private readonly IConfiguration configuration;

        public DbSet<Message> Messages { get; set; }

        public MessageDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Message>().ToTable("Messages");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Messages.db");
        }
    }
}
