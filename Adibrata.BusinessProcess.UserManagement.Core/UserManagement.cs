using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Security;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Adibrata.BusinessProcess.UserManagement.Core
{
    public class UserManagement
    {

        static string Connectionstring = AppConfig.Config("ConnectionString");
        static string _coyName = AppConfig.Config("CoyName");
        Boolean _isvalid;
        public virtual Boolean UserNamePasswordVerification(UserManagementEntities _ent)
        {
            string _passwordstore, _passwordentry;
         
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("Select Password from MS_User With (nolock) where UserName = @UserName");

                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.UserName;

                _passwordstore = (string)SqlHelper.ExecuteScalar(Connectionstring, CommandType.Text, sb.ToString(), sqlParams);

                _passwordentry = Encryption.EncryptToSHA3(_ent.Password) + Encryption.EncryptToSHA3(_coyName);
                if (_passwordstore == _passwordentry && _passwordentry != "")
                {
                    _isvalid = true;
                }
                else
                {
                    _isvalid = false;
                }
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

            return _isvalid;

        }


    }
}

