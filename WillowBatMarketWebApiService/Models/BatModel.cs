using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WillowBatMarketWebApiService.Models
{
    public class BatModel
    {
        [Key]
        public Guid batId { get; set; }
        public decimal batWeight { get; set; }
        public string batBladded { get; set; }
        public decimal sellingPrice { get; set; }
        public int batGrains { get; set; }
        public string batQuality { get; set; }
        public Guid manufacturerId { get; set; }
        public string batBrand { get; set; }
        public short quantity { get; set; }
        public string toe { get; set; }
        public string spine { get; set; }
        public string batSize { get; set; }
        public decimal costPrice { get; set; }
        public decimal discount { get; set; }
        [NotMapped]
        public string base64Image { get; set; }

    }
}
