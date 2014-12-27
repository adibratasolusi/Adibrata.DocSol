using Adibrata.BusinessProcess.Paging.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.BusinessProcess.Paging.Core
{
    public class UserFormRegistration
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");
        public virtual DataTable UserRegistrationPaging(PagingEntities _ent)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@StartRecord", SqlDbType.VarChar, 10);
                sqlParams[0].Value = _ent.StartRecord;
                sqlParams[1] = new SqlParameter("@EndRecord", SqlDbType.VarChar, 10);
                sqlParams[1].Value = _ent.EndRecord;
                sqlParams[2] = new SqlParameter("@wherecond", SqlDbType.VarChar, 8000);
                sqlParams[2].Value = _ent.WhereCond;
                sqlParams[3] = new SqlParameter("@sortby", SqlDbType.VarChar, 8000);
                sqlParams[3].Value = _ent.SortBy;
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spArchievePreparePaging", sqlParams));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.Paging.Extend",
                    ClassName = "ArchivingPaging",
                    FunctionName = "ArchivingPaging",
                    ExceptionNumber = 1,
                    EventSource = "ArchivingPaging",
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
