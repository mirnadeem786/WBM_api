using System;
using System.ComponentModel.DataAnnotations;

namespace WillowBatMarketWebApiService.Entity
{
    public class Auction
    {
        [Key]
        public Guid auctionId { get; set; }
          public Guid  itemId { get; set; }
             public decimal startingPrice { get; set; }
             public DateTime startingDateTime { get; set; }
              public DateTime  endDateTime { get; set; }
          public decimal  highestAmount { get; set; }

    }
}
