using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer.Dtos
{
    public class TargetApplicationInsert
    {
        [Url]
        [Required(ErrorMessage = "URL Mandatory")]
        public string URL { get; set; }
        public string Name { get; set; }
      
        [Required(ErrorMessage = "Interval Mandatory")]
        public int Interval { get; set; }
        public int SystemUser_Id { get; set; }
    }

    public class TargetApplicationUpdate 
    {
        
        public int Id { get; set; }
        public bool IsActive { get; set; }
        [Url]
        [Required(ErrorMessage = "URL Mandatory")]
        public string URL { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Interval Mandatory")]
        public int Interval { get; set; }
    }



    public class TargetApplicationDto
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public bool? LastCheckIsUp { get; set; }
        public DateTime? LastCheckTime { get; set; }
        public int Interval { get; set; }
        public bool? IsActive { get; set; }
        public string Email { get; set; }

    }
}
