using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer.Entities
{
    public class SystemUser
    {
        public int Id { get; set; }
        public string Phone_Number { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<TargetApplication> TargetApplications { get; set; }
    }
}
