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
        SqlTransaction _trans;
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
        public virtual DataTable EditUploadSave(DocSolEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
       
            DataTable _dtValue = new DataTable();
            SqlParameter[] sqlParams;
            StringBuilder sb = new StringBuilder();
            try
            {

                #region "List Parameter SQL"
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                //sb.Append("spEditDocumentSave");
                sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@Id", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                sqlParams[1] = new SqlParameter("@Content", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.ContentValue;
                sqlParams[2] = new SqlParameter("@Usr", SqlDbType.VarChar, 50);
                sqlParams[2].Value = _ent.UserName;
                sqlParams[3] = new SqlParameter("@datatype", SqlDbType.VarChar, 50);
                sqlParams[3].Value = _ent.Datatype;
                sqlParams[4] = new SqlParameter("@ContentName", SqlDbType.VarChar, 50);
                sqlParams[4].Value = _ent.ContentName;


                
                //_dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, sb.ToString(), sqlParams));
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spEditDocumentSave", sqlParams);
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
            return _dtValue;
        }


        public virtual void UpdateDocContent(DocSolEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;
            SqlParameter[] sqlParamsDel;
            if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
            _trans = _conn.BeginTransaction();
            StringBuilder sbsearchtag = new StringBuilder();
            try
            {

                sqlParamsDel = new SqlParameter[1];
                sqlParamsDel[0] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                sqlParamsDel[0].Value = _ent.DocTransId;

                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spDocTransContentDelete", sqlParamsDel);
                sbsearchtag.Append(" Cari ");
                sbsearchtag.Append(" View ");
                sbsearchtag.Append(" Search ");
                sbsearchtag.Append(" Upload Time ");
                sbsearchtag.Append(DateTime.Now.ToString("dd/MM/yyyy"));
                sbsearchtag.Append(" ");
                sbsearchtag.Append(DateTime.Now.ToString("MM/dd/yyyy"));
                sbsearchtag.Append(" ");
                sbsearchtag.Append(DateTime.Now.ToString("yyyy/MM/dd"));
                sbsearchtag.Append(" ");
                sbsearchtag.Append(DateTime.Now.ToString("HH:MM:ss"));
                sbsearchtag.Append(" ");
                sbsearchtag.Append(" Upload Document ");
                sbsearchtag.Append(" User Upload ");
                sbsearchtag.Append(_ent.UserLogin);
                sbsearchtag.Append(" Document Upload ");
                sbsearchtag.Append(_ent.DocumentType);


                #region DOC TRANS CONTENT INSERT

                for (int i = 0; i < _ent.DtContent.Rows.Count; i++)
                {
                    _ent.ContentValue = "";
                    _ent.ContentValueNumeric = 0;
                    _ent.ContentValueDate = DateTime.Now;
                    _ent.ContentValue = _ent.DtContent.Rows[i]["EntryValue"].ToString();
                    if (_ent.DtContent.Rows[i]["DataType"].ToString().ToLower().Trim() == "date")
                    {
                        _ent.ContentValueDate = Convert.ToDateTime(_ent.DtContent.Rows[i]["EntryValueDate"].ToString());
                    }
                    if (_ent.DtContent.Rows[i]["DataType"].ToString().ToLower().Trim() == "number")
                    {
                        _ent.ContentValueNumeric = Convert.ToDecimal(_ent.DtContent.Rows[i]["EntryValueNumber"].ToString());
                    }
                    sbsearchtag.Append(" Document Type Code ");
                    sbsearchtag.Append(_ent.DtContent.Rows[i]["Field1"].ToString());

                    sbsearchtag.Append(" Content Name ");
                    sbsearchtag.Append(_ent.DtContent.Rows[i]["Field2"].ToString());
                    sbsearchtag.Append(" Value ");
                    sbsearchtag.Append(_ent.ContentValue);

                    sbsearchtag.Append(" Value Date ");
                    sbsearchtag.Append(_ent.ContentValueDate.ToString("dd/MM/yyyy"));
                    sbsearchtag.Append(" ");
                    sbsearchtag.Append(_ent.ContentValueDate.ToString("MM/dd/yyyy"));
                    sbsearchtag.Append(" ");
                    sbsearchtag.Append(_ent.ContentValueDate.ToString("yyyy/MM/dd"));
                    sbsearchtag.Append(" ");
                    if (_ent.DocContentNeedApproval)
                    {
                        sbsearchtag.Append(" Request To ");
                        sbsearchtag.Append(_ent.RequestTo);

                        sbsearchtag.Append(" Approval Notes ");
                        sbsearchtag.Append(_ent.ApprovalNotes);

                    }


                    sqlParams = new SqlParameter[8];
                    sqlParams[0] = new SqlParameter("@DocTypeCode", SqlDbType.VarChar, 50);
                    sqlParams[0].Value = _ent.DtContent.Rows[i]["Field1"].ToString();
                    sqlParams[1] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                    sqlParams[1].Value = _ent.DocTransId;
                    sqlParams[2] = new SqlParameter("@ContentName", SqlDbType.VarChar, 50);
                    sqlParams[2].Value = _ent.DtContent.Rows[i]["Field2"].ToString(); ;
                    sqlParams[3] = new SqlParameter("@ContentValue", SqlDbType.VarChar, 8000);
                    sqlParams[3].Value = _ent.ContentValue;
                    sqlParams[4] = new SqlParameter("@ContentValueDate", SqlDbType.DateTime);
                    sqlParams[4].Value = _ent.ContentValueDate;
                    sqlParams[5] = new SqlParameter("@ContentValueNumeric", SqlDbType.Decimal);
                    sqlParams[5].Value = _ent.ContentValueNumeric;
                    sqlParams[6] = new SqlParameter("@ContentSearchTag", SqlDbType.VarChar);
                    sqlParams[6].Value = sbsearchtag.ToString();
                    sqlParams[7] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                    sqlParams[7].Value = _ent.UserLogin;
                    SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spDocTransContentInsert", sqlParams);

                }
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
                    ClassName = "UploadProcess",
                    FunctionName = "DocUpload",
                    ExceptionNumber = 1,
                    EventSource = "Document Upload",
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
                #endregion

































       // public virtual DataTable EditUploadContent(DocSolEntities _ent)
       //{
       //    DataTable _dt = new DataTable();
       //    RuleEngineEntities _entrule = new RuleEngineEntities { RuleName = "RUDocContentItem" };
       //    try
       //    {
       //        if (!DataCache.Contains(_ent.DocumentType))
       //        {
       //            StringBuilder sb = new StringBuilder();

       //            sb.Append(" Field1 = '");
       //            sb.Append(_ent.DocumentType);
       //            sb.Append(_ent.ContentValue);
       //            sb.Append("' ");
       //            _entrule.WhereCond = sb.ToString();
       //            _dt = Adibrata.Framework.Rule.RuleEngineProcess.RuleEngineResultList(_entrule);
       //            if (!_dt.Columns.Contains("DataType"))
       //            {
       //                _dt.Columns.Add("DataType", typeof(string));
       //            }
                   
       //            string _value;
       //            string[] _splitvalue;

       //            foreach (DataRow _row in _dt.Rows)
       //            {
       //                _value = _row["Result"].ToString().Replace(")", "");
                   
       //                _splitvalue = _value.Split('(');
       //                Int16 _counter = 0;
       //                foreach (string word in _splitvalue)
       //                {
       //                    if (_counter == 0) { 
       //                        _row["Result"] = word; 
       //                        _counter = 1; } else {_row["DataType"] = word; _counter = 0; }
       //                }
       //                _row.AcceptChanges();
       //            }
                   
       //            _dt.AcceptChanges();
       //            DataCache.Insert<DataTable>(_ent.DocumentType, _dt);
       //        }
       //        else
       //        {
       //            _dt = DataCache.Get<DataTable>(_ent.DocumentType);
       //        }
       //    }
       //    catch (Exception _exp)
       //    {
       //        ErrorLogEntities _errent = new ErrorLogEntities
       //        {
       //            UserLogin = _ent.UserLogin,
       //            NameSpace = "Adibrata.BusinessProcess.DocumentSol",
       //            ClassName = "DocContent",
       //            FunctionName = "DocContentRetrieve",
       //            ExceptionNumber = 1,
       //            EventSource = "DocContent",
       //            ExceptionObject = _exp,
       //            EventID = 200, // 80 Untuk Framework 
       //            ExceptionDescription = _exp.Message
       //        };
       //        ErrorLog.WriteEventLog(_errent);
       //    }
       //    return _dt;

       //}

       
    }
}
