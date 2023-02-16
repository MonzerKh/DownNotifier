using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer.Dtos
{
    public class AppCheckHistoryInsert
    {
        public int TargetApplication_Id { get; set; }
        public bool IsUp { get; set; }
        public DateTime ExecuteTime { get; set; }
    }

    public class AppCheckHistoryUpdate : AppCheckHistoryInsert
    {
        public int Id { get; set; }
    }

    public class AppCheckHistoryDto : AppCheckHistoryInsert
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
