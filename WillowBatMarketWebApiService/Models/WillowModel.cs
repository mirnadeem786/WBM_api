using System.ComponentModel.DataAnnotations;
using System;

namespace WillowBatMarketWebApiService.Models
{
    public class WillowModel
    {

        [Key]
        public Guid willowId { get; set; }
        public Guid willowSellerId { get; set; }
        public string WillowImage { get; set; }
        public decimal willowPrice { get; set; }
        public string willowType { get; set; }
        public Decimal willowSize { get; set; }


    }


}

