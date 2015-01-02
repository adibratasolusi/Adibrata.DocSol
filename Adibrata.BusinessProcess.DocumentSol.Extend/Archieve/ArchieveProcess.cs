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
    public class ArchieveProcess: Adibrata.BusinessProcess.DocumentSol.Core.ArchieveProcess
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");
        SqlTransaction _trans;
         public virtual void ArchievePrepare(DocSolEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;

            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@DocTransCode", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.DocTransCode;

                #endregion

                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spArchievePrepare", sqlParams);
                _trans.Commit();

            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                    ClassName = "ArchieveProcess",
                    FunctionName = "ArchievePrepare",
                    ExceptionNumber = 1,
                    EventSource = "Archieve",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) { _conn.Close(); };
                _conn.Dispose();
            }
        }

         public virtual void ArchieveApproval(DocSolEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;

            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.DocTransId;
                sqlParams[1] = new SqlParameter("@Status", SqlDbType.Char, 1);
                sqlParams[1].Value = _ent.ApprovalStatus;

                #endregion

                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spArchieveApproval", sqlParams);
                _trans.Commit();

            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "ArchieveProcess",
                    FunctionName = "ArchieveApproval",
                    ExceptionNumber = 1,
                    EventSource = "Archieve",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) { _conn.Close(); };
                _conn.Dispose();
            }
        }

         public virtual void ArchievePreparelQueueProcess(DocSolEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;

            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                for (int i = 0; i < _ent.ListArchieve.Count; i++)
                {

                    #region "List Parameter SQL"
                    sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@DocTransCode", SqlDbType.VarChar,50);
                    sqlParams[0].Value = _ent.ListArchieve[i];

                    #endregion

                    SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spArchievePrepare", sqlParams);
                }
                _trans.Commit();

            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "ArchieveProcess",
                    FunctionName = "ArchievePreparelQueueProcess",
                    ExceptionNumber = 1,
                    EventSource = "Archieve",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) { _conn.Close(); };
                _conn.Dispose();
            }
        }

           public virtual void ArchieveApprovalQueueProcess(DocSolEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;

            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                for (int i = 0; i < _ent.ListArchieve.Count; i++)
                {

                    #region "List Parameter SQL"
                    sqlParams = new SqlParameter[2];
                    sqlParams[0] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                    sqlParams[0].Value = _ent.ListArchieve[i];
                    sqlParams[1] = new SqlParameter("@Status", SqlDbType.Char, 1);
                    sqlParams[1].Value = _ent.ApprovalStatus;

                    #endregion

                    SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spArchieveApproval", sqlParams);
                }
                _trans.Commit();

            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "ArchieveProcess",
                    FunctionName = "ArchieveApprovalQueueProcess",
                    ExceptionNumber = 1,
                    EventSource = "Archieve",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) { _conn.Close(); };
                _conn.Dispose();
            }
        }
    

    }
}
