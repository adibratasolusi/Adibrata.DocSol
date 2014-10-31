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

        public static void UserMangementAddEdit (UserManagementEntities _ent)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
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
            SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, "spUserManagementAddEdit");


        }
    }
}
