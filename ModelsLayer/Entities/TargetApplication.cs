using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer.Entities
{
    public class TargetApplication
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public bool LastCheckIsUp { get; set; }
        public int Interval { get; set; }
        public bool IsActive { get; set; }

        public List<AppCheckHistory> AppCheckHistories { get; set; }
        public SystemUser SystemUser { get; set; }
        public int SystemUser_Id { get; set; }
    }
}
