using ModelsLayer.Dtos;
using ModelsLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModelsLayer.Business
{
    public class MonitorUrls
    {
        public TargetApplicationDto Application { get; set; }
        public Timer TimerCall { get; set; }

       

    }
}
