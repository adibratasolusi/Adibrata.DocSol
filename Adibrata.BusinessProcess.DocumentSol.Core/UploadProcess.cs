using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.BusinessProcess.DocumentSol.Core
{
    public class UploadProcess
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");
        public virtual DataTable DocMasterGetActive(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("spDocMasterGetActive");
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, sb.ToString()));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "DocMasterGetActive",
                    ExceptionNumber = 1,
                    EventSource = "DocMasterGetActive",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _dt;
        }

        //public void UpdatePathDetails(PathDetails pathInfo)
        //{


        //    string bitsServer = AppConfig.Config("BITSServer");
        //    var webClient = new WebClient();
        //    byte[] fileBytes = webClient.DownloadData(bitsServer + pathInfo.FileName);
        //    Image img = byteArrayToImage(fileBytes);
        //    pathInfo.DPI = img.HorizontalResolution.ToString();
        //    pathInfo.Pixel = img.Width + "x" + img.Height;
        //    pathInfo.SizeFileBytes = fileBytes.Length;
        //    pathInfo.DateCreated = getImageDtCreate(img);
        //    string strMessage = string.Empty;
        //    SqlConnection con = new SqlConnection(Connectionstring);
        //    int result = 0;
        //    try
        //    {

        //        SqlCommand command = new SqlCommand("spUpdatePath", con);
        //        command.CommandType = CommandType.StoredProcedure;

        //        command.Parameters.Add("@DocTransID", SqlDbType.BigInt).Value = pathInfo.DocTransID;
        //        command.Parameters.Add("@FileName", SqlDbType.VarChar).Value = pathInfo.FileName;
        //        command.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = pathInfo.DateCreated;
        //        command.Parameters.Add("@SizeFileBytes", SqlDbType.Decimal).Value = pathInfo.SizeFileBytes;
        //        command.Parameters.Add("@Pixel", SqlDbType.VarChar).Value = pathInfo.Pixel;
        //        command.Parameters.Add("@ComputerName", SqlDbType.VarChar).Value = pathInfo.ComputerName;
        //        command.Parameters.Add("@DPI", SqlDbType.VarChar).Value = pathInfo.DPI;
        //        command.Parameters.Add("@FileBinary", SqlDbType.VarBinary).Value = fileBytes;
        //        con.Open();
        //        result = command.ExecuteNonQuery();
        //        con.Close();

        //        if (result == 1)
        //        {

        //            //logging success here
        //        }
        //        else
        //        {

        //            //logging error db here
        //        }
        //    }
        //    catch (Exception _exp)
        //    {
        //        //logging app here
        //        ErrorLogEntities _errent = new ErrorLogEntities
        //        {
        //            UserLogin = SessionProperty.UserName,
        //            NameSpace = "Adibrata.Framework.WCF",
        //            ClassName = "Service1",
        //            FunctionName = "UpdatePathDetails",
        //            ExceptionNumber = 1,
        //            EventSource = "UploadServices",
        //            ExceptionObject = _exp,
        //            EventID = 200, // 1 Untuk Framework 
        //            ExceptionDescription = _exp.Message
        //        };
        //        ErrorLog.WriteEventLog(_errent);
        //    }

        //}

        public virtual string DocTransInsert(DocSolEntities _ent)
        {
            string pathId = "";
            try
            {
                DataTable _dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@TransId", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.TransId;
                sqlParams[1] = new SqlParameter("@docType", SqlDbType.VarChar,50);
                sqlParams[1].Value = _ent.DocumentType;
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransInsert", sqlParams));
                pathId = _dt.Rows[0]["Id"].ToString(); 

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "PathInsert",
                    ExceptionNumber = 1,
                    EventSource = "PathInsert",
                    ExceptionObject = _exp,
                    EventID = 201, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return pathId;

        }

        public virtual DataTable AgreementGetInfo(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            try
            {
                
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@agreementNo", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.AgrmntNo;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spAgreementGetAgreementInfo", sqlParams));

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "AgreementGetInfo",
                    ExceptionNumber = 1,
                    EventSource = "AgreementGetInfo",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dt;

        }


        public virtual DataTable PathGetView(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[0];
                sqlParams[0] = new SqlParameter("@agrmntNo", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.AgrmntNo;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spPathView", sqlParams));


            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "PathGetView",
                    ExceptionNumber = 1,
                    EventSource = "PathGetView",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dt;

        }


        public virtual DataTable PagingAgreement(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@by", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.By;
                sqlParams[1] = new SqlParameter("@crit", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.Crit;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spPagingAgreement", sqlParams));


            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "PagingAgreement",
                    ExceptionNumber = 1,
                    EventSource = "PagingAgreement",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dt;

        }

        public virtual string DocTransBinaryInsert(DocSolEntities _ent)
        {
            string Id = "";
            try
            {

                DataTable _dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@DocTransID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.DocTransId;
                sqlParams[1] = new SqlParameter("@FileName", SqlDbType.VarChar, 8000);
                sqlParams[1].Value = _ent.FileName;
                sqlParams[2] = new SqlParameter("@DateCreated", SqlDbType.DateTime);
                sqlParams[2].Value = _ent.DateCreated;
                sqlParams[3] = new SqlParameter("@SizeFileBytes", SqlDbType.Decimal);
                sqlParams[3].Value = _ent.SizeFileBytes;
                sqlParams[4] = new SqlParameter("@Pixel", SqlDbType.VarChar, 100);
                sqlParams[4].Value = _ent.Pixel;
                sqlParams[5] = new SqlParameter("@ComputerName", SqlDbType.VarChar, 100);
                sqlParams[5].Value = _ent.ComputerName;
                sqlParams[6] = new SqlParameter("@DPI", SqlDbType.VarChar, 100);
                sqlParams[6].Value = _ent.DPI;
               // _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransInsert", sqlParams));
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransBinaryInsert", sqlParams));
                Id = _dt.Rows[0]["Id"].ToString();

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "PathInsert",
                    ExceptionNumber = 1,
                    EventSource = "PathInsert",
                    ExceptionObject = _exp,
                    EventID = 201, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return Id;

        }

        public virtual void DocTransBinaryUpdate(DocSolEntities _ent)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@Id", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                sqlParams[1] = new SqlParameter("@FileBinary", SqlDbType.VarBinary);
                sqlParams[1].Value = _ent.FileBinary;
                SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, "spDocTransBinaryUpdate", sqlParams);

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "PathInsert",
                    ExceptionNumber = 1,
                    EventSource = "PathInsert",
                    ExceptionObject = _exp,
                    EventID = 201, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

    }
}
