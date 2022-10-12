using System;
using System.ComponentModel.DataAnnotations;

namespace WillowBatMarketWebApiService.Entity
{
    public class BatOrder
    {

        [Key]
        public Guid ORDER_ID { get; set; }  
        public  Guid BAT_ID { get; set; }
       public short QUANTITY { get; set; }
        public Double   AMOUNT { get; set; }
    }
}
