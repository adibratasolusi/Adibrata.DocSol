using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Adibrata.Framework.WCF.DocTransView
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        static string Connectionstring = AppConfig.Config("ConnectionString");
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

        #region INQUIRY
        public DataTable DocTransInquiryDetail(DocTrans _ent)
        {
            DataTable _dt = new DataTable();
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
            DataTable _dt = new DataTable();
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
                sqlParams[0].Value = _ent.DocTransID;
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
    }
}
