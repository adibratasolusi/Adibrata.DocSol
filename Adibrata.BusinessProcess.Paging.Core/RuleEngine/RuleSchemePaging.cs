using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using Adibrata.Rule.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Adibrata.BusinessProcess.Paging.Entities;

namespace Adibrata.BusinessProcess.Paging.Core
{
    public class RuleSchemePaging
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");

        public virtual DataTable RuleEngineList(PagingEntities _ent)
        {
            DataTable _dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("spRuleEnginePaging");
                SqlParameter[] sqlParams = new SqlParameter[4];

                sqlParams[0] = new SqlParameter("@StartPage", SqlDbType.Int);
                sqlParams[0].Value = _ent.StartRecord;

                sqlParams[1] = new SqlParameter("@EndPage", SqlDbType.Int);
                sqlParams[1].Value = _ent.EndRecord;

                sqlParams[2] = new SqlParameter("@WhereCod", SqlDbType.VarChar, 4000);
                sqlParams[2].Value = _ent.WhereCond;

                sqlParams[3] = new SqlParameter("@SortBy", SqlDbType.VarChar, 4000);
                sqlParams[3].Value = _ent.SortBy;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, sb.ToString(), sqlParams));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = _ent.UserLogin,
                    NameSpace = " Adibrata.Rule.Engine",
                    ClassName = "RuleEngineProcess",
                    FunctionName = "UploadRuleEngine",
                    ExceptionNumber = 1,
                    EventSource = "RuleEngineList",
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
