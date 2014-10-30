using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Adibrata.Rule.Engine;
using System.Configuration;
using Adibrata.BusinessProcess.Paging.Entities;
using Adibrata.Framework.Logging;

namespace Adibrata.BusinessProcess.Paging.Core
{
    public class RuleSchemePaging
    {
        // Henry Sudarma
        public virtual DataTable PagingProcess(PagingEntities _ent)
        {
            DataTable _dt = new DataTable();
            RuleEngineEntities _ent1 = new RuleEngineEntities();
            try
            {
                _ent1.ConnectionString = _ent.ConnectionString;
                _ent1.CurrentPage = _ent.CurrentPage;
                _ent1.WhereCond = "";
                _ent1.SortBy = "";
                _ent1.PageSize = _ent.PageSize;
                _dt = RuleEngineProcess.ListRuleEngine(_ent1);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "EMAIL",
                    NameSpace = "Adibrata.BusinessProcess.Paging.Core",
                    ClassName = "RuleEngine",
                    FunctionName = "RuleEnginePaging",
                    ExceptionNumber = 1,
                    EventSource = "Paging_Core",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dt;
        }
    }
}
