using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Framework.Caching;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Rule;
using System;
using System.Data;
using System.Text;

namespace Adibrata.BusinessProcess.DocumentSol.Extend
{
   public class DocType: Adibrata.BusinessProcess.DocumentSol.Core.DocType
    {
       public virtual DataTable DocTypeRetrieve (DocSolEntities _ent)
       {
           DataTable _dt = new DataTable();
           RuleEngineEntities _entrule = new RuleEngineEntities { RuleName = "RuDocType" };
           try
           {
               if (!DataCache.Contains(_ent.LineOfBusiness))
               {
                   StringBuilder sb = new StringBuilder();
                   sb.Append(" Field1 = '");
                   sb.Append(_ent.LineOfBusiness);
                   sb.Append("' ");
                   _entrule.WhereCond = sb.ToString();
                   _dt = Adibrata.Framework.Rule.RuleEngineProcess.RuleEngineResultList(_entrule);
                   DataCache.Insert<DataTable>(_ent.LineOfBusiness, _dt);
               }
               else
               {
                   _dt = DataCache.Get<DataTable>(_ent.LineOfBusiness);
               }
           }
           catch (Exception _exp)
           {
               ErrorLogEntities _errent = new ErrorLogEntities
               {
                   UserLogin = _ent.UserLogin,
                   NameSpace = "Adibrata.BusinessProcess.DocumentSol",
                   ClassName = "DocContent",
                   FunctionName = "DocContentRetrieve",
                   ExceptionNumber = 1,
                   EventSource = "DocContent",
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
