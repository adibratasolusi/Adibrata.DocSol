using Adibrata.Framework.Logging;
using Adibrata.Framework.Messaging.ServiceReference1;
using System;

namespace Adibrata.Framework.Messaging
{
    public static class MessageToWCF
    {
        public static void UpdateFilePath(WCFEntities oWCF)
        {

            ServiceReference1.Service1Client objService = new ServiceReference1.Service1Client();
            try
            {
                PathDetails pathInfo = new PathDetails();
                pathInfo.Ext = oWCF.DicExtString;
                pathInfo.FileName = oWCF.DicFileString;
                objService.UpdatePathDetails(pathInfo);
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "WCF",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToWCF",
                    FunctionName = "UpdateFilePath",
                    ExceptionNumber = 1,
                    EventSource = "Messaging",
                    ExceptionObject = _exp,
                    EventID = 1,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }
    }
}
