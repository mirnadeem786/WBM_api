using System;
using System.ComponentModel.DataAnnotations;

namespace WillowBatMarketWebApiService.Entity
{
    public class Ussers
    {
        [Key]
        public Guid usserId { get; set; }
        public string name { get; set; }
        public string addressDetails { get; set; }
        public string email { get; set; }
        public string encriptedPassword { get; set; }
        public string phoneNo { get; set; }
        public  string userType { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
        // public bool isActive { get; set; }
        //public Guid? RoleId { get; set; }



    }
}
