using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer.Entities
{
    public class AppCheckHistory
    {
        public int Id { get; set; }
        public DateTime ExecuteTime { get; set; }
        public bool IsUp { get; set; }

        public int TargetApplication_Id { get; set; }
        public TargetApplication TargetApplication { get; set; }

    }
}
