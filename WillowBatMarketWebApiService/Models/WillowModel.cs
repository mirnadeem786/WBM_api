using System.ComponentModel.DataAnnotations;
using System;

namespace WillowBatMarketWebApiService.Models
{
    public class WillowModel
    {



        [Key]
      
        public Guid willowId { get; set; }
        public Guid willowSellerId { get; set; }
       
        public byte[] WillowImage { get; set; }
        public decimal willowPrice { get; set; }
        public string willowType { get; set; }
        public int willowSize { get; set; }
        public string address { get; set; }
    }


}

