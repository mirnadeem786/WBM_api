using System.ComponentModel.DataAnnotations;
using System;

namespace WillowBatMarketWebApiService.Models
{
    public class ResetPassword
    {
       
            public Guid? UserId { get; set; }
            [Required]
            public string OldPassword { get; set; }

            private string _password;
            [MinLength(6)]
            [Required]
            public string NewPassword
            {
                get => _password;
                set => _password = replaceEmptyWithNull(value);
            }

            private string _confirmPassword;
            [Compare("NewPassword")]
            [Required]
            public string ConfirmNewPassword
            {
                get => _confirmPassword;
                set => _confirmPassword = replaceEmptyWithNull(value);
            }
            private string replaceEmptyWithNull(string value)
            {
                // replace empty string with null to make field optional
                return string.IsNullOrEmpty(value) ? null : value;
            }






        }
}
