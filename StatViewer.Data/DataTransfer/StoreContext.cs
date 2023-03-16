using Microsoft.EntityFrameworkCore;
using StatViewer.Data.Models;

namespace StatViewer.Data.DataTransfer
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Entry> Entries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("storeDb");
            }
        }

    }
}
