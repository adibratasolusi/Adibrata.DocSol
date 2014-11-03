using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.BusinessProcess.UserManagement.Entities;
using System.Data.SqlClient;
using System.Data;
using Adibrata.Framework.Logging;

namespace Adibrata.BusinessProcess.UserManagement.Core
{
    public class UserManagement
    {
        public static void UserMangementAddEdit(UserManagementEntities _ent)
        {

            string conString = _ent.ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand("spMsUserInsert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = _ent.UserName;
                        cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = _ent.Pass;
                        cmd.Parameters.Add("@isActive", SqlDbType.VarChar).Value = _ent.IsActive;
                        cmd.Parameters.Add("@isConnect", SqlDbType.VarChar).Value = _ent.IsConnect;
                        cmd.Parameters.Add("@expiredDt", SqlDbType.VarChar).Value = _ent.ClassName;
                        cmd.Parameters.Add("@seqWrongPwd", SqlDbType.VarChar).Value = _ent.seqWrongPwd;
                        cmd.Parameters.Add("@usrCrt", SqlDbType.VarChar).Value = _ent.UsrCrt;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "USERMANAGEMENT",
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Core",
                    ClassName = "UserManagement",
                    FunctionName = "UserMangementAddEdit",
                    ExceptionNumber = 1,
                    EventSource = "UserManagement_Core",
                    ExceptionObject = _exp,
                    EventID = 1001,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        public static string GetPassword(UserManagementEntities _ent)
        {
            string pass;
            try
            {

                string conString = _ent.ConnectionString;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand("spMsUserGetPass", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = _ent.UserName;
                        cmd.Parameters.Add(new SqlParameter("@pass", SqlDbType.VarChar));
                        cmd.Parameters["@pass"].Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();  // *** since you don't need the returned data - just call ExecuteNonQuery
                        pass = (string)cmd.Parameters["@pass"].Value;
                        con.Close();

                        
                    }
                }
            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {

                    UserName = "USERMANAGEMENT",
                    NameSpace = "Adibrata.BusinessProcess.UserManagement.Core",
                    ClassName = "UserManagement",
                    FunctionName = "GetPassword",
                    ExceptionNumber = 1,
                    EventSource = "UserManagement_Core",
                    ExceptionObject = _exp,
                    EventID = 1001,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return pass;
        }
    }
}
