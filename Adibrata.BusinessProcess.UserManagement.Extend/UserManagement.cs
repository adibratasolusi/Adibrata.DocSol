using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Adibrata.BusinessProcess.UserManagement.Extend
{
    public class UserManagement:Adibrata.BusinessProcess.UserManagement.Core.UserManagement
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");
        public virtual DataTable MainMenuGetActive(UserManagementEntities _ent)
        {
            DataTable _dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("spMsMenuGetActiveMenu");
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, sb.ToString()));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "UserManagement",
                    FunctionName = "MainMenuGetActive",
                    ExceptionNumber = 1,
                    EventSource = "MainMenuGetActive",
                    ExceptionObject = _exp,
                    EventID = 70, // 70 Untuk Usermanagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _dt;
        }

        public virtual void MainMenuInsertUpdate(UserManagementEntities _ent)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@menuItemId", SqlDbType.Int);
                sqlParams[0].Value = _ent.MenuItemId;
                sqlParams[1] = new SqlParameter("@menuParentId", SqlDbType.Int);
                sqlParams[1].Value = _ent.MenuParentId;
                sqlParams[2] = new SqlParameter("@shortOrder", SqlDbType.Int);
                sqlParams[2].Value = _ent.ShortOrder;
                sqlParams[3] = new SqlParameter("@menuTxt", SqlDbType.VarChar,50);
                sqlParams[3].Value = _ent.MenuTxt;
                sqlParams[4] = new SqlParameter("@isSeparator", SqlDbType.VarChar,1);
                sqlParams[4].Value = _ent.IsSeparator;
                sqlParams[5] = new SqlParameter("@isActive", SqlDbType.VarChar,1);
                sqlParams[5].Value = _ent.IsActive;
                sqlParams[6] = new SqlParameter("@form", SqlDbType.VarChar,50);
                sqlParams[6].Value = _ent.Form;
                if (_ent.FlagInsert == true)
                {
                    SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, "spMsMenuInsert", sqlParams);
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, "spMsMenuUpdate", sqlParams);
                }
                
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.Paging.Core.UserManagement",
                    ClassName = "UserRegisterPaging",
                    FunctionName = "MainMenuInsert",
                    ExceptionNumber = 1,
                    EventSource = "MainMenuInsert",
                    ExceptionObject = _exp,
                    EventID = 70, // 70 Untuk Usermanagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        public virtual DataTable MsUserMenuGetByUserId(UserManagementEntities _ent)
        {

            DataTable _dt = new DataTable();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@UserId ", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.UserID;
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spMsUserMenuGetByUserId", sqlParams));
                

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "UserManagement",
                    FunctionName = "MsUserMenuGetByUserId",
                    ExceptionNumber = 1,
                    EventSource = "UserManagement",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dt;
        }


        SqlTransaction _trans;
        public virtual void spMSUserMenuInsert(UserManagementEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(Connectionstring);
            SqlParameter[] sqlParams;
            
            try
            {
                
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();


                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@UserId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.UserID;
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spMsUserMenuDeleteBeforeInsert", sqlParams);

                sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@UserId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.UserID;
                sqlParams[1] = new SqlParameter("@FormId", SqlDbType.BigInt);
                sqlParams[1].Value = _ent.FormID;
                sqlParams[2] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                sqlParams[2].Value = _ent.UserLogin;
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spMSUserMenuInsert", sqlParams);
                

                _trans.Commit();


            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "UserManagement",
                    FunctionName = "spMSUserMenuInsert",
                    ExceptionNumber = 1,
                    EventSource = "UserManagement",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk DocumentManagement
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
