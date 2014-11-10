using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Framework.Logging;
using Adibrata.Framework.DataAccess;
using Adibrata.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Adibrata.BusinessProcess.UserManagement.Core
{
    public class UserManagement
    {

        static string Connectionstring = AppConfig.Config("ConnectionString");

        public virtual DataTable MainMenuGetActive()
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
                    UserName = "Main",
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
        public virtual void UserMangementAddEdit (UserManagementEntities _ent)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
            sqlParams[0].Value = _ent.UserName; 
            sqlParams[1] = new SqlParameter("@Password", SqlDbType.VarChar, 300);
            sqlParams[1].Value = _ent.Password;
            sqlParams[2] = new SqlParameter("@MaxWrong", SqlDbType.Int);
            sqlParams[2].Value = _ent.MaxWrong;
            sqlParams[3] = new SqlParameter("@ExpiredDate", SqlDbType.DateTime);
            sqlParams[3].Value = _ent.ExpiredDate;
            sqlParams[4] = new SqlParameter("@IsActive", SqlDbType.Int);
            sqlParams[4].Value = _ent.IsActive;
            sqlParams[5] = new SqlParameter("@FullName", SqlDbType.VarChar,50);
            sqlParams[5].Value = _ent.FullName;
            SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, "spUserManagementAddEdit");
        }

    }
}
