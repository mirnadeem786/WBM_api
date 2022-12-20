using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WillowBatMarketWebApiService.Entity
{
    public class Willow
    {
        [Key]
       public Guid willowId { get; set; }
       public Guid willowSellerId { get; set; }
    
        public string WillowImage { get; set; }

       public decimal willowPrice { get; set; }
       public string willowType { get; set; }
       public  int willowSize { get; set; }
        public string address { get; set; }
        [NotMapped]
        public string base64Image { get; set; }
        [NotMapped]
        public decimal totalAmount
        {
            get
            {

                return willowPrice * willowSize;
            }


        }


    }
}
