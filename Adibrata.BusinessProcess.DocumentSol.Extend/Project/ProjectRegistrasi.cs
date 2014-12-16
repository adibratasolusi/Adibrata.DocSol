using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.Caching;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Rule;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Adibrata.BusinessProcess.DocumentSol.Extend
{
    public class ProjectRegistrasi : Adibrata.BusinessProcess.DocumentSol.Core.ProjectRegistrasi
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");

        SqlTransaction _trans;
        public virtual void ProjectRegistrasiAdd(DocSolEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;

            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@ProjName", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.ProjectName;
                sqlParams[1] = new SqlParameter("@ProjType", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.ProjectType;
                sqlParams[2] = new SqlParameter("@CustCode", SqlDbType.VarChar,50);
                sqlParams[2].Value = _ent.CustomerCode;
                sqlParams[3] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                sqlParams[3].Value = _ent.UserLogin;
                sqlParams[4] = new SqlParameter("@DtmCrt", SqlDbType.SmallDateTime);
                sqlParams[4].Value = DateTime.Now;
                
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spProjectSave", sqlParams);
                #endregion
                _trans.Commit();
            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                    ClassName = "ProjectRegistrasi",
                    FunctionName = "ProjectRegistrasiAdd",
                    ExceptionNumber = 1,
                    EventSource = "ProjectRegistrasi",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) { _conn.Close(); };
                _conn.Dispose();
            }
        }

        public virtual DocSolEntities ProjectRegistrasiView(DocSolEntities _ent)
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ProjCode", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.ProjectCode;

                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spProjectView", sqlParams);

                while (_rdr.Read())
                {
                    _ent.CustomerCode = (string)_rdr["CustCode"];
                    _ent.ProjectName = (string)_rdr["ProjName"];
                    _ent.ProjectCode = (string)_rdr["ProjCode"];
                    _ent.CompanyName = (string)_rdr["CustName"];
                    _ent.ProjectType = (string)_rdr["ProjType"];
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
                    ClassName = "ProjectRegistrasi",
                    FunctionName = "ProjectRegistrasiView",
                    ExceptionNumber = 1,
                    EventSource = "ProjectRegistrasi",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            return _ent;
        }

        public virtual DataTable ProjectTypeReceive(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            RuleEngineEntities _entrule = new RuleEngineEntities { RuleName = "RUBusinessLine" };
            try
            {
                if (!DataCache.Contains("ListBusinessLine"))
                {

                    _entrule.WhereCond = "";
                    _dt = Adibrata.Framework.Rule.RuleEngineProcess.RuleEngineResultList(_entrule);
                    DataCache.Insert<DataTable>("ListBusinessLine", _dt);
                }
                else
                {
                    _dt = DataCache.Get<DataTable>("ListBusinessLine");
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol",
                    ClassName = "ProjectRegistrasi",
                    FunctionName = "ProjectTypeReceive",
                    ExceptionNumber = 1,
                    EventSource = "ProjectRegistrasi",
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
