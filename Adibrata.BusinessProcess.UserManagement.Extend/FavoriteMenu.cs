using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Configuration;
using System.Data.SqlClient;
using System.Data;
using Adibrata.Framework.Logging;
using Adibrata.Framework.DataAccess;

namespace Adibrata.BusinessProcess.UserManagement.Extend
{
    public class FavoriteMenu : Adibrata.BusinessProcess.UserManagement.Core.FavoriteMenu
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");

        SqlTransaction _trans;

        public virtual void FavoriteMenuAdd (UserManagementEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;
            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@FormUrl", SqlDbType.VarChar, 255);
                sqlParams[0].Value = _ent.FormURL;
                sqlParams[1] = new SqlParameter("@UserLogin", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.UserLogin;
                
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spFavoriteMenuAdd", sqlParams);
                _trans.Commit();
            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "FavoriteMenu",
                    FunctionName = "FavoriteMenuAdd",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);

            }
        }

        public virtual Boolean FavoriteMenuDisable(UserManagementEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;
            Boolean _enabled = false;
            try
            {
                sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@FormUrl", SqlDbType.VarChar, 255);
                sqlParams[0].Value = _ent.FormURL;
                sqlParams[1] = new SqlParameter("@UserLogin", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.UserLogin;
                int _value = (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spFavoriteMenuDisabled", sqlParams);
                if (_value == 1)
                {
                    _enabled = false;
                }
                else
                {
                    _enabled = true;
                }
                
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "FavoriteMenu",
                    FunctionName = "FavoriteMenuDisable",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);

            }
            return _enabled;
        }

        public virtual DataTable FavoriteMenuList(UserManagementEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;
            DataTable _dt = new DataTable();
            try
            {
                
                sqlParams = new SqlParameter[1];
          
                sqlParams[0] = new SqlParameter("@UserLogin", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.UserLogin;

                _dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spFavoriteMenuList", sqlParams));
                
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "FavoriteMenu",
                    FunctionName = "FavoriteMenuList",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);

            }
            return _dt;
        }


    }
}
