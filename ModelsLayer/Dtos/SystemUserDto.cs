using ModelsLayer.ValidateAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer.Dtos
{
    public class SystemUserInsert
    {
        public string Phone_Number { get; set; }

        [Required(ErrorMessage = "UserName Mandatory")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Mandatory")]
        [CustomEmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Mandatory")]
        public string Password { get; set; }
    }

    public class SystemUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
    }

    public class SystemUserLogin
    {
        [Required(ErrorMessage = "UserName Mandatory")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Mandatory")]
        public string Password { get; set; }
    }
}
