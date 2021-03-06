﻿using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Security;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Adibrata.BusinessProcess.UserManagement.Extend
{
    public class UserRegister : Adibrata.BusinessProcess.UserManagement.Core.UserRegister
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");
        static string _coyName = AppConfig.Config("CoyName");

        SqlTransaction _trans;

        public virtual DataTable UserRegisterListReportData(UserManagementEntities _ent)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spUserRegisterListReport"));

            return dt;
        }

        public virtual void UserRegisterDelete (UserManagementEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams = new SqlParameter[1];
            try
            {
                sqlParams[0] = new SqlParameter("@UserID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.UserID;

                _trans = _conn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spMsUserDelete", sqlParams);
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

        public virtual UserManagementEntities UserRegisterView(UserManagementEntities _ent)
        {
            
            SqlParameter[] sqlParams = new SqlParameter[1];
            SqlDataReader _rdr;
            try
            {
                sqlParams[0] = new SqlParameter("@UserID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.UserID;

                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spMsUserView", sqlParams);
                while (_rdr.Read())
                {
                    _ent.UserName = (string)_rdr["UserName"];
                    _ent.Password = "";
                    _ent.FullName = (string)_rdr["FullName"];
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
            return _ent;
        }

        public virtual void UserRegisterAddEdit (UserManagementEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[11];
            
            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };

                string password = Encryption.EncryptToSHA3(_ent.Password) + Encryption.EncryptToSHA3(_coyName);

                #region "List Parameter SQL"
                sqlParams[0] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.UserName;
                sqlParams[1] = new SqlParameter("@FullName", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.FullName;
                sqlParams[2] = new SqlParameter("@Password", SqlDbType.VarChar, 500);
                sqlParams[2].Value = password;
               

                sqlParams[3] = new SqlParameter("@IsActive", SqlDbType.Int);
                sqlParams[3].Value = _ent.IsActive;
                sqlParams[4] = new SqlParameter("@IsConnect", SqlDbType.Int);
                sqlParams[4].Value = _ent.IsActive;
                sqlParams[5] = new SqlParameter("@ID", SqlDbType.BigInt);
                sqlParams[5].Value = _ent.UserID;
                sqlParams[6] = new SqlParameter("@IsEdit", SqlDbType.Bit);
                sqlParams[6].Value = _ent.IsEdit;

                sqlParams[7] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                sqlParams[7].Value = _ent.UserLogin;
                sqlParams[8] = new SqlParameter("@DtmCrt", SqlDbType.SmallDateTime);
                sqlParams[8].Value = DateTime.Now;
                sqlParams[9] = new SqlParameter("@UsrUpd", SqlDbType.VarChar, 50);
                sqlParams[9].Value = _ent.UserLogin;
                sqlParams[10] = new SqlParameter("@DtmUpd", SqlDbType.SmallDateTime);
                sqlParams[10].Value = DateTime.Now;

                #endregion
                _trans = _conn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure,  "spMsUserSave", sqlParams);
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
    }
}
