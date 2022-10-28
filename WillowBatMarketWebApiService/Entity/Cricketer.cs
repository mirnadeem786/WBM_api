using System;
using System.ComponentModel.DataAnnotations;

namespace WillowBatMarketWebApiService.Entity
{
    public class Cricketer
    {
      
       
        [Key]
        public Guid cricketerId { get; set; }    
     public Guid usserId { get; set; }
    }
}
