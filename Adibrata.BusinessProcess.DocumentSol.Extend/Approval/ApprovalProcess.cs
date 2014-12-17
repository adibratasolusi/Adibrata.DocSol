using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using Adibrata.Framework.Rule;
using System.Text;
using Adibrata.Framework.Caching;

namespace Adibrata.BusinessProcess.DocumentSol.Extend
{
    public class ApprovalProcess : Adibrata.BusinessProcess.DocumentSol.Core.ApprovalProcess
    {
          static string ConnectionString = AppConfig.Config("ConnectionString");
          public virtual DocSolEntities ApprovalPathRetrieve(DocSolEntities _ent)
          {
              DataTable _dt = new DataTable();
              RuleEngineEntities _entrule = new RuleEngineEntities { RuleName = "RUDocContentAppr" };
              try
              {
                  StringBuilder sb = new StringBuilder();
                  sb.Append(" Field1 = '");
                  sb.Append(_ent.DocumentType);
                  sb.Append("' ");
                  sb.Append(" And Field2 = '");
                  sb.Append(_ent.UserLogin);
                  sb.Append("' ");
                  _entrule.WhereCond = sb.ToString();
                  _dt = Adibrata.Framework.Rule.RuleEngineProcess.RuleEngineResultList(_entrule);
                  foreach (DataRow _row in _dt.Rows)
                  {
                      _ent.UserApprovalPath = (string)_row["Result"];
                  }
              }
              catch (Exception _exp)
              {
                  ErrorLogEntities _errent = new ErrorLogEntities
                  {
                      UserLogin = _ent.UserLogin,
                      NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                      ClassName = "ApprovalProcess",
                      FunctionName = "ApprovalPathRetrieve",
                      ExceptionNumber = 1,
                      EventSource = "DocContent",
                      ExceptionObject = _exp,
                      EventID = 200, // 80 Untuk Framework 
                      ExceptionDescription = _exp.Message
                  };
                  ErrorLog.WriteEventLog(_errent);
              }
              return _ent;
          }

          public virtual void ApprovalDocContentSave(DocSolEntities _ent)
          {
              DataTable _dt = new DataTable();
              RuleEngineEntities _entrule = new RuleEngineEntities { RuleName = "RUDocContentAppr" };
              try
              {
                  StringBuilder sb = new StringBuilder();
                  sb.Append(" Field1 = '");
                  sb.Append(_ent.DocumentType);
                  sb.Append("' ");
                  sb.Append(" And Field2 = '");
                  sb.Append(_ent.UserLogin);
                  sb.Append("' ");
                  _entrule.WhereCond = sb.ToString();
                  _dt = Adibrata.Framework.Rule.RuleEngineProcess.RuleEngineResultList(_entrule);
                  foreach (DataRow _row in _dt.Rows)
                  {
                      _ent.UserApprovalPath = (string)_row["Result"];
                  }
              }
              catch (Exception _exp)
              {
                  ErrorLogEntities _errent = new ErrorLogEntities
                  {
                      UserLogin = _ent.UserLogin,
                      NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                      ClassName = "ApprovalProcess",
                      FunctionName = "ApprovalPathRetrieve",
                      ExceptionNumber = 1,
                      EventSource = "DocContent",
                      ExceptionObject = _exp,
                      EventID = 200, // 80 Untuk Framework 
                      ExceptionDescription = _exp.Message
                  };
                  ErrorLog.WriteEventLog(_errent);
              }
          }

        public virtual DataTable ApprovalRequestTo (DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            try
            {
                RuleEngineEntities _entrule = new RuleEngineEntities { RuleName = "RUDocContentAppr" };
                StringBuilder sb = new StringBuilder();

                sb.Append(" Field1 = '");
                sb.Append(_ent.DocumentType);
                sb.Append("' ");

                sb.Append(" AND Field2 = '1'");
                
                _entrule.WhereCond = sb.ToString();
                _dt = Adibrata.Framework.Rule.RuleEngineProcess.RuleEngineResultList(_entrule);

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                    ClassName = "ApprovalProcess",
                    FunctionName = "ApprovalRequestTo",
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
    }
}
