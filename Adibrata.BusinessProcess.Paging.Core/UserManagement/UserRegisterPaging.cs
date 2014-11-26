using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.Framework.DataAccess;
using Adibrata.BusinessProcess.Paging.Entities;
using System.Data;
using System.Data.SqlClient;
using Adibrata.Configuration;
using Adibrata.Framework.Logging;

namespace Adibrata.BusinessProcess.Paging.Core.UserManagement
{
    public class UserRegisterPaging
    {

        static string Connectionstring = AppConfig.Config("ConnectionString");

        public virtual DataTable UserManagementList(PagingEntities _ent)
        {
            DataTable _dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("spUserPaging");
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@StartRecord", SqlDbType.Int);
                sqlParams[0].Value = _ent.StartRecord;
                sqlParams[1] = new SqlParameter("@EndRecord", SqlDbType.Int);
                sqlParams[1].Value = _ent.EndRecord;
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
                    UserName = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.Paging.Core.UserManagement",
                    ClassName = "UserRegisterPaging",
                    FunctionName = "UserManagementList",
                    ExceptionNumber = 1,
                    EventSource = "UserManagementList",
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
