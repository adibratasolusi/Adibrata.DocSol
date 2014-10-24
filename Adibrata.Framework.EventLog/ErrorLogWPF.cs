using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.Framework.Logging
{
   public class ErrorLogWPF
    {
       public Exception ErrorLog_WPF (ErrorLogEntities _ent)
       {
           Exception _exp;
           _exp = ErrorLog.WriteEventLog(_ent);

           return _exp;
       }
    }
}
