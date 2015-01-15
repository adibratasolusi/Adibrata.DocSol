using Adibrata.BusinessProcess.Entities.Base;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Adibrata.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceWCF" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceWCF.svc or ServiceWCF.svc.cs at the Solution Explorer and start debugging.
    public class ServiceWCF : IServiceWCF
    {
        string Connectionstring = AppConfig.Config("ConnectionString");
        string ArchieveConnectionstring = AppConfig.Config("ArchiveConnectionString");
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
                    try
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
                            for (int a = 0; a < dtDocTransBinary.Rows.Count; a++)
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

                                SqlHelper.ExecuteNonQuery(_Archievetrans, CommandType.StoredProcedure, "spDocTransBinaryInsert", sqlParams);

                            }
                            #endregion

                            #region DOC TRANS CONTENT INSERT

                            for (int b = 0; b < dtDocTransContent.Rows.Count; b++)
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

                        }
                        _Archievetrans.Commit();

                        #region DOC TRANS DELETE

                        sqlParams = new SqlParameter[1];
                        sqlParams[0] = new SqlParameter("@docTransId", SqlDbType.BigInt);
                        sqlParams[0].Value = _ent.DocTransID;
                        SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spArchieveDeleteExecution", sqlParams);

                        #endregion

                        _trans.Commit();
                    }
                    catch (Exception _exp)
                    {
                        _trans.Rollback();
                        _Archievetrans.Rollback();
                    }



                }
                #endregion


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


        #region INQUIRY
        public DataTable DocTransInquiryDetail(DocTrans _ent)
        {
            DataTable _dt = new DataTable("Testing");
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DocTransID", SqlDbType.BigInt);
                sqlParams[0].Value = DocTransGetTransID(_ent);
                sqlParams[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.UserName;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransBinaryView", sqlParams));


            }
            catch (Exception _exp)
            {


            }
            return _dt;

        }


        public DataTable DocTransContentDetail(DocTrans _ent)
        {
            DataTable _dt = new DataTable("Testing");
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DocTransID", SqlDbType.BigInt);
                sqlParams[0].Value = DocTransGetTransID(_ent);
                sqlParams[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.UserName;

                _dt.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spDocTransContentView", sqlParams));


            }
            catch (Exception _exp)
            {

            }
            return _dt;

        }
        #endregion


        public Int64 DocTransGetTransID(DocTrans _ent)
        {
            DataTable _dt = new DataTable();
            Int64 _transid = 0;
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DocTransCode", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.DocTransCode;
                sqlParams[1] = new SqlParameter("@TransID", SqlDbType.BigInt);
                sqlParams[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, "spDocTransGetTransID", sqlParams);
                _transid = Convert.ToInt64(sqlParams[1].Value);
            }
            catch (Exception _exp)
            {

            }
            return _transid;

        }

        #region UPLOAD


        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public DateTime getImageDtCreate(Image img)
        {
            PropertyItem propItem = img.GetPropertyItem(0x132);
            DateTime dt = new DateTime();
            if (propItem != null)
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                string text = encoding.GetString(propItem.Value, 0, propItem.Len - 1);

                CultureInfo provider = CultureInfo.InvariantCulture;
                dt = DateTime.ParseExact(text, "yyyy:MM:d H:m:s", provider);


            }
            else
            {
                dt = DateTime.Now;
            }
            return dt;
        }

        public void UpdatePathDetails(PathDetails pathInfo)
        {


            string bitsServer = AppConfig.Config("BITSServer");
            var webClient = new WebClient();
            byte[] fileBytes = webClient.DownloadData(bitsServer + pathInfo.FileName);
            pathInfo.FileName = pathInfo.DocTransBinaryID + pathInfo.FileName;
            string strMessage = string.Empty;
            SqlConnection con = new SqlConnection(Connectionstring);
            int result = 0;
            try
            {

                SqlCommand command = new SqlCommand("spDocTransBinaryUpdate", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DocTransBinaryID", SqlDbType.BigInt).Value = pathInfo.DocTransBinaryID;
                command.Parameters.Add("@FileName", SqlDbType.VarChar).Value = pathInfo.FileName;
                command.Parameters.Add("@FileBinary", SqlDbType.VarBinary).Value = fileBytes;
                con.Open();
                result = command.ExecuteNonQuery();
                con.Close();

                if (result == 1)
                {

                    //logging success here
                }
                else
                {

                    //logging error db here
                }
            }
            catch (Exception _exp)
            {
                //logging app here
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = SessionProperty.UserName,
                    NameSpace = "Adibrata.Framework.WCF",
                    ClassName = "Service1",
                    FunctionName = "UpdatePathDetails",
                    ExceptionNumber = 1,
                    EventSource = "UploadServices",
                    ExceptionObject = _exp,
                    EventID = 200, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }

        }

        #endregion
    }
}
