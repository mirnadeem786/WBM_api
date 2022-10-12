using System;
using System.ComponentModel.DataAnnotations;

namespace WillowBatMarketWebApiService.Entity
{
    public class OrderStatus
    {
        [Key]
        public Guid orderId { get; set; }
        public string status { get; set; }
        public DateTime date { get; set; }

    }
}
