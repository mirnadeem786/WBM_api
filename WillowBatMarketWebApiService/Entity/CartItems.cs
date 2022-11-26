using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WillowBatMarketWebApiService.Entity
{
   
    public class CartItems
    {
      // [ ForeignKey("cartId")]
        public Guid cartId { get; set; }
           public short quantity { get; set; }
          public decimal  amount { get; set; }
            public DateTime  createdOn { get; set; }
        public DateTime updatedOn { get; set; }
           public Guid itemId { get; set; }
          public string itemType { get; set; }
       
       


    }
}
