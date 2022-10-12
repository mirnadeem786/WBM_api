using System.ComponentModel.DataAnnotations;
using System;

namespace WillowBatMarketWebApiService.Models
{
    public class CricketerModel
    {


        public string CRICKETER_NAME { get; set; }
        [Key]
        public Guid CRICKETER_ID { get; set; }
        public Single CRICKETER_HEIGHT { get; set; }
        public int CRICKETER_AGE { get; set; }
        public String CRICKETER_CONTACT_NO { get; set; }



    }
}
