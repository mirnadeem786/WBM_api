using System;
using System.ComponentModel.DataAnnotations;

namespace WillowBatMarketWebApiService.Entity
{
    public class Cart
    {
        [Key]
       public Guid cartId { get; set; }
          public Guid  customerId { get; set; }
           public short quantity { get; set; }
          public decimal  amount { get; set; }
            public DateTime  createdOn { get; set; }
        public DateTime updatedOn { get; set; }
           public Guid itemId { get; set; }
          public string itemType { get; set; }
       


    }
}
