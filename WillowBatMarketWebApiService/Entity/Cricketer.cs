using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WillowBatMarketWebApiService.Entity
{
    public class Cricketer
    {
      
       
        [Key]
        public Guid cricketerId { get; set; }    
     public Guid usserId { get; set; }
        [NotMapped]
     public virtual Cart Cart { get; set; }
        public List<OrderItems> Items { get; set; } 
    }
}
