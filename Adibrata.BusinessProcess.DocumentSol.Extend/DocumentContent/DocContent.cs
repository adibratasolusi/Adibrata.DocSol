using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.Framework.Logging;
using System.Data.SqlClient;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Caching;
using System.Data;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.Rule;


namespace Adibrata.BusinessProcess.DocumentSol.Extend
{
   public class DocContent : Adibrata.BusinessProcess.DocumentSol.Core.DocContent.DocContent
    {
       public virtual DataTable DocContentRetrieve(DocSolEntities _ent)
       {
           DataTable _dt = new DataTable();
           RuleEngineEntities _entrule = new RuleEngineEntities { RuleName = "RUDocContentItem" };
           try
           {
               if (!DataCache.Contains(_ent.DocumentType))
               {
                   StringBuilder sb = new StringBuilder();

                   sb.Append(" Field1 = '");
                   sb.Append(_ent.DocumentType);
                   sb.Append("' ");


                   _entrule.WhereCond = sb.ToString();
                   _dt = Adibrata.Framework.Rule.RuleEngineProcess.RuleEngineResultList(_entrule);
                   _dt.Columns.Add("DataType", typeof(string));
                   string _value;
                   string[] _splitvalue;

                   foreach (DataRow _row in _dt.Rows)
                   {
                       _value = _row["Result"].ToString().Replace(")", "");
                       _splitvalue = _value.Split('(');
                       Int16 _counter = 0;
                       foreach (string word in _splitvalue)
                       {
                           if (_counter == 0) { _row["Result"] = word; _counter = 1; } else {_row["DataType"] = word; _counter = 0; }
                       }
                       _row.AcceptChanges();
                   }
                   
                   _dt.AcceptChanges();
                   DataCache.Insert<DataTable>(_ent.DocumentType, _dt);
               }
               else
               {
                   _dt = DataCache.Get<DataTable>(_ent.DocumentType);
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
