using Adibrata.Framework.Logging;
using System;
using System.Data;

namespace Adibrata.Framework.Messaging
{
    public static class MessageToWCF
    {
        public static void UpdateFilePath(WCFEntities oWCF)
        {


            Adibrata.Framework.Messaging.ServiceWCF.ServiceWCFClient objService = new Adibrata.Framework.Messaging.ServiceWCF.ServiceWCFClient();
            
            try
            {

                Adibrata.Framework.Messaging.ServiceWCF.PathDetails pathInfo = new Adibrata.Framework.Messaging.ServiceWCF.PathDetails();
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


            Adibrata.Framework.Messaging.ServiceWCF.ServiceWCFClient objService = new Adibrata.Framework.Messaging.ServiceWCF.ServiceWCFClient();
            

            try
            {

                Adibrata.Framework.Messaging.ServiceWCF.DocTrans oDocTrans = new Adibrata.Framework.Messaging.ServiceWCF.DocTrans();
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


        public static DataTable DocTransContentDetail(WCFEntities oWCF)
        {
            DataTable _dt = new DataTable();

            Adibrata.Framework.Messaging.ServiceWCF.ServiceWCFClient objService = new Adibrata.Framework.Messaging.ServiceWCF.ServiceWCFClient();


            try
            {

                Adibrata.Framework.Messaging.ServiceWCF.DocTrans oDocTrans = new Adibrata.Framework.Messaging.ServiceWCF.DocTrans();
                oDocTrans.DocTransCode = oWCF.DocTransCode;
                oDocTrans.UserName = oWCF.UserName;
                _dt = objService.DocTransContentDetail(oDocTrans);

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

            return _dt;
        }
        

        public static DataTable DocTransInquiryDetail(WCFEntities oWCF)
        {
            DataTable _dt = new DataTable();

            Adibrata.Framework.Messaging.ServiceWCF.ServiceWCFClient objService = new Adibrata.Framework.Messaging.ServiceWCF.ServiceWCFClient();


            try
            {

                Adibrata.Framework.Messaging.ServiceWCF.DocTrans oDocTrans = new Adibrata.Framework.Messaging.ServiceWCF.DocTrans();
                oDocTrans.DocTransCode = oWCF.DocTransCode;
                oDocTrans.UserName = oWCF.UserName;
                _dt = objService.DocTransInquiryDetail(oDocTrans);

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

            return _dt;
        }


        public static DataTable DocTransApproval(WCFEntities oWCF)
        {
            DataTable _dt = new DataTable();

            Adibrata.Framework.Messaging.AdibrataWF.Service1Client objService = new Adibrata.Framework.Messaging.AdibrataWF.Service1Client();


            try
            {

                Adibrata.Framework.Messaging.AdibrataWF.EntitiesBase oEnt = new Adibrata.Framework.Messaging.AdibrataWF.EntitiesBase();
                oEnt.BranchId = oWCF.BranchId;
                oEnt.RoleId = oWCF.RoleId;
                oEnt.Status = oWCF.Status;
                _dt = objService.GetDataCust(oEnt);

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "WCF",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToWCF",
                    FunctionName = "DocTransApproval",
                    ExceptionNumber = 1,
                    EventSource = "Messaging",
                    ExceptionObject = _exp,
                    EventID = 201,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _dt;
        }


        public static String DocTransApprovalGetNextStep()
        {
            String result = "";
            Adibrata.Framework.Messaging.AdibrataWF.Service1Client objService = new Adibrata.Framework.Messaging.AdibrataWF.Service1Client();
            try
            {
                result = objService.GetNextStep();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "WCF",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToWCF",
                    FunctionName = "DocTransApproval",
                    ExceptionNumber = 1,
                    EventSource = "Messaging",
                    ExceptionObject = _exp,
                    EventID = 201,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return result;
        }

    }
}
