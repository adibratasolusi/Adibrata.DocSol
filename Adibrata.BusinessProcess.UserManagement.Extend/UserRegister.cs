using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Adibrata.BusinessProcess.UserManagement.Extend
{
    public class UserRegister : Adibrata.BusinessProcess.UserManagement.Core.UserRegister
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");
        SqlTransaction _trans;
        public virtual DataTable UserRegisterListReportData(UserManagementEntities _ent)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spUserRegisterListReport"));

            return dt;
        }

        public virtual void UserRegisterAddEdit (UserManagementEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;
            if (_ent.IsEdit == true) { sqlParams = new SqlParameter[28]; } else { sqlParams = new SqlParameter[27]; }
            try
            {
                #region "List Parameter SQL"
                sqlParams[0] = new SqlParameter("@AprvSchemeCode", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.ApprovalSchemeCode;
                #endregion 

                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                if (_ent.IsEdit == true)
                {
                    sqlParams[28] = new SqlParameter("@ApprSchemeID", SqlDbType.BigInt);
                    sqlParams[28].Value = _ent.ApprovalShemeID;

                    SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spApprSchemeEdit", sqlParams);
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spApprSchemeAdd", sqlParams);
                }
                _trans.Commit();

            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "UserRegister",
                    FunctionName = "UserRegisterAddEdit",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalSchemeList",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Approval
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
