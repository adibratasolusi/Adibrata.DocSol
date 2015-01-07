using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using Adibrata.BusinessProcess.Paging.Entities;
using Adibrata.Configuration;
using Adibrata.BusinessProcess.Entities.Base;
using System.Data.SqlClient;

namespace Adibrata.BusinessProcess.Paging.Extend
{
    public class DocTransActivity : Adibrata.BusinessProcess.Paging.Core.DocTransActivity
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");

        public virtual DataTable DocTransActivityPaging(PagingEntities _ent)
        {
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
                    _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransActivityPaging", sqlParams));
                }
                catch (Exception _exp)
                {
                    ErrorLogEntities _errent = new ErrorLogEntities
                    {
                        UserLogin = _ent.UserLogin,
                        NameSpace = "Adibrata.BusinessProcess.Paging.Extend",
                        ClassName = "ImageProcessCheckin",
                        FunctionName = "CheckoutPaging",
                        ExceptionNumber = 1,
                        EventSource = "CheckoutPaging",
                        ExceptionObject = _exp,
                        EventID = 80, // 80 Untuk Framework 
                        ExceptionDescription = _exp.Message
                    };
                    ErrorLog.WriteEventLog(_errent);
                }
                return _dt;
            }
        }

        public virtual Int64 DocTransActivityPagingTotRec(PagingEntities _ent)
        {
            DataTable _dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            Int64 _value = 0;
            try
            {
                sb.Append("spDocTransActivityPagingTotRec");
                SqlParameter[] sqlParams = new SqlParameter[2];

                sqlParams[0] = new SqlParameter("@wherecond", SqlDbType.VarChar, 8000);
                sqlParams[0].Value = _ent.WhereCond;
                sqlParams[1] = new SqlParameter("@sortby", SqlDbType.VarChar, 8000);
                sqlParams[1].Value = _ent.SortBy;


                _value = Convert.ToInt64(SqlHelper.ExecuteScalar(Connectionstring, CommandType.StoredProcedure, sb.ToString(), sqlParams));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.Paging.Extend",
                    ClassName = "CustomerRegistrasi",
                    FunctionName = "CustomerPagingTotRec",
                    ExceptionNumber = 1,
                    EventSource = "Customer",
                    ExceptionObject = _exp,
                    EventID = 200,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _value;
        }
    }
}
