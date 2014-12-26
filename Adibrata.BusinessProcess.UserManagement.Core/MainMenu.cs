﻿using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Adibrata.BusinessProcess.UserManagement.Core
{
    public class MainMenu
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
                sqlParams[0] = new SqlParameter("@menuParentId", SqlDbType.Int);
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

        public virtual DataTable MenuTreeRetrieve(UserManagementEntities _ent)
        {
            DataTable _dt = new DataTable();
            SqlDataReader _dr;
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@CurrentLevel", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.MenuLevel;

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

    }
}
