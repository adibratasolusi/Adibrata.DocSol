using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.BusinessProcess.Entities.Base;
using System.Data;
using System.Data.SqlClient;
using Adibrata.Configuration;

namespace Adibrata.BusinessProcess.DocumentSol.Extend
{
    public class LoginActivity
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");

        SqlTransaction _trans;

        public void LoginActivitySave(DocSolEntities _ent)
       {
           SqlConnection _conn = new SqlConnection(ConnectionString);
           SqlParameter[] sqlParams;

           try
           {
               if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
               _trans = _conn.BeginTransaction();
               sqlParams = new SqlParameter[11];
               sqlParams[0] = new SqlParameter("@FormUrl", SqlDbType.VarChar, 50);
               sqlParams[0].Value = _ent.FormPath;
               sqlParams[1] = new SqlParameter("@UserLogin", SqlDbType.VarChar, 20);
               sqlParams[1].Value = _ent.UserLogin;
               sqlParams[2] = new SqlParameter("@DateTimeAccess", SqlDbType.SmallDateTime);
               sqlParams[2].Value = DateTime.Now;
               SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spUserLoginActivitySave", sqlParams);
               _trans.Commit();
           }
           catch (Exception _exp)
           {
               _trans.Rollback();

               ErrorLogEntities _errent = new ErrorLogEntities
               {
                   UserLogin = _ent.UserLogin,
                   NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                   ClassName = "LoginActivity",
                   FunctionName = "LoginActivitySave",
                   ExceptionNumber = 1,
                   EventSource = "LoginActivity",
                   ExceptionObject = _exp,
                   EventID = 200, // 80 Untuk Framework 
                   ExceptionDescription = _exp.Message
               };
               ErrorLog.WriteEventLog(_errent);
           }
        }

    }
}
