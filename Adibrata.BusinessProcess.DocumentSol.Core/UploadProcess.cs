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

        public virtual string PathInsert(DocSolEntities _ent)
        {
            string pathId = "";
            try
            {
                DataTable _dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@agrmntNo", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.AgrmntNo;
                sqlParams[1] = new SqlParameter("@docType", SqlDbType.VarChar,50);
                sqlParams[1].Value = _ent.DocType;
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spPathInsert", sqlParams));
                pathId = _dt.Rows[0]["PATH_ID"].ToString(); 

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "MainMenuInsertUpdate",
                    ExceptionNumber = 1,
                    EventSource = "MainMenuInsertUpdate",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
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

    }
}
