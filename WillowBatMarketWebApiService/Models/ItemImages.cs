using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WillowBatMarketWebApiService.Models
{
    public class ItemImages
    {
        [Key]
       public  Guid imageId { get; set; }   
        public string imageName { get; set; }
        public string imageType { get; set; }
        public Guid itemId { get; set; }
        public string imagePath { get; set; }
        [NotMapped]
        public string base64image { get; set; }


    }

}
