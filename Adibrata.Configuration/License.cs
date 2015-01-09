using Adibrata.Framework.Caching;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using Adibrata.Framework.Security;

namespace Adibrata.Configuration
{
    public static class License
    {
        private static string ConnectionStringConfiguration = "Server=.;Database=Configuration;User Id=sa;Password=Alpha2014";
        private static string ConnectionString= AppConfig.Config("ConnectionString");
        public static bool UserLicense()
        {

            bool _valid = false;
            string key = "UserLicense";
            
            string _licensevalue, _currentdata;
            

            string SQLSyntax = "Select value from [App.Config] with (nolock) where [key] = @Key";
            try
            {
                if (!DataCache.Contains(key))
                {
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@Key", SqlDbType.VarChar, 500);
                    sqlParams[0].Value = "User";
                    _licensevalue = Encryption.DecryptFromRSA(SqlHelper.ExecuteScalar(ConnectionStringConfiguration, CommandType.Text, SQLSyntax, sqlParams).ToString());
                    DataCache.Insert<string>(key, _licensevalue, DataCache.Duration.Minutes, 5);
                }
                else
                {
                    _licensevalue = DataCache.Get<string>(key);
                }

                SQLSyntax = "Select Count(1) from MS_User with (nolock) where Active = 1 ";
                _currentdata = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, SQLSyntax).ToString();
                if (Convert.ToInt16(_licensevalue) >= Convert.ToInt16(_currentdata))
                {
                    _valid = true;
                }
                else
                {
                    _valid = false;
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
            return _valid;
        }

        public static bool DocumentUplad()
        {
            bool _valid = false;
            string key = "DocumentUploadLicense";
            
            string _licensevalue, _currentdata;
         
            string SQLSyntax = "Select value from [App.Config] with (nolock) where [key] = @Key";
            try
            {
                if (!DataCache.Contains(key))
                {
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@Key", SqlDbType.VarChar, 500);
                    sqlParams[0].Value = "DocumentUpload";
                    _licensevalue = Encryption.DecryptFromRSA(SqlHelper.ExecuteScalar(ConnectionStringConfiguration, CommandType.Text, SQLSyntax, sqlParams).ToString());
                    DataCache.Insert<string>(key, _licensevalue, DataCache.Duration.Minutes, 5);
                }
                else
                {
                    _licensevalue = DataCache.Get<string>(key);
                }

                SQLSyntax = "Select Count(1) from DocTrans with (nolock) where DocTransStaus <> 'CANCEL' ";
                _currentdata = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, SQLSyntax).ToString();
                if (Convert.ToInt16(_licensevalue) >= Convert.ToInt16(_currentdata))
                {
                    _valid = true;
                }
                else
                {
                    _valid = false;
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
            return _valid;
        }

        public static bool ActiveDate()
        {
            bool _valid = false;
            string key = "ActiveApplication";

            string _licensevalue;

            string SQLSyntax = "Select value from [App.Config] with (nolock) where [key] = @Key";
            try
            {
                if (!DataCache.Contains(key))
                {
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@Key", SqlDbType.VarChar, 500);
                    sqlParams[0].Value = "ActiveDate";
                    _licensevalue = Encryption.DecryptFromRSA(SqlHelper.ExecuteScalar(ConnectionStringConfiguration, CommandType.Text, SQLSyntax, sqlParams).ToString());
                    DataCache.Insert<string>(key, _licensevalue, DataCache.Duration.Minutes, 5);
                }
                else
                {
                    _licensevalue = DataCache.Get<string>(key);
                }
                DateTime test = Convert.ToDateTime(_licensevalue);
                DateTime test1 = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                if ( test == test1 )
                {
                    _valid = false;
                }
                else
                {
                    _valid = true;
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
            return _valid;
        }
    }
}
