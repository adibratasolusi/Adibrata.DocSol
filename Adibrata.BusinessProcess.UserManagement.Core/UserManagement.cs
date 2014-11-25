using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
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
        public virtual Boolean UserNamePasswordVerification(UserManagementEntities _ent)
        {
            string _passwordstore, _passwordentry;
            Boolean _isvalid;
            StringBuilder sb = new StringBuilder();
            sb.Append("Select Password from MSUser With (nolok) where UserName = @UserName");

            SqlParameter[] sqlParams = new SqlParameter[0];
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
            return _isvalid;

        }


    }
}

