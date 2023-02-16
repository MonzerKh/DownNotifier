using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer.Business
{
    public class AppSettings
    {
        public string MailFrom { get; set; }
        public string MailUser { get; set; }
        public string MailPwd { get; set; }
        public string MailHost { get; set; }
        public string MailPort { get; set; }
        public string MailDisplayName { get; set; }

    }
}
