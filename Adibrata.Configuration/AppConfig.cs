using Adibrata.Framework.Caching;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Adibrata.Configuration
{
    public static class AppConfig
    {
        public const string ConnectionString = "Server=Maximus-PC;Database=Configuration;User Id=sa;Password=Alpha2014";
        public static string Config(string key)
        {
            string value = "";


            //const string ConnectionString = "Server=MAXIMUS-PC;Database=Configuration;User Id=sa;Password=Alpha2014";

            string SQLSyntax = "Select value from [App.Config] with (nolock) where [key] = @Key";
            try
            {
                if (!DataCache.Contains(key))
                {
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@Key", SqlDbType.VarChar, 500);
                    sqlParams[0].Value = key;
                    value = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, SQLSyntax, sqlParams).ToString();
                    DataCache.Insert<string>(key, value, DataCache.Duration.Day, 1);
                }
                else
                {
                    value = DataCache.Get<string>(key);
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "",
                    NameSpace = "Adibrata.Configuration",
                    ClassName = "AppConfig",
                    FunctionName = "Config",
                    ExceptionNumber = 1,
                    EventSource = "Configuration",
                    ExceptionObject = _exp,
                    EventID = 10, // 10 Untuk Configuration
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return value;
        }
    }
}
