using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WillowBatMarketWebApiService.Entity
{
    public class Manufacturer
    {
      
        public Guid manufacturerId { get; set; }
        public Guid usserId { get; set; }
        //public  Address address { get; set; }
    }
}
