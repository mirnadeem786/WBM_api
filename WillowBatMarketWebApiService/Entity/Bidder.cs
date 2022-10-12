using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WillowBatMarketWebApiService.Entity
{
    
    public class Bidder
    {
        
       public Guid bidderId { get; set; }
      //  public string bidderName { get; set; }  
       public decimal amount { get; set; }

        [ForeignKey ("auctionId")]
        public Guid auctionId { get; set; }


    }
}
