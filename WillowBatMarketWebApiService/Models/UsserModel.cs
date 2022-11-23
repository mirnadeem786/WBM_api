using System.ComponentModel.DataAnnotations;
using System;

namespace WillowBatMarketWebApiService.Models
{
    public class UsserModel
    {

               [Key]
            public Guid usserId { get; set; }
        [Required]
            public string name { get; set; }
            public string addressDetails { get; set; }
        [Required]
            public string email { get; set; }
        [Required]
        public string phoneNo { get; set; }
            public string usserType { get; set; }
            public DateTime createdOn { get; set; }
            public DateTime updatedOn { get; set; }
        // public bool isActive { get; set; }
        //public Guid? RoleId { get; set; }


        private string _password;
        [MinLength(8)]
        public string password
        {
            get => _password;
            set => _password = replaceEmptyWithNull(value);
        }

        private string _confirmPassword;
        [Compare("password")]
        public string confirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword =replaceEmptyWithNull(value);
        }

        // helpers

        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }



}
    

