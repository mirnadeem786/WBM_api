using System.ComponentModel.DataAnnotations;
using System;
using System.Net;
using WillowBatMarketWebApiService.Entity;

namespace WillowBatMarketWebApiService.Models
{
    public class ManufacturerModel
    {
        [Required]

       
        public Guid manufacturerId { get; set; }
        public Guid usserId { get; set; }
        // public Address address { get; set; }

    }
   
}
