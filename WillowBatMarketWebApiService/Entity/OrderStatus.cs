using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
