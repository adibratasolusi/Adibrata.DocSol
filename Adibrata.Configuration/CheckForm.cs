using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Caching;
using Adibrata.Framework.Logging;
using System.Data.SqlClient;
using System.Data;

namespace Adibrata.Configuration
{
   public static class CheckForm
    {
       private static string ConnectionString = AppConfig.Config("ConnectionString");
       public static Boolean CheckFormSecurity (string UserName, string FormID)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("Select 1 from ");
           Boolean _value=false;

            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@UserName", SqlDbType.VarChar,50);
                sqlParams[0].Value = UserName;
                sqlParams[1] = new SqlParameter("@FormCode", SqlDbType.VarChar,50);
                sqlParams[1].Value = FormID;
                SqlHelper.ExecuteNonQuery(ConnectionString,CommandType.StoredProcedure, "sp");
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "",
                    NameSpace = "Adibrata.Configuration",
                    ClassName = "CheckFormSecurity",
                    FunctionName = "CheckFormSecurity",
                    ExceptionNumber = 1,
                    EventSource = "Configuration",
                    ExceptionObject = _exp,
                    EventID = 10, // 10 Untuk Configuration
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
           return _value;
       }
    }
}
