using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BitCoinPriceMonitor.Infrastrucutre.Persistance
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<PriceSnapshot> PriceSnapshots { get; set; }
        public DbSet<PriceSource> PriceSources { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PriceSnapshot>()
                .HasIndex(p => p.RetrievedTimeStamp);
            builder.Entity<PriceSnapshot>().Property(p => p.Value).HasPrecision(2);

            builder.Entity<PriceSource>().Property(p => p.Name).HasMaxLength(256);

            builder.Entity<PriceSource>().HasMany(p => p.PriceSnapshots)
                .WithOne(p => p.PriceSource)
                .HasForeignKey(p => p.PriceSourceId);

            builder.Entity<PriceSource>().HasData(new PriceSource 
            { 
                Id = Guid.NewGuid().ToString(), 
                CreatedTimeStamp= DateTime.UtcNow,
                CreatorId= Guid.Empty.ToString(),
                Name = "Bit Stamp",
                Url = "https://www.bitstamp.net/api/ticker/"
            });
            builder.Entity<PriceSource>().HasData(new PriceSource
            {
                Id = Guid.NewGuid().ToString(),
                CreatedTimeStamp = DateTime.UtcNow,
                Name = "Coin Base",
                CreatorId = Guid.Empty.ToString(),
                Url = "https://api.pro.coinbase.com/products/ADA-USD/ticker"
            });
        }
    }
}