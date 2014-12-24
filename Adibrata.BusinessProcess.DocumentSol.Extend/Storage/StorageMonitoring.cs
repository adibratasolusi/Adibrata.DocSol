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

namespace Adibrata.BusinessProcess.DocumentSol.Extend
{
    public class StorageMonitoring : Adibrata.BusinessProcess.DocumentSol.Core.StorageMonitoring
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");

        public DataTable StorageSizeDetail(DocSolEntities _ent)
        {
            SqlParameter[] sqlParams;
            DataTable dt = new DataTable();
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@Ext", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.Ext;


                dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spSummarySizeStorageByExt", sqlParams));


                
                #endregion
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                    ClassName = "StorageMonitoring",
                    FunctionName = "StorageSizeDetail",
                    ExceptionNumber = 1,
                    EventSource = "StorageDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            return dt;
        }

        public DataTable StorageExtDetail(DocSolEntities _ent)
          {

           SqlParameter[] sqlParams;
           DataTable dt= new DataTable();
           try
           {
               #region "List Parameter SQL"
               sqlParams = new SqlParameter[1];
               sqlParams[0] = new SqlParameter("@Ext", SqlDbType.VarChar, 50);
               sqlParams[0].Value = _ent.Ext;


               dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spSummarySizeDetail", sqlParams));


               #endregion
           }
           catch (Exception _exp)
           {
               #region "Write to Event Viewer"
               ErrorLogEntities _errent = new ErrorLogEntities
               {
                   UserLogin = _ent.UserLogin,
                   NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                   ClassName = "StorageMonitoring",
                   FunctionName = "StorageExtDetail",
                   ExceptionNumber = 1,
                   EventSource = "StorageDetail",
                   ExceptionObject = _exp,
                   EventID = 200, // 80 Untuk DocumentManagement
                   ExceptionDescription = _exp.Message
               };
               ErrorLog.WriteEventLog(_errent);
               #endregion
           }
           return dt;
       }

        public DataTable SummarySize(DocSolEntities _ent)
        {

          
            DataTable dt = new DataTable();
            try
            {
                #region "List Parameter SQL"


                dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spSummarySizeStorage"));


                #endregion
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                    ClassName = "StorageMonitoring",
                    FunctionName = "StorageExtDetail",
                    ExceptionNumber = 1,
                    EventSource = "StorageDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            return dt;
        }

        public DataTable SummarySizeDetail(DocSolEntities _ent)
        {
            SqlParameter[] sqlParams;
            DataTable dt = new DataTable();
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@Id", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;


                dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spSummarySizeDetailById", sqlParams));


                #endregion
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                    ClassName = "StorageMonitoring",
                    FunctionName = "StorageExtDetail",
                    ExceptionNumber = 1,
                    EventSource = "StorageDetail",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            return dt;

        }


    }
}
