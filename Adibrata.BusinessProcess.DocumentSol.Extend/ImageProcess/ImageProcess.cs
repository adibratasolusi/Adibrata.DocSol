using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.BusinessProcess.DocumentSol.Extend
{
   
    public class ImageProcess : Adibrata.BusinessProcess.DocumentSol.Core.ImageProcess
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");
        
        public void ImageStatusLocked(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.Id;


                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spImageLockedStatus", sqlParams);


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
        }
        public void ImageStatusUnlocked(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;


                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spImageUnlockedStatus", sqlParams);


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
        }
        public void DocTransCheckOut(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                sqlParams[1] = new SqlParameter("@Usr", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.UserName;


                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDocTransCheckOut", sqlParams);


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
                    ClassName = "ImageProcess",
                    FunctionName = "DocTransCheckOut",
                    ExceptionNumber = 1,
                    EventSource = "CheckOut",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }
        public void DocTransCheckIn(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                sqlParams[1] = new SqlParameter("@Usr", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.UserName;


                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDocTransCheckIn", sqlParams);


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
                    ClassName = "ImageProcess",
                    FunctionName = "DocTransCheckIn",
                    ExceptionNumber = 1,
                    EventSource = "CheckOut",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }
        public void DocTransBookMark(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                sqlParams[1] = new SqlParameter("@Usr", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.UserName;
                sqlParams[2] = new SqlParameter("@bookmarkstat", SqlDbType.VarChar, 50);
                sqlParams[2].Value = _ent.BookmarkStat;


                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDocTransBookMark", sqlParams);


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
                    ClassName = "ImageProcess",
                    FunctionName = "DocTransBookMark",
                    ExceptionNumber = 1,
                    EventSource = "ImageProcess",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }
        public void DocTransBinaryUpdateFileBinary(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                sqlParams[1] = new SqlParameter("@Usr", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.UserName;
                sqlParams[2] = new SqlParameter("@filebin", SqlDbType.VarBinary);
                sqlParams[2].Value = _ent.FileBinary;


                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDocTransBinaryUpdateFileBinary", sqlParams);


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
                    ClassName = "ImageProcess",
                    FunctionName = "DocTransBinaryUpdateFileBinary",
                    ExceptionNumber = 1,
                    EventSource = "ImageProcess",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }
        public void UpdateBinaryDocTransBinary(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@DocTransBinaryId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                sqlParams[1] = new SqlParameter("@Note", SqlDbType.VarChar, 20);
                sqlParams[1].Value = _ent.Note;
                sqlParams[2] = new SqlParameter("@OldBinary", SqlDbType.VarBinary);
                sqlParams[2].Value = _ent.OldFileBinary;
                sqlParams[3] = new SqlParameter("@NewBinary", SqlDbType.VarBinary);
                sqlParams[3].Value = _ent.NewFileBinary;
                sqlParams[4] = new SqlParameter("@UsrUpd", SqlDbType.VarChar, 20);
                sqlParams[4].Value = _ent.UserName;


                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDocTransBinaryUpdateBinary", sqlParams);


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
                    ClassName = "ImageProcess",
                    FunctionName = "UpdateBinaryDocTransBinary",
                    ExceptionNumber = 1,
                    EventSource = "ImageProcess",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }
        public void UpdateNoteDocTransBinary(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@DocTransBinaryId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                sqlParams[1] = new SqlParameter("@Note", SqlDbType.VarChar, 20);
                sqlParams[1].Value = _ent.Note;
                sqlParams[2] = new SqlParameter("@UsrUpd", SqlDbType.VarChar, 20);
                sqlParams[2].Value = _ent.UserName;


                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDocTransBinaryUpdateNote", sqlParams);


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
                    ClassName = "ImageProcess",
                    FunctionName = "UpdateNoteDocTransBinary",
                    ExceptionNumber = 1,
                    EventSource = "ImageProcess",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }
        public DataTable DocTransBinaryNoteView(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            DataTable dt = new DataTable();
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@DocTransBinaryId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;


                dt.Load(SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDocTransBinaryNoteView", sqlParams));


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
                    FunctionName = "DocTransBinaryNoteView",
                    ExceptionNumber = 1,
                    EventSource = "ImageProcess",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
            return dt;
        }
        public void DocTransBinaryReplaceFileBinary(DocSolEntities _ent)//method
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@DocTransBinaryId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                sqlParams[1] = new SqlParameter("@FileBinary", SqlDbType.VarBinary);
                sqlParams[1].Value = _ent.FileBinary;
                sqlParams[2] = new SqlParameter("@UsrUpd", SqlDbType.VarChar, 20);
                sqlParams[2].Value = _ent.UserName;


                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spDocTransBinaryReplaceFileBinary", sqlParams);


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
                    ClassName = "ImageProcess",
                    FunctionName = "UpdateNoteDocTransBinary",
                    ExceptionNumber = 1,
                    EventSource = "ImageProcess",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }
        public void SaveEditImage(DocSolEntities _ent)
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@DocTransBinaryId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.Id;
                sqlParams[1] = new SqlParameter("@FileBinary", SqlDbType.VarBinary);
                sqlParams[1].Value = _ent.FileBinary;
                sqlParams[2] = new SqlParameter("@UsrUpd", SqlDbType.VarChar, 20);
                sqlParams[2].Value = _ent.UserName;
                sqlParams[3] = new SqlParameter("@FileName", SqlDbType.VarChar, 50);
                sqlParams[3].Value = _ent.FileName;



                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spImageMaintenanceSave", sqlParams);


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
                    ClassName = "ImageProcess",
                    FunctionName = "UpdateNoteDocTransBinary",
                    ExceptionNumber = 1,
                    EventSource = "ImageProcess",
                    ExceptionObject = _exp,
                    EventID = 200, // 80 Untuk DocumentManagement
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion
            }
        }



    }


}
