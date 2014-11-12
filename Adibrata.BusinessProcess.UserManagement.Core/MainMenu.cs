﻿using Adibrata.BusinessProcess.UserManagement.Entities;
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
                _dt = (DataTable)SqlHelper.ExecuteDataset(Connectionstring, CommandType.StoredProcedure, sb.ToString()).Tables[0];
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "UserManagement",
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
                sqlParams[0] = new SqlParameter("@menuItemId", SqlDbType.Int);
                sqlParams[0].Value = _ent.MenuItemId;
                sqlParams[1] = new SqlParameter("@menuParentId", SqlDbType.Int);
                sqlParams[1].Value = _ent.MenuParentId;
                sqlParams[2] = new SqlParameter("@shortOrder", SqlDbType.Int);
                sqlParams[2].Value = _ent.ShortOrder;
                sqlParams[3] = new SqlParameter("@menuTxt", SqlDbType.VarChar, 50);
                sqlParams[3].Value = _ent.MenuTxt;
                sqlParams[4] = new SqlParameter("@isSeparator", SqlDbType.VarChar, 1);
                sqlParams[4].Value = _ent.IsSeparator;
                sqlParams[5] = new SqlParameter("@isActive", SqlDbType.VarChar, 1);
                sqlParams[5].Value = _ent.IsActive;
                sqlParams[6] = new SqlParameter("@form", SqlDbType.VarChar, 50);
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
                    UserName = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.Paging.Core.UserManagement",
                    ClassName = "UserRegisterPaging",
                    FunctionName = "MainMenuInsert",
                    ExceptionNumber = 1,
                    EventSource = "MainMenuInsert",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

    }
}