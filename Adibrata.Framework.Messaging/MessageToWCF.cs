using Adibrata.Framework.Logging;
using System;

namespace Adibrata.Framework.Messaging
{
    public static class MessageToWCF
    {
        public static void UpdateFilePath(WCFEntities oWCF)
        {


            Adibrata.Framework.Messaging.ServiceReference1.Service1Client objService = new Adibrata.Framework.Messaging.ServiceReference1.Service1Client();
            
            try
            {

                Adibrata.Framework.Messaging.ServiceReference1.PathDetails pathInfo = new Adibrata.Framework.Messaging.ServiceReference1.PathDetails();
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

        public static void ArchieveExecProcess(WCFEntities oWCF)
        {


            Adibrata.Framework.Messaging.ServiceReference2.Service1Client objService = new Adibrata.Framework.Messaging.ServiceReference2.Service1Client();
            

            try
            {
                
                Adibrata.Framework.Messaging.ServiceReference2.DocTrans oDocTrans = new Adibrata.Framework.Messaging.ServiceReference2.DocTrans();
                oDocTrans.DocTransID = oWCF.DocTransID;
                oDocTrans.UserName = oWCF.UserName;
                objService.ArchieveExecutionProcess(oDocTrans);

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "WCF",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToWCF",
                    FunctionName = "ArchieveExecProcess",
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
