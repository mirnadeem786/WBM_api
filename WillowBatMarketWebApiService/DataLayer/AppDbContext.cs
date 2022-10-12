using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WillowBatMarketWebApiService.Entity;

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




    }
}
