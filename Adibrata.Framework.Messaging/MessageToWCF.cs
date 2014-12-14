using Adibrata.Framework.Logging;
using Adibrata.Framework.Messaging.ServiceReference1;
using System;

namespace Adibrata.Framework.Messaging
{
    public static class MessageToWCF
    {
        public static void UpdateFilePath(WCFEntities oWCF)
        {
            
            
            Service1Client objService = new Service1Client();
            
            try
            {

                PathDetails pathInfo = new PathDetails();
                pathInfo.FileName = oWCF.FileName;
                pathInfo.DocTransBinaryID = oWCF.DocTransBinaryID;
                objService.UpdatePathDetails(pathInfo);
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "WCF",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToWCF",
                    FunctionName = "UpdateFilePath",
                    ExceptionNumber = 1,
                    EventSource = "Messaging",
                    ExceptionObject = _exp,
                    EventID = 201,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }
    }
}
