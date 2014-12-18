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
          string _levelapproval;
          public virtual string ApprovalPathRetrieve(DocSolEntities _ent)
          {
              DataTable _dt = new DataTable();

              try
              {
                  StringBuilder sb = new StringBuilder();
                  sb.Append("Select Field2 from ");
                  sb.Append(" RUDocContentAppr ");
                  sb.Append(" Where ");
                  sb.Append(" Field1 = '");
                  sb.Append(_ent.DocumentType);
                  sb.Append("' ");
                  sb.Append(" And Result = '");
                  sb.Append(_ent.UserLogin);
                  sb.Append("' ");
                  _levelapproval = (string)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sb.ToString());

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
              return _levelapproval;
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

          public virtual DocSolEntities ApprovalTransView(DocSolEntities _ent)
          {
              SqlParameter[] sqlParams;
              SqlDataReader _rdr;
              try
              {
                  #region "List Parameter SQL"
                  sqlParams = new SqlParameter[1];
                  sqlParams[0] = new SqlParameter("@DocTransApprCode", SqlDbType.VarChar, 50);
                  sqlParams[0].Value = _ent.ApprovalTransCode;

                  _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDocTransApprovalView", sqlParams);

                  while (_rdr.Read())
                  {
                      _ent.DocumentType = (string)_rdr["DocTypeCode"];
                      _ent.DocTransCode = (string)_rdr["DocTransCode"];
                      _ent.CustomerCode = (string)_rdr["CustCode"];
                      _ent.CompanyName = (string)_rdr["CustName"];
                      _ent.ProjectCode = (string)_rdr["ProjCode"];
                      _ent.ProjectName = (string)_rdr["ProjName"];
                      _ent.ProjectType = (string)_rdr["ProjType"];
                      _ent.DocumentTransID = Convert.ToInt64(_rdr["DocTransID"]);
                  }
                  _rdr.Close();
                  #endregion
              }
              catch (Exception _exp)
              {
                  #region "Write to Event Viewer"
                  ErrorLogEntities _errent = new ErrorLogEntities
                  {
                      UserLogin = _ent.UserLogin,
                      NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                      ClassName = "CustomerRegistrasi",
                      FunctionName = "CustomerCompanyRegistrasiView",
                      ExceptionNumber = 1,
                      EventSource = "CustomerRegistrasi",
                      ExceptionObject = _exp,
                      EventID = 200, // 80 Untuk DocumentManagement
                      ExceptionDescription = _exp.Message
                  };
                  ErrorLog.WriteEventLog(_errent);
                  #endregion
              }
              return _ent;
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

        public virtual Boolean ApprovalRequestVisibility (DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            Boolean _value = false;
            RuleEngineEntities _entrule = new RuleEngineEntities { RuleName = "RuDocTypeApproval" };
              try
              {
                  StringBuilder sb = new StringBuilder();
                  sb.Append(" Field1 = '");
                  sb.Append(_ent.DocumentType);
                  sb.Append("' ");
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
              return _value;

        }
    }
}
