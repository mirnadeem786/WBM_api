using Microsoft.EntityFrameworkCore;
using System;

namespace WillowBatMarketWebApiService.Models
{
    [Keyless]
    public class CricketerDashBoardModel
    {
        public string MmanufacturerName { get; set; }
        public string phoneNo { get; set; }
        public decimal batWeight { get; set; }
        public string batBladded { get; set; }
        public decimal sellingPrice { get; set; }
        public decimal costPrice { get; set; }
        public decimal discount { get; set; }
        public int batGrains { get; set; }
        public string batQuality { get; set; }
        public Guid manufacturerId { get; set; }
        public string batBrand { get; set; }
        public int batLength { get; set; }
        public short batStock { get; set; }
        public string batImage { get; set; }


    }
}
