using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WillowBatMarketWebApiService.Entity
{
    public class Cart
    {
        [Key]
        public Guid cartId { get; set; }    
        public Guid cricketerId { get; set; }
        public decimal totalAmount { get; set; }
        public virtual List<CartItems> CartItems { get; set; }
        [NotMapped]
        public virtual Cricketer Cricketer { get; set; }    


    }
}
