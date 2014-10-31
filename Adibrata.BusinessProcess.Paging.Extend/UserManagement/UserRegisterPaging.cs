using Adibrata.BusinessProcess.Paging.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Adibrata.BusinessProcess.Paging.Extend.UserManagement
{
   public class UserRegisterPaging: Adibrata.BusinessProcess.Paging.Core.UserManagement.UserRegisterPaging
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");
        public virtual DataTable UserRegister(PagingEntities _ent)
        {
            DataTable _dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("spMsUserPaging");
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@currentpage", SqlDbType.VarChar, 500);
                sqlParams[0].Value = _ent.CurrentPage;
                sqlParams[1] = new SqlParameter("@pagesize", SqlDbType.VarChar, 500);
                sqlParams[1].Value = _ent.PageSize;
                sqlParams[2] = new SqlParameter("@wherecond", SqlDbType.VarChar, 500);
                sqlParams[2].Value = _ent.WhereCond;
                sqlParams[3] = new SqlParameter("@sortby", SqlDbType.VarChar, 500);
                sqlParams[3].Value = _ent.SortBy;
                _dt = (DataTable)SqlHelper.ExecuteDataset(Connectionstring, CommandType.StoredProcedure, sb.ToString(), sqlParams).Tables[0];
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = " Adibrata.BusinessProcess.Paging.Core.UserManagement",
                    ClassName = "UserRegisterPaging",
                    FunctionName = "UserRegister",
                    ExceptionNumber = 1,
                    EventSource = "UserRegister",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _dt;
        }
    }
}
