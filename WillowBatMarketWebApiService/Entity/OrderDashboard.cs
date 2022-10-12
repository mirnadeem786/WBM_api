using Microsoft.EntityFrameworkCore;
using System;

namespace WillowBatMarketWebApiService.Entity
{
    [Keyless]
    public class OrderDashboard
    {
         public Guid entityId { get; set; }
            public Guid itemId { get; set; }
            public string entityName { get; set; }
             public string itemName { get; set; }
            public DateTime orderDate { get; set; }
           public string orderStatus { get; set; }
             public int quantity { get; set; }
         public decimal   amount { get; set; }
        public Guid orderId { get; set; }


    }
}
