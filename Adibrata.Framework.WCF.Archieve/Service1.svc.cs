using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Adibrata.Framework.WCF.Archieve
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        static string Connectionstring = AppConfig.Config("ConnectionString");
        static string ArchieveConnectionstring = AppConfig.Config("ArchieveConnectionString");
        SessionEntities SessionProperty = new SessionEntities();
        SqlTransaction _trans;
        SqlTransaction _Archievetrans;
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public void ArchieveExecutionProcess(DocTrans _ent)
        {
            SqlConnection _conn = new SqlConnection(Connectionstring);
            SqlConnection _Archieveconn = new SqlConnection(ArchieveConnectionstring);
            SqlParameter[] sqlParams;
            DataTable dtDocTrans = new DataTable();
            DataTable dtDocTransBinary = new DataTable();
            DataTable dtDocTransContent = new DataTable();
            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();

                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@docTransId", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.DocTransID;

                #endregion


                dtDocTrans.Load(SqlHelper.ExecuteReader(_trans, CommandType.StoredProcedure, "spArchieveGetDocTrans", sqlParams));
                dtDocTransBinary.Load(SqlHelper.ExecuteReader(_trans, CommandType.StoredProcedure, "spArchieveGetDocTransBinary", sqlParams));
                dtDocTransContent.Load(SqlHelper.ExecuteReader(_trans, CommandType.StoredProcedure, "spArchieveGetDocTransContent", sqlParams));

                if (_Archieveconn.State == ConnectionState.Closed) { _Archieveconn.Open(); };
                _Archievetrans = _Archieveconn.BeginTransaction();
                #region Archieve Process
                if (dtDocTrans.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDocTrans.Rows.Count; i++)
                    {
                        #region DOC TRANS INSERT

                        Int64 docTransId = 0;
                        DataTable _dt = new DataTable();
                        sqlParams = new SqlParameter[3];
                        sqlParams[0] = new SqlParameter("@TransId", SqlDbType.VarChar, 50);
                        sqlParams[0].Value = (string)dtDocTrans.Rows[i]["TransID"];
                        sqlParams[1] = new SqlParameter("@docType", SqlDbType.VarChar, 50);
                        sqlParams[1].Value = (string)dtDocTrans.Rows[i]["DocTypeCode"];
                        sqlParams[2] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                        sqlParams[2].Value = _ent.UserName;
                        _dt.Load(SqlHelper.ExecuteReader(_Archievetrans, CommandType.StoredProcedure, "spDocTransInsert", sqlParams));
                        docTransId = (Int64)_dt.Rows[0]["Id"];
                        #endregion

                        #region DOC TRANS BINARY INSERT
                        for (int a = 0; a < dtDocTransBinary.Rows.Count; i++)
                        {

                            sqlParams = new SqlParameter[9];
                            sqlParams[0] = new SqlParameter("@DocTransID", SqlDbType.BigInt);
                            sqlParams[0].Value = docTransId;
                            sqlParams[1] = new SqlParameter("@FileName", SqlDbType.VarChar, 8000);
                            sqlParams[1].Value = dtDocTransBinary.Rows[a]["FileName"].ToString();
                            sqlParams[2] = new SqlParameter("@DateCreated", SqlDbType.DateTime);
                            sqlParams[2].Value = (DateTime)dtDocTransBinary.Rows[a]["DateCreated"];
                            sqlParams[3] = new SqlParameter("@SizeFileBytes", SqlDbType.Decimal);
                            sqlParams[3].Value = (decimal)dtDocTransBinary.Rows[a]["SizeFileBytes"];
                            sqlParams[4] = new SqlParameter("@Pixel", SqlDbType.VarChar, 100);
                            sqlParams[4].Value = (string)dtDocTransBinary.Rows[a]["Pixel"];
                            sqlParams[5] = new SqlParameter("@ComputerName", SqlDbType.VarChar, 100);
                            sqlParams[5].Value = (string)dtDocTransBinary.Rows[a]["ComputerName"];
                            sqlParams[6] = new SqlParameter("@DPI", SqlDbType.VarChar, 100);
                            sqlParams[6].Value = (string)dtDocTransBinary.Rows[a]["DPI"];
                            sqlParams[7] = new SqlParameter("@FileBinary", SqlDbType.VarBinary);
                            sqlParams[7].Value = (byte[])dtDocTransBinary.Rows[a]["FileBinary"];
                            sqlParams[8] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                            sqlParams[8].Value = _ent.UserName;

                            SqlHelper.ExecuteNonQuery(_Archievetrans, CommandType.StoredProcedure, "spDocTransContentInsert", sqlParams);

                        }
                        #endregion

                        #region DOC TRANS CONTENT INSERT

                        for (int b = 0; b < dtDocTransContent.Rows.Count; i++)
                        {

                            sqlParams = new SqlParameter[8];
                            sqlParams[0] = new SqlParameter("@DocTypeCode", SqlDbType.VarChar, 50);
                            sqlParams[0].Value = dtDocTransContent.Rows[b]["DocTypeCode"].ToString();
                            sqlParams[1] = new SqlParameter("@DocTransId", SqlDbType.BigInt);
                            sqlParams[1].Value = docTransId;
                            sqlParams[2] = new SqlParameter("@ContentName", SqlDbType.VarChar, 50);
                            sqlParams[2].Value = dtDocTransContent.Rows[b]["ContentName"].ToString();
                            sqlParams[3] = new SqlParameter("@ContentValue", SqlDbType.VarChar, 8000);
                            sqlParams[3].Value = dtDocTransContent.Rows[b]["ContentValue"].ToString();
                            sqlParams[4] = new SqlParameter("@ContentValueDate", SqlDbType.DateTime);
                            sqlParams[4].Value = (DateTime)dtDocTransContent.Rows[b]["ContenValueDate"];
                            sqlParams[5] = new SqlParameter("@ContentValueNumeric", SqlDbType.Decimal);
                            sqlParams[5].Value = (decimal)dtDocTransContent.Rows[b]["ContentValueNumeric"];
                            sqlParams[6] = new SqlParameter("@ContentSearchTag", SqlDbType.VarChar);
                            sqlParams[6].Value = dtDocTransContent.Rows[b]["ContensSearchTag"].ToString();
                            sqlParams[7] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                            sqlParams[7].Value = _ent.UserName;
                            SqlHelper.ExecuteNonQuery(_Archievetrans, CommandType.StoredProcedure, "spDocTransContentInsert", sqlParams);

                        }

                        #endregion

                        _Archievetrans.Commit();
                    }
                }
                #endregion

                _trans.Commit();

            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserName,
                    NameSpace = "Adibrata.Framework.WCF.Archieve",
                    ClassName = "Service1",
                    FunctionName = "ArchieveExecutionProcess",
                    ExceptionNumber = 1,
                    EventSource = "Archieve",
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

    }
}
