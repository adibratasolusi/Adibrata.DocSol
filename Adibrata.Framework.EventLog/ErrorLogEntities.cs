using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Adibrata.Framework.Logging
{
    [Serializable]    
    public class ErrorLogEntities
    {
     
        public string NameSpace { get; set; }
        public string ClassName { get; set; }
        public string ExceptionDescription { get; set; }
        public int ExceptionNumber { get; set; }
        public int EventID { get; set; }
        public string UserName { get; set; }
        public string EventSource { get; set; }
        public Exception ExceptionObject { get; set; }
        public string FunctionName { get; set;  }
    }
}
