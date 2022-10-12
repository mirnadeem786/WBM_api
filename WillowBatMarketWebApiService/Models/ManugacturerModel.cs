using System.ComponentModel.DataAnnotations;
using System;

namespace WillowBatMarketWebApiService.Models
{
    public class ManufacturerModel
    {
        [Required]
        public string MANUFAC_NAME { get; set; }
        public string MANUFAC_CONTACT_NO { get; set; }
        public Guid MANUFAC_ID { get; set; }
    }
}
