using System;
using System.ComponentModel.DataAnnotations;

namespace WillowBatMarketWebApiService.Entity
{
    public class OrderEntity
    {
        [Key]
       public Guid orderId { get; set; }
           public Guid   entityId { get; set; }
           public DateTime orderDate { get; set; }
            public string orderStatus { get; set; }
           public string entityType { get; set; }
            public string  entityName { get; set; }
    }
}
