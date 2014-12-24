using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using log4net; 
using System.Reflection;
using Adibrata.BusinessProcess.Entities.Base;

namespace Adibrata.Framework.Logging
{

    public static class ErrorLog
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public enum State {Normal, Error, Debug};
        
        public static void InitiateLog()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static Exception WriteEventLog(ErrorLogEntities _ent)
        {
            Exception _exp = new Exception();
            StringBuilder sb = new StringBuilder();
            sb.Append("Login User    : "); sb.AppendLine(_ent.UserLogin);
            sb.Append("Namespace     : "); sb.AppendLine(_ent.NameSpace);
            sb.Append("ClassName     : "); sb.AppendLine(_ent.ClassName);
            sb.Append("Function Name : "); sb.AppendLine(_ent.FunctionName);
            sb.Append("Event Number  : "); sb.AppendLine(_ent.ExceptionNumber.ToString());
            sb.AppendLine("Event Message  : ");sb.AppendLine(_ent.ExceptionDescription);
            sb.AppendLine("Exception Message : ");
            if (_ent.ExceptionObject != null)
            { sb.AppendLine(_ent.ExceptionObject.Message); }
            if (_exp.InnerException != null)
            {
                sb.AppendLine ("Inner Exception : ");
                sb.AppendLine(_exp.InnerException.ToString());
            }
            if (!EventLog.SourceExists(_ent.EventSource)) EventLog.CreateEventSource(_ent.EventSource, "Application");
            EventLog.WriteEntry(_ent.EventSource, sb.ToString(), EventLogEntryType.Error, _ent.EventID);
            logger.Error(sb.ToString());
            
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append("Error Occured in Namespace : "); sbMessage.Append(_ent.NameSpace); sbMessage.Append(" and Class : "); sbMessage.AppendLine(_ent.ClassName);
            sbMessage.AppendLine("Please See Event Log For Detail Information");
            throw new Exception (sbMessage.ToString());
        }

        public static void WriteEventLog(Boolean IsInfo, ErrorLogEntities _ent)
        {
            Exception _exp = new Exception();
            StringBuilder sb = new StringBuilder();
            sb.Append("Login User    : "); sb.AppendLine(_ent.UserLogin);
            sb.Append("Namespace     : "); sb.AppendLine(_ent.NameSpace);
            sb.Append("ClassName     : "); sb.AppendLine(_ent.ClassName);
            sb.Append("Function Name : "); sb.AppendLine(_ent.FunctionName);
            sb.Append("Event Number  : "); sb.AppendLine(_ent.ExceptionNumber.ToString());
            sb.AppendLine("Event Message     : "); sb.AppendLine(_ent.ExceptionDescription);
            if (!EventLog.SourceExists(_ent.EventSource)) EventLog.CreateEventSource(_ent.EventSource, "Application");
            EventLog.WriteEntry(_ent.EventSource, sb.ToString(), EventLogEntryType.Information, _ent.EventID);
            logger.Error(sb.ToString());

            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append("Error Occured in Namespace : "); sbMessage.Append(_ent.NameSpace); sbMessage.Append(" and Class : "); sbMessage.AppendLine(_ent.ClassName);
            sbMessage.AppendLine("Please See Event Log For Detail Information");
        }
    }
}
