using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Framework.Caching;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Rule;
using System;
using System.Data;
using System.Text;


namespace Adibrata.BusinessProcess.DocumentSol.Extend
{
   public class DocContent : Adibrata.BusinessProcess.DocumentSol.Core.DocContent.DocContent
    {
       int _numoffiles;
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
                   if (!_dt.Columns.Contains("DataType"))
                   {
                       _dt.Columns.Add("DataType", typeof(string));
                   }
                   
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
                   EventID = 200, // 80 Untuk Framework 
                   ExceptionDescription = _exp.Message
               };
               ErrorLog.WriteEventLog(_errent);
           }
           return _dt;

       }

       public virtual int DocContentFiles (DocSolEntities _ent)
       {
           DataTable _dt = new DataTable();
          

           RuleEngineEntities _entrule = new RuleEngineEntities { RuleName = "RUDocFiles" };
           try
           {
               string _cachename = "Files" + _ent.DocumentType;
               if (!DataCache.Contains(_cachename))
               {
                   StringBuilder sb = new StringBuilder();

                   sb.Append(" Field1 = '");
                   sb.Append(_ent.DocumentType);
                   sb.Append("' ");


                   _entrule.WhereCond = sb.ToString();
                   _dt = Adibrata.Framework.Rule.RuleEngineProcess.RuleEngineResultList(_entrule);
              
                   foreach (DataRow _row in _dt.Rows)
                   {
                       _numoffiles = Convert.ToInt32(_row["Result"]);

                   }
                   DataCache.Insert<int>(_cachename, _numoffiles);
               }
               else
               {
                   _numoffiles = DataCache.Get<int>(_cachename);
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
                   EventID = 200, // 80 Untuk Framework 
                   ExceptionDescription = _exp.Message
               };
               ErrorLog.WriteEventLog(_errent);
           }
           return _numoffiles;
       }

    }
}
