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
   public class DocContent : Adibrata.BusinessProcess.DocumentSol.Core.DocContent.DocContent
    {
       int _numoffiles;
       static string ConnectionString = AppConfig.Config("ConnectionString");
       SqlTransaction _trans;
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

       public virtual DataTable DocSaveComment(DocSolEntities _ent)
       {
           SqlConnection _conn = new SqlConnection(ConnectionString);

           DataTable _dt = new DataTable();
           SqlParameter[] sqlParams;
           StringBuilder sb = new StringBuilder();
           try
           {

               #region "List Parameter SQL"
               if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
               _trans = _conn.BeginTransaction();
               //sb.Append("spInsertDocTransComment");
               sqlParams = new SqlParameter[3];
               sqlParams[0] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
               sqlParams[0].Value = _ent.DocTransId;
               sqlParams[1] = new SqlParameter("@Comment", SqlDbType.VarChar, 800);
               sqlParams[1].Value = _ent.Comment;
               sqlParams[2] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
               sqlParams[2].Value = _ent.UserName;




               //_dtValue.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, sb.ToString(), sqlParams));
               SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spInsertDocTransComment", sqlParams);
               _trans.Commit();


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

       public virtual DataTable DocViewComment(DocSolEntities _ent)
       {
           DataTable _dtc = new DataTable();
           try
           {

               SqlParameter[] sqlParams = new SqlParameter[1];
               sqlParams[0] = new SqlParameter("@DocTransID", SqlDbType.BigInt);
               sqlParams[0].Value = _ent.DocTransId;
              

               _dtc.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDocTransCommentView", sqlParams));


           }
           catch (Exception _exp)
           {

               ErrorLogEntities _errent = new ErrorLogEntities
               {
                   UserLogin = _ent.UserLogin,
                   NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                   ClassName = "UploadProcess",
                   FunctionName = "DocTransContentDetail",
                   ExceptionNumber = 1,
                   EventSource = "DocTransContentDetail",
                   ExceptionObject = _exp,
                   EventID = 80, // 80 Untuk Framework 
                   ExceptionDescription = _exp.Message
               };
               ErrorLog.WriteEventLog(_errent);
           }
           return _dtc;
       }

       public virtual Int64 DocTransCommentGetID(DocSolEntities ent)
       {
           DataTable _dtcom = new DataTable();
           Int64 _transcomid = 0;
           try
           {
               SqlParameter[] sqlParams = new SqlParameter[2];
               sqlParams[0] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
               sqlParams[0].Value = ent.DocTransId;
               sqlParams[1] = new SqlParameter("@TransCommentID", SqlDbType.BigInt);
               sqlParams[1].Direction = ParameterDirection.Output;
               SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spDocTransCommentGetID", sqlParams);
               _transcomid = Convert.ToInt64(sqlParams[1].Value);
           }
           catch (Exception _exp)
           {

               ErrorLogEntities _errent = new ErrorLogEntities
               {
                   UserLogin = ent.UserLogin,
                   NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                   ClassName = "UploadProcess",
                   FunctionName = "DocTransContentDetail",
                   ExceptionNumber = 1,
                   EventSource = "DocTransContentDetail",
                   ExceptionObject = _exp,
                   EventID = 80, // 80 Untuk Framework 
                   ExceptionDescription = _exp.Message
               };
               ErrorLog.WriteEventLog(_errent);
           }
           return _transcomid;

       }

       public virtual DataTable DocTransCommentClear(DocSolEntities ent)
       {
           SqlConnection _conn = new SqlConnection(ConnectionString);

           DataTable _dtcom = new DataTable();
           SqlParameter[] sqlParams;
           StringBuilder sb = new StringBuilder();
           try
           {

               #region "List Parameter SQL"
               if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
               _trans = _conn.BeginTransaction();
               //sb.Append("spInsertDocTransComment");
               sqlParams = new SqlParameter[1];
               sqlParams[0] = new SqlParameter("@DocTransCommentId", SqlDbType.BigInt);
               sqlParams[0].Value = ent.DocTransCommentId;
             
               //_dtValue.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, sb.ToString(), sqlParams));
               SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spDocTransCommentDelete", sqlParams);
               _trans.Commit();


               #endregion
           }
           catch (Exception _exp)
           {
               #region "Write to Event Viewer"
               ErrorLogEntities _errent = new ErrorLogEntities
               {
                   UserLogin = ent.UserLogin,
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
           return _dtcom;

       }

       public virtual DataTable DocSaveLinkto(DocSolEntities _ent)
       {
           DataTable _dtl = new DataTable();
           try
           {

               SqlParameter[] sqlParams = new SqlParameter[2];
               sqlParams[0] = new SqlParameter("@DocTransID", SqlDbType.BigInt);
               sqlParams[0].Value = _ent.DocTransId;
               sqlParams[1] = new SqlParameter("@DocTransLinkedId", SqlDbType.BigInt);
               sqlParams[1].Value = _ent.LinkDoc;


               _dtl.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDocTransLinkedDocument", sqlParams));


           }
           catch (Exception _exp)
           {

               ErrorLogEntities _errent = new ErrorLogEntities
               {
                   UserLogin = _ent.UserLogin,
                   NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                   ClassName = "UploadProcess",
                   FunctionName = "DocTransContentDetail",
                   ExceptionNumber = 1,
                   EventSource = "DocTransContentDetail",
                   ExceptionObject = _exp,
                   EventID = 80, // 80 Untuk Framework 
                   ExceptionDescription = _exp.Message
               };
               ErrorLog.WriteEventLog(_errent);
           }
           return _dtl;
       }
    }
}
