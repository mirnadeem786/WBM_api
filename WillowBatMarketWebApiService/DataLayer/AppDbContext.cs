using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.DataLayer
{
    public class AppDbContext:DbContext
    {



        protected readonly IConfiguration Configuration;


        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseSqlServer(Configuration.GetConnectionString("myDbCon"));
        }


        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Willow>Willow { get; set; }
        public DbSet<Cricketer> Cricketer { get; set; }
        public DbSet<Bat> Bat { get; set; }
        public DbSet<WillowSeller> WillowSeller { get; set; }
        
        public DbSet<CricketerDashboard> CricketerDashboard { get; set; }   

        public DbSet<ManufacturerDashboard> ManufacturerDashboard { get; set; }
        public DbSet<OrderDashboard> OrderDashboard { get; set; }
       
         public DbSet<Cart> Cart { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Auction> Auction { get; set; }
        public DbSet<Bidder> Bidder { get; set; }
        public DbSet<Ussers> Ussers { get; set; }
        public DbSet<UsserType>UsserType { get; set; }
         public DbSet<ItemImages> ItemImages { get; set; }    
        public DbSet<CartItems> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItems>().HasKey(c => new { c.itemId, c.cartId });
            modelBuilder.Entity<CartItems>().HasOne<Cart>().WithMany(c => c.CartItems).HasForeignKey(f => f.cartId);
          modelBuilder.Entity<Cricketer>().HasOne(c=>c.Cart).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Cart>().HasMany(c=>c.CartItems).WithOne().OnDelete(DeleteBehavior.Cascade); 
            modelBuilder.Entity<Cricketer>().HasOne(c => c.Cart).WithOne().HasForeignKey<Cart>(c => c.cricketerId);

        }


    }
}
