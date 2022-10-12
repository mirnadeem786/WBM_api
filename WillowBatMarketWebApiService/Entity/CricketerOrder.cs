using System;
using System.ComponentModel.DataAnnotations;

namespace WillowBatMarketWebApiService.Entity
{
    public class CricketerOrder
    {
        [Key]
       public Guid ORDER_ID { get; set; }
         public string STATUS { get; set; } 
       public DateTime DATE { get; set; }
        public Guid  CRICKETER_ID { get; set; }
    }
}

