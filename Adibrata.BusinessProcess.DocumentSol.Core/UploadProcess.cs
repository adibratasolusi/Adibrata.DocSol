﻿using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.ImageProcessing;
using Adibrata.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Adibrata.BusinessProcess.DocumentSol.Core
{
    public class UploadProcess
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");
        public virtual DataTable DocMasterGetActive(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("spDocMasterGetActive");
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, sb.ToString()));
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "DocMasterGetActive",
                    ExceptionNumber = 1,
                    EventSource = "DocMasterGetActive",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

            return _dt;
        }

        //public void UpdatePathDetails(PathDetails pathInfo)
        //{


        //    string bitsServer = AppConfig.Config("BITSServer");
        //    var webClient = new WebClient();
        //    byte[] fileBytes = webClient.DownloadData(bitsServer + pathInfo.FileName);
        //    Image img = byteArrayToImage(fileBytes);
        //    pathInfo.DPI = img.HorizontalResolution.ToString();
        //    pathInfo.Pixel = img.Width + "x" + img.Height;
        //    pathInfo.SizeFileBytes = fileBytes.Length;
        //    pathInfo.DateCreated = getImageDtCreate(img);
        //    string strMessage = string.Empty;
        //    SqlConnection con = new SqlConnection(Connectionstring);
        //    int result = 0;
        //    try
        //    {

        //        SqlCommand command = new SqlCommand("spUpdatePath", con);
        //        command.CommandType = CommandType.StoredProcedure;

        //        command.Parameters.Add("@DocTransID", SqlDbType.BigInt).Value = pathInfo.DocTransID;
        //        command.Parameters.Add("@FileName", SqlDbType.VarChar).Value = pathInfo.FileName;
        //        command.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = pathInfo.DateCreated;
        //        command.Parameters.Add("@SizeFileBytes", SqlDbType.Decimal).Value = pathInfo.SizeFileBytes;
        //        command.Parameters.Add("@Pixel", SqlDbType.VarChar).Value = pathInfo.Pixel;
        //        command.Parameters.Add("@ComputerName", SqlDbType.VarChar).Value = pathInfo.ComputerName;
        //        command.Parameters.Add("@DPI", SqlDbType.VarChar).Value = pathInfo.DPI;
        //        command.Parameters.Add("@FileBinary", SqlDbType.VarBinary).Value = fileBytes;
        //        con.Open();
        //        result = command.ExecuteNonQuery();
        //        con.Close();

        //        if (result == 1)
        //        {

        //            //logging success here
        //        }
        //        else
        //        {

        //            //logging error db here
        //        }
        //    }
        //    catch (Exception _exp)
        //    {
        //        //logging app here
        //        ErrorLogEntities _errent = new ErrorLogEntities
        //        {
        //            UserLogin = SessionProperty.UserName,
        //            NameSpace = "Adibrata.Framework.WCF",
        //            ClassName = "Service1",
        //            FunctionName = "UpdatePathDetails",
        //            ExceptionNumber = 1,
        //            EventSource = "UploadServices",
        //            ExceptionObject = _exp,
        //            EventID = 200, // 1 Untuk Framework 
        //            ExceptionDescription = _exp.Message
        //        };
        //        ErrorLog.WriteEventLog(_errent);
        //    }

        //}


        public virtual DataTable AgreementGetInfo(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@agreementNo", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.AgrmntNo;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spAgreementGetAgreementInfo", sqlParams));

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "AgreementGetInfo",
                    ExceptionNumber = 1,
                    EventSource = "AgreementGetInfo",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dt;

        }


        public virtual DataTable PathGetView(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[0];
                sqlParams[0] = new SqlParameter("@agrmntNo", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.AgrmntNo;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spPathView", sqlParams));


            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "PathGetView",
                    ExceptionNumber = 1,
                    EventSource = "PathGetView",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dt;

        }


        public virtual DataTable PagingAgreement(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@by", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.By;
                sqlParams[1] = new SqlParameter("@crit", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.Crit;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spPagingAgreement", sqlParams));


            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "PagingAgreement",
                    ExceptionNumber = 1,
                    EventSource = "PagingAgreement",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dt;

        }


        #region UPLOAD PROCESS

        SqlTransaction _trans;
        public virtual List<KeyValuePair<Int64, string>> DocUpload(DocSolEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(Connectionstring);
            SqlParameter[] sqlParams;
            DataTable _dt;
            StringBuilder sbsearchtag = new StringBuilder();

            List<KeyValuePair<Int64, string>> listDocBinary = new  List<KeyValuePair<Int64, string>>();
            try
            {
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
                
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                #region DOC TRANS INSERT

                Int64 docTransId = 0;
                _dt = new DataTable();
                sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@TransCode", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.TransCode;
                sqlParams[1] = new SqlParameter("@docType", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.DocumentType;
                sqlParams[2] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                sqlParams[2].Value = _ent.UserLogin;
                sqlParams[3] = new SqlParameter("@MaturityDt", SqlDbType.DateTime);
                sqlParams[3].Value = _ent.MaturityDt;
                _dt.Load(SqlHelper.ExecuteReader(_trans, CommandType.StoredProcedure, "spDocTransInsert", sqlParams));
                docTransId = (Int64)_dt.Rows[0]["Id"];
                #endregion
               
                #region DOC TRANS BINARY INSERT
                for (int i = 0; i < _ent.ListPath.Count; i++)
                {
                    byte[] byteFile = File.ReadAllBytes(_ent.ListPath[i]);
                   

                    _ent.Ext = Path.GetExtension(_ent.ListPath[i]).ToLower();
                 
                    _ent.SizeFileBytes = byteFile.Length;
                    _ent.ComputerName = Environment.MachineName;
                    _ent.DateCreated = File.GetCreationTime(_ent.ListPath[i]);
                    if (_ent.Ext == ".jpg" || _ent.Ext == ".jpeg" || _ent.Ext == ".png")
                    {

                        System.Drawing.Image imgFile = ImageConverterProcess.byteArrayToImage(byteFile);

                        _ent.Pixel = imgFile.Width + "x" + imgFile.Height;
                        _ent.DPI = imgFile.HorizontalResolution.ToString();

                    }
                    else
                    {

                        _ent.Pixel = "-";
                        _ent.DPI = "-";
                    }


                    _ent.FileName = Path.GetFileName(_ent.ListPath[i]);
                    sbsearchtag.Append(" Upload Computer Name ");
                    sbsearchtag.Append(_ent.ComputerName);
                    sbsearchtag.Append(" File Name ");
                    sbsearchtag.Append(_ent.FileName);
                    sbsearchtag.Append(" Pixel ");
                    sbsearchtag.Append(_ent.Pixel);
                    sbsearchtag.Append(" DPI ");
                    sbsearchtag.Append(_ent.DPI);
                    sbsearchtag.Append(" File Extention ");
                    sbsearchtag.Append(_ent.Ext);
                    sbsearchtag.Append(" File Upload Date ");
                    sbsearchtag.Append(_ent.DateCreated.ToString("dd/MM/yyyy"));
                    sbsearchtag.Append(" ");
                    sbsearchtag.Append(_ent.DateCreated.ToString("MM/dd/yyyy"));
                    sbsearchtag.Append(" ");
                    sbsearchtag.Append(_ent.DateCreated.ToString("yyyy/MM/dd"));
                    sbsearchtag.Append(" ");
                    sbsearchtag.Append(_ent.DateCreated.ToString("HH:mm:ss"));
                    sbsearchtag.Append(" ");
                    sbsearchtag.Append(" Ukuran File ");
                    sbsearchtag.Append(_ent.SizeFileBytes.ToString());
                    sbsearchtag.Append(" Bytes ");
                    sbsearchtag.Append((_ent.SizeFileBytes / 1024).ToString());
                    sbsearchtag.Append(" Kilo Bytes ");
                    sbsearchtag.Append((_ent.SizeFileBytes / 1024 / 1024).ToString());
                    sbsearchtag.Append(" Mega Bytes ");
                    sbsearchtag.Append((_ent.SizeFileBytes / 1024 / 1024 / 1024).ToString());
                    sbsearchtag.Append(" Giga Bytes ");
                    _dt = new DataTable();

                    sqlParams = new SqlParameter[8];
                    sqlParams[0] = new SqlParameter("@DocTransID", SqlDbType.BigInt);
                    sqlParams[0].Value = docTransId;
                    sqlParams[1] = new SqlParameter("@FileName", SqlDbType.VarChar, 8000);
                    sqlParams[1].Value = _ent.FileName;
                    sqlParams[2] = new SqlParameter("@DateCreated", SqlDbType.DateTime);
                    sqlParams[2].Value = _ent.DateCreated;
                    sqlParams[3] = new SqlParameter("@SizeFileBytes", SqlDbType.Decimal);
                    sqlParams[3].Value = _ent.SizeFileBytes;
                    sqlParams[4] = new SqlParameter("@Pixel", SqlDbType.VarChar, 100);
                    sqlParams[4].Value = _ent.Pixel;
                    sqlParams[5] = new SqlParameter("@ComputerName", SqlDbType.VarChar, 100);
                    sqlParams[5].Value = _ent.ComputerName;
                    sqlParams[6] = new SqlParameter("@DPI", SqlDbType.VarChar, 100);
                    sqlParams[6].Value = _ent.DPI;
                    sqlParams[7] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                    sqlParams[7].Value = _ent.UserLogin;
                    // _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransInsert", sqlParams));
                    _dt.Load(SqlHelper.ExecuteReader(_trans, CommandType.StoredProcedure, "spDocTransBinaryInsert", sqlParams));

                    Int64 Id = Convert.ToInt64(_dt.Rows[0]["Id"].ToString());
                    listDocBinary.Add(new KeyValuePair<Int64, string>(Id, _ent.ListPath[i])); ;

                }
                #endregion

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
                    sqlParams[1].Value = docTransId;
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

                #endregion
                if (_ent.DocContentNeedApproval)
                {
                    sqlParams = new SqlParameter[8];
                    sqlParams[0] = new SqlParameter("@DocTypeCode", SqlDbType.VarChar, 50);
                    sqlParams[0].Value = _ent.DocumentType;
                    sqlParams[1] = new SqlParameter("@DocTransID", SqlDbType.BigInt);
                    sqlParams[1].Value = docTransId;
                    sqlParams[2] = new SqlParameter("@DocTransReqDate", SqlDbType.SmallDateTime);
                    sqlParams[2].Value = DateTime.Now;
                    sqlParams[3] = new SqlParameter("@DocTransReqUser", SqlDbType.VarChar, 50);
                    sqlParams[3].Value = _ent.UserLogin;
                    sqlParams[4] = new SqlParameter("@RequestTo", SqlDbType.VarChar, 50);
                    sqlParams[4].Value = _ent.RequestTo;
                    sqlParams[5] = new SqlParameter("@ApprovalNotes", SqlDbType.VarChar, 8000);
                    sqlParams[5].Value = _ent.ApprovalNotes;
                    sqlParams[6] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                    sqlParams[6].Value = _ent.UserLogin;
                    sqlParams[7] = new SqlParameter("@DtmCrt", SqlDbType.SmallDateTime);
                    sqlParams[7].Value = DateTime.Now;
                    SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spDocTransApprovalRequest", sqlParams);
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

            return listDocBinary;
        }

        public virtual string DocTransInsert(DocSolEntities _ent)
        {
            string pathId = "";
            try
            {
                DataTable _dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@TransId", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.TransId;
                sqlParams[1] = new SqlParameter("@docType", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.DocumentType;
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransInsert", sqlParams));
                pathId = _dt.Rows[0]["Id"].ToString();

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "PathInsert",
                    ExceptionNumber = 1,
                    EventSource = "PathInsert",
                    ExceptionObject = _exp,
                    EventID = 201, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return pathId;

        }

        public virtual string DocTransBinaryInsert(DocSolEntities _ent)
        {
            string Id = "";
            try
            {

                DataTable _dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@DocTransID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.DocTransId;
                sqlParams[1] = new SqlParameter("@FileName", SqlDbType.VarChar, 8000);
                sqlParams[1].Value = _ent.FileName;
                sqlParams[2] = new SqlParameter("@DateCreated", SqlDbType.DateTime);
                sqlParams[2].Value = _ent.DateCreated;
                sqlParams[3] = new SqlParameter("@SizeFileBytes", SqlDbType.Decimal);
                sqlParams[3].Value = _ent.SizeFileBytes;
                sqlParams[4] = new SqlParameter("@Pixel", SqlDbType.VarChar, 100);
                sqlParams[4].Value = _ent.Pixel;
                sqlParams[5] = new SqlParameter("@ComputerName", SqlDbType.VarChar, 100);
                sqlParams[5].Value = _ent.ComputerName;
                sqlParams[6] = new SqlParameter("@DPI", SqlDbType.VarChar, 100);
                sqlParams[6].Value = _ent.DPI;
                // _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransInsert", sqlParams));
                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransBinaryInsert", sqlParams));
                Id = _dt.Rows[0]["Id"].ToString();

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "DocTransBinaryInsert",
                    ExceptionNumber = 1,
                    EventSource = "DocTransBinaryInsert",
                    ExceptionObject = _exp,
                    EventID = 201, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return Id;

        }
        public virtual void DocTransBinaryUpdate(DocSolEntities _ent)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@Id", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                sqlParams[1] = new SqlParameter("@FileBinary", SqlDbType.VarBinary);
                sqlParams[1].Value = _ent.FileBinary;
                SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, "spDocTransBinaryUpdate", sqlParams);

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "DocTransBinaryUpdate",
                    ExceptionNumber = 1,
                    EventSource = "DocTransBinaryUpdate",
                    ExceptionObject = _exp,
                    EventID = 201, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }
        public virtual void DocTransContentInsert(DocSolEntities _ent)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@DocTypeCode", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.DocTypeCode;
                sqlParams[1] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                sqlParams[1].Value = _ent.DocTransId;
                sqlParams[2] = new SqlParameter("@ContentName", SqlDbType.VarChar, 50);
                sqlParams[2].Value = _ent.ContentName;
                sqlParams[3] = new SqlParameter("@ContentValue", SqlDbType.VarChar, 8000);
                sqlParams[3].Value = _ent.ContentValue;
                sqlParams[4] = new SqlParameter("@ContentValueDate", SqlDbType.DateTime);
                sqlParams[4].Value = _ent.ContentValueDate;
                sqlParams[5] = new SqlParameter("@ContentValueNumeric", SqlDbType.Decimal);
                sqlParams[5].Value = _ent.ContentValueNumeric;
                sqlParams[6] = new SqlParameter("@ContentSearchTag", SqlDbType.VarChar, 8000);
                sqlParams[6].Value = _ent.ContentSearchTag;
                SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, "spDocTransContentInsert", sqlParams);

            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "DocTransContentInsert",
                    ExceptionNumber = 1,
                    EventSource = "DocTransContentInsert",
                    ExceptionObject = _exp,
                    EventID = 201, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }
        #endregion


        #region INQUIRY
        public virtual DataTable DocTransInquiryDetail(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DocTransID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.DocTransId;
                sqlParams[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.UserName;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransBinaryView", sqlParams));


            }
            catch (Exception _exp)
            {

                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.DocumentSol.Core",
                    ClassName = "UploadProcess",
                    FunctionName = "DocTransInquiryDetail",
                    ExceptionNumber = 1,
                    EventSource = "DocTransInquiryDetail",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _dt;

        }


        public virtual DataTable DocTransContentDetail(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DocTransID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.DocTransId;
                sqlParams[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.UserName;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransContentView", sqlParams));


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
            return _dt;

        }
        #endregion

        public virtual Int64 DocTransGetTransID(DocSolEntities _ent)
        {
            DataTable _dt = new DataTable();
            Int64 _transid = 0;
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DocTransCode", SqlDbType.VarChar,50);
                sqlParams[0].Value = _ent.DocTransCode;
                sqlParams[1] = new SqlParameter("@TransID", SqlDbType.BigInt);
                sqlParams[1].Direction = ParameterDirection.Output;
           
                SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, "spDocTransGetTransID", sqlParams);
                _transid = Convert.ToInt64(sqlParams[1].Value);
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
            return _transid;

        }
    }
}
