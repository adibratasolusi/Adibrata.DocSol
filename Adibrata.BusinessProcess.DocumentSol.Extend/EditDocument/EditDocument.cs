using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.Caching;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Rule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.BusinessProcess.DocumentSol.Extend
{
    public class EditDocument:Adibrata.BusinessProcess.DocumentSol.Core.EditDocument
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");
        public virtual DataTable EditUpload(DocSolEntities _ent)
        {
             DataTable _dt = new DataTable();
             SqlParameter[] sqlParams;
             StringBuilder sb = new StringBuilder();
            try
            {
                #region "List Parameter SQL"
                sb.Append("spEditUploadDocument");
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@Id", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                _dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, sb.ToString(), sqlParams));

            
                #endregion
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                    ClassName = "ImageProcess",
                    FunctionName = "ImageStatusLocked",
                    ExceptionNumber = 1,
                    EventSource = "DeleteDocument",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);

                #endregion
            } 
            return _dt;
        }
        public virtual DataTable EditUploadValue(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            SqlParameter[] sqlParams;
            StringBuilder sb = new StringBuilder();
            try
            {
                #region "List Parameter SQL"
                sb.Append("spEditDocumentvalue");
                sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.DocTransId;
                sqlParams[1] = new SqlParameter("@DocTypeCode", SqlDbType.VarChar,50);
                sqlParams[1].Value = _ent.DocumentType;
                _dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, sb.ToString(), sqlParams));


                #endregion
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Extend",
                    ClassName = "ImageProcess",
                    FunctionName = "ImageStatusLocked",
                    ExceptionNumber = 1,
                    EventSource = "DeleteDocument",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);

                #endregion
            }
            return _dt;
        }
        public virtual DataTable EditUploadContent(DocSolEntities _ent)
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
                   sb.Append(_ent.ContentValue);
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
                           if (_counter == 0) { 
                               _row["Result"] = word; 
                               _counter = 1; } else {_row["DataType"] = word; _counter = 0; }
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

       
    }
}
