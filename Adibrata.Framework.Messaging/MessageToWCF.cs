using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Configuration;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace Adibrata.Framework.Messaging
{
    public static class MessageToWCF
    {
        public static void UpdateFilePath(WCFEntities oWCF)
        {

            SessionEntities SessionProperty = new SessionEntities();
            Adibrata.Framework.Messaging.ServiceReference1.Service1Client objService = new Adibrata.Framework.Messaging.ServiceReference1.Service1Client();

            //try
            //{

            string Connectionstring = AppConfig.Config("ConnectionString");
            oWCF.FileName = oWCF.FileName;
            oWCF.DocTransBinaryID = oWCF.DocTransBinaryID;



            string bitsServer = AppConfig.Config("BITSServer");
            var webClient = new WebClient();
            byte[] fileBytes = webClient.DownloadData(bitsServer + oWCF.FileName);
            oWCF.FileName = oWCF.DocTransBinaryID + oWCF.FileName;
            string strMessage = string.Empty;
            SqlConnection con = new SqlConnection(Connectionstring);
            int result = 0;
            try
            {

                SqlCommand command = new SqlCommand("spDocTransBinaryUpdate", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DocTransBinaryID", SqlDbType.BigInt).Value = oWCF.DocTransBinaryID;
                command.Parameters.Add("@FileName", SqlDbType.VarChar).Value = oWCF.FileName;
                command.Parameters.Add("@FileBinary", SqlDbType.VarBinary).Value = fileBytes;
                con.Open();
                result = command.ExecuteNonQuery();
                con.Close();

                if (result == 1)
                {

                    //logging success here
                }
                else
                {

                    //logging error db here
                }
            }
            catch (Exception _exp)
            {
                //logging app here
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.Framework.WCF",
                    ClassName = "Service1",
                    FunctionName = "UpdatePathDetails",
                    ExceptionNumber = 1,
                    EventSource = "UploadServices",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            //}
            //catch (Exception _exp)
            //{

            //    ErrorLogEntities _errent = new ErrorLogEntities
            //    {
            //        UserLogin = "WCF",
            //        NameSpace = "Adibrata.Framework.Messaging",
            //        ClassName = "MessageToWCF",
            //        FunctionName = "UpdateFilePath",
            //        ExceptionNumber = 1,
            //        EventSource = "Messaging",
            //        ExceptionObject = _exp,
            //        EventID = 201,
            //        ExceptionDescription = _exp.Message
            //    };
            //    ErrorLog.WriteEventLog(_errent);
            //}

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
