using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.BusinessProcess.Entities.Base
{
    [Serializable]
    public class SessionEntities
    {
        public string UserName { get; set; }
        public DateTime BusinessDate { get; set; }
        public string OfficeID { get; set; }
        public string FormID { get; set; }
        public string FullName { get; set; }
        public Boolean IsEdit { get; set; }
    }
}
