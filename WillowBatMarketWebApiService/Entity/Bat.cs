using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WillowBatMarketWebApiService.DataLayer;

namespace WillowBatMarketWebApiService.Entity
{
    public class Bat
    {
        [Key]
        public Guid batId { get; set; }
        public int batWeight { get;  set; }
        public string batBladded { get;  set; }
        public decimal sellingPrice { get;  set; }
        public decimal costPrice { get; set; }
        public int batGrains { get;  set; }
        public string batQuality { get;  set; }
        public Guid manufacturerId { get; set; }
        public string batBrand { get;  set; }
        public int batLength { get; set; }
        public short batStock { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
        public decimal discount { get; set; }
        [NotMapped]
        public string base64Image { get; set; }

    }
}
