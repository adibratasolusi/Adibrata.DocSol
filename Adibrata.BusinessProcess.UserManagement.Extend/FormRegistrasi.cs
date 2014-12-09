using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Security;
using System;
using System.Data;
using System.Data.SqlClient;
namespace Adibrata.BusinessProcess.UserManagement.Extend
{
   public class FormRegistrasi
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");
        static string _coyName = AppConfig.Config("CoyName");

        SqlTransaction _trans;

        public virtual void FormRegisterDelete(UserManagementEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams = new SqlParameter[1];
            try
            {
                sqlParams[0] = new SqlParameter("@FormID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.FormID;

                _trans = _conn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spMsFormDelete", sqlParams);
                _trans.Commit();
            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "UserRegister",
                    FunctionName = "UserRegisterDelete",
                    ExceptionNumber = 1,
                    EventSource = "UserRegistration",
                    ExceptionObject = _exp,
                    EventID = 70, // 70 Untuk User Managemetn
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                throw new Exception("User already use for transaction");
                #endregion
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) { _conn.Close(); };
                _conn.Dispose();
            }
        }

        public virtual void FormRegisterAddEdit(UserManagementEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;
            string _spname;

            if (_ent.IsEdit) { sqlParams = new SqlParameter[5]; } else { sqlParams = new SqlParameter[6]; }
            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };

                #region "List Parameter SQL"
                sqlParams[0] = new SqlParameter("@FormCode", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.FormCode;
                sqlParams[1] = new SqlParameter("@FormUrl", SqlDbType.VarChar, 250);
                sqlParams[1].Value = _ent.FormURL;
                sqlParams[2] = new SqlParameter("@FormName", SqlDbType.VarChar, 250);
                sqlParams[2].Value = _ent.FormName;

                if (_ent.IsEdit)
                {
                    sqlParams[3] = new SqlParameter("@IsActive", SqlDbType.Int);
                    sqlParams[3].Value = _ent.IsActive;
                    sqlParams[4] = new SqlParameter("@ID", SqlDbType.BigInt);
                    sqlParams[4].Value = _ent.FormID;
                    sqlParams[5] = new SqlParameter("@UsrUpd", SqlDbType.VarChar, 50);
                    sqlParams[5].Value = _ent.UserLogin;
                    sqlParams[6] = new SqlParameter("@DtmUpd", SqlDbType.SmallDateTime);
                    sqlParams[6].Value = DateTime.Now;
                    _spname = "spMsFormEdit";
                }
                else
                {
                    sqlParams[3] = new SqlParameter("@IsActive", SqlDbType.Int);
                    sqlParams[3].Value = 1;
                    sqlParams[4] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                    sqlParams[4].Value = _ent.UserLogin;
                    sqlParams[5] = new SqlParameter("@DtmCrt", SqlDbType.SmallDateTime);
                    sqlParams[5].Value = DateTime.Now;
                    _spname = "spMsFormAdd";
                }
                #endregion
                _trans = _conn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, _spname, sqlParams);
                _trans.Commit();

            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "UserRegister",
                    FunctionName = "UserRegisterAddEdit",
                    ExceptionNumber = 1,
                    EventSource = "UserRegistration",
                    ExceptionObject = _exp,
                    EventID = 70, // 80 Untuk Approval
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) { _conn.Close(); };
                _conn.Dispose();
            }
        }


        public virtual UserManagementEntities FormRegisterView(UserManagementEntities _ent)
        {

            SqlParameter[] sqlParams = new SqlParameter[1];
            SqlDataReader _rdr;
            try
            {
                sqlParams[0] = new SqlParameter("@FormCode", SqlDbType.VarChar,50);
                sqlParams[0].Value = _ent.FormCode;

                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spMsUserView", sqlParams);
                while (_rdr.Read())
                {
                    _ent.FormCode= (string)_rdr["FormCode"];
                    _ent.FormName = (string)_rdr["FormName"];
                    _ent.FormURL = (string)_rdr["FormURL"];
                }
            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Extend",
                    ClassName = "FormRegistrasi",
                    FunctionName = "FormRegisterView",
                    ExceptionNumber = 1,
                    EventSource = "FormRegistration",
                    ExceptionObject = _exp,
                    EventID = 70, // 70 Untuk User Managemetn
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            return _ent;
        }
    }
}
