using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Adibrata.Framework.Caching;
namespace Adibrata.BusinessProcess.UserManagement.Core
{
    public class MainMenu
    {

        static string Connectionstring = AppConfig.Config("ConnectionString");
        SqlTransaction _trans;
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
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Core",
                    ClassName = "MainMenu",
                    FunctionName = "MainMenuGetActive",
                    ExceptionNumber = 1,
                    EventSource = "MainMenuGetActive",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
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
                sqlParams[0] = new SqlParameter("@menuParentId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.MenuParentId;
                sqlParams[1] = new SqlParameter("@shortOrder", SqlDbType.Int);
                sqlParams[1].Value = _ent.ShortOrder;
                sqlParams[2] = new SqlParameter("@menuTxt", SqlDbType.VarChar, 50);
                sqlParams[2].Value = _ent.MenuTxt;
                sqlParams[3] = new SqlParameter("@isSeparator", SqlDbType.VarChar, 1);
                sqlParams[3].Value = _ent.IsSeparator;
                sqlParams[4] = new SqlParameter("@isActive", SqlDbType.VarChar, 1);
                sqlParams[4].Value = _ent.IsActive;
                sqlParams[5] = new SqlParameter("@form", SqlDbType.VarChar, 50);
                sqlParams[5].Value = _ent.Form;
                if (_ent.FlagInsert == true)
                {
                    SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, "spMsMenuInsert", sqlParams);
                }
                else
                {
                    sqlParams[6] = new SqlParameter("@menuItemId", SqlDbType.Int);
                    sqlParams[6].Value = _ent.MenuItemId;
                    SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, "spMsMenuUpdate", sqlParams);
                }

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Core",
                    ClassName = "MainMenu",
                    FunctionName = "MainMenuInsertUpdate",
                    ExceptionNumber = 1,
                    EventSource = "MainMenuInsert",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        public virtual DataTable MainMenuGetActiveItemID(UserManagementEntities _ent)
        {
            DataTable _dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("spMsMenuGetActiveMenuCmbBox");
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, sb.ToString()));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Core",
                    ClassName = "MainMenu",
                    FunctionName = "MainMenuGetActiveItemID",
                    ExceptionNumber = 1,
                    EventSource = "MainMenu",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _dt;
        }
        public virtual DataTable MainMenuGetActiveShortOrder(UserManagementEntities _ent)
        {
            DataTable _dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("spMsMenuGetShortOrder");
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@menuItemId", SqlDbType.Int);
                sqlParams[0].Value = _ent.MenuItemId;
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, sb.ToString(), sqlParams));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Core",
                    ClassName = "MainMenu",
                    FunctionName = "MainMenuGetActiveShortOrder",
                    ExceptionNumber = 1,
                    EventSource = "MainMenu",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _dt;
        }

        public virtual DataTable SearchEngineMenu(UserManagementEntities _ent)
        {
            DataTable _dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@Crit", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.Form;
                sb.Append("spMenuSearch");
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, sb.ToString(),sqlParams));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Core",
                    ClassName = "MainMenu",
                    FunctionName = "SearchEngineMenu",
                    ExceptionNumber = 1,
                    EventSource = "MainMenu",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _dt;
        }

        #region "Menu Tree"
        public virtual DataTable MenuTreeRetrieve(UserManagementEntities _ent)
        {
            DataTable _dt = new DataTable();
            
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@MenuName", SqlDbType.VarChar,50);
                sqlParams[0].Value = _ent.MenuName;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure,"spMenuTreeGetItem", sqlParams));
          
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "MainMenu",
                    FunctionName = "MenuTreeRetrieve",
                    ExceptionNumber = 1,
                    EventSource = "MainMenuGetActive",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _dt;
        }
        
        public virtual String MenuTreeGetURL(UserManagementEntities _ent)
        {
            String _menuurl = "";
            
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@MenuName", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.MenuName;

                
                if (!DataCache.Contains(_ent.MenuName))
                {
                    _menuurl = SqlHelper.ExecuteScalar(Connectionstring, CommandType.StoredProcedure, "spMenuTreeGetUrl", sqlParams).ToString();
                    DataCache.Insert<String>(_ent.MenuName, _menuurl);
                }
                else
                {
                    _menuurl = DataCache.Get<String>(_ent.MenuName);
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "MainMenu",
                    FunctionName = "MenuTreeRetrieve",
                    ExceptionNumber = 1,
                    EventSource = "MainMenuGetActive",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _menuurl;
        }

        public virtual DataTable MenuTreeGetDetail (UserManagementEntities _ent)
        {
           
            DataTable _dt = new DataTable();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@MenuName", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.MenuName;
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spMenuTreeGetDetail", sqlParams));

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "MainMenu",
                    FunctionName = "MenuTreeRetrieve",
                    ExceptionNumber = 1,
                    EventSource = "MainMenuGetActive",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _dt;
        }

        public virtual DataTable MenuTreeGetFormList(UserManagementEntities _ent)
        {

            DataTable _dt = new DataTable();
            try
            {
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spMenuTreeListForm"));

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "MainMenu",
                    FunctionName = "MenuTreeGetFormList",
                    ExceptionNumber = 1,
                    EventSource = "MainMenu",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _dt;
        }

        public virtual void MenuTreeSave(UserManagementEntities _ent)
        {

            SqlConnection _conn = new SqlConnection(Connectionstring);
            SqlParameter[] sqlParams;
            
            sqlParams = new SqlParameter[5];

            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };


                #region "List Parameter SQL"
                sqlParams[0] = new SqlParameter("@FormName", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.FormName;
                sqlParams[1] = new SqlParameter("@ParentLevel", SqlDbType.BigInt);
                sqlParams[1].Value = _ent.ParentLevelMenu;

                sqlParams[2] = new SqlParameter("@MenuName", SqlDbType.VarChar, 50);
                sqlParams[2].Value = _ent.MenuName;
                sqlParams[3] = new SqlParameter("@MenuOrder", SqlDbType.SmallInt);
                sqlParams[3].Value = _ent.MenuOrder;
                sqlParams[4] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                sqlParams[4].Value = _ent.UserLogin;
                #endregion 
                _trans = _conn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spMenuTreeSave", sqlParams);
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
                    ClassName = "UserRegister",
                    FunctionName = "UserRegisterAddEdit",
                    ExceptionNumber = 1,
                    EventSource = "UserRegistration",
                    ExceptionObject = _exp,
                    EventID = 70, // 80 Untuk Approval
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
    
        #endregion 

    }
}
