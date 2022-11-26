using System;
using System.ComponentModel.DataAnnotations;

namespace WillowBatMarketWebApiService.Entity
{
    public class OrderItems
    {

        [Key]
        public Guid orderId { get; set; }
        public Guid cricketerId { get; set; }
        public DateTime orderDate { get; set; }
        public short quantity { get; set; }
        public decimal amount { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
        public Guid itemId { get; set; }
        public string itemType { get; set; }
        public decimal discount { get; set; }
    }
}
