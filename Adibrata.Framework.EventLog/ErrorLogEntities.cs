using Adibrata.BusinessProcess.Entities.Base;
using System;
namespace Adibrata.Framework.Logging
{
    [Serializable]    
    public class ErrorLogEntities : EntitiesBase
    {
       public string NameSpace { get; set; }
        public string ExceptionDescription { get; set; }
        public int ExceptionNumber { get; set; }
        public int EventID { get; set; }
        public string EventSource { get; set; }
        public Exception ExceptionObject { get; set; }
        public string FunctionName { get; set;  }
    }
}
