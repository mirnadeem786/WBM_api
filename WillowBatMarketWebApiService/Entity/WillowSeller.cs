using System;
using System.ComponentModel.DataAnnotations;

namespace WillowBatMarketWebApiService.Entity
{
    public class WillowSeller
    {
        public string willowSellerName { get; set; }
        [Key]
        public Guid willowSellerId { get; set; }
        public string willowSellerContactNo { get; set; }
      




    }
}
