using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WillowBatMarketWebApiService.Entity
{
    public class Manufacturer
    {
        public string manufacturerName { get; set; }
        public string phoneNo { get; set; }
        [Key]
        public Guid manufacturerId { get; set; }
    }
}
