using System.ComponentModel.DataAnnotations;
using System;

namespace WillowBatMarketWebApiService.Models
{
    public class WillowModel
    {


        [Key]
        public Guid WILLOW_ID { get; set; }
        public Guid WILLOW_SELLER_ID { get; set; }
        public string IMAGE { get; set; }
        public int PRICE { get; set; }
        public string TYPE { get; set; }

        public Decimal SIZE { get; set; }


    }


}

