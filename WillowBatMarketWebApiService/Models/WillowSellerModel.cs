using System.ComponentModel.DataAnnotations;
using System;

namespace WillowBatMarketWebApiService.Models
{
    public class WillowSellerModel
    {
        public string WillowSellerName { get; set; }
        [Key]
        public Guid Id { get; set; }
        public string Phone { get; set; }




    }
}
