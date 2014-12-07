using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

#region "Struktur table Company Customer"
//CustID BIGINT, 
//NPWP varchar(20), 
//SIUP Varchar(50), 
//TDP Varchar(50), 
//AkteNo Varchar (50),
//Address Varchar(100), 
//RT Varchar(4), 
//RW Varchar (4), 
//Kelurahan Varchar (50), 
//Kecamatan Varchar(50),
//City Varchar(50),  
//ZipCode varchar (12), 
//DtmUpd Datetime, 
//UsrUpd Varchar(50), 
//UsrCrt DateTime, 
//DtmCrt Varchar(50)
#endregion 

#region "Struktur table Customer"
    //[ID] [int] IDENTITY(1,1) NOT NULL,
    //[CustCode] [nvarchar](20) NOT NULL,
    //[CustName] [nvarchar](50) NOT NULL,
    //[CustType] [char](1) NOT NULL,
    //[FirstAgreementDate] [datetime] NULL,
    //[CustGroupID] [int] NULL,
    //[CollectibilityId] [int] NULL,
    //[CustomerGroupLevel] [int] NULL,
    //[CustomerGroupCode] [int] NULL,
    //[UsrUpd] [nvarchar](50) NOT NULL,
    //[DtmUpd] [datetime] NOT NULL,
    //[UsrCrt] [nvarchar](50) NOT NULL,
//[DtmCrt] [datetime] NOT NULL,
#endregion

namespace Adibrata.BusinessProcess.DocumentSol.Extend
{
    public class CustomerRegistrasi
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");
        
        SqlTransaction _trans;
        public virtual void CustomerCompanyRegistrasiAdd(DocSolEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(ConnectionString);
            SqlParameter[] sqlParams;

            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[11];
                sqlParams[0] = new SqlParameter("@CustName", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.CompanyName;
                sqlParams[1] = new SqlParameter("@CustType", SqlDbType.VarChar, 2);
                sqlParams[1].Value = "C";
                sqlParams[2] = new SqlParameter("@PostingDate", SqlDbType.DateTime);
                sqlParams[2].Value = DateTime.Now;
                sqlParams[3] = new SqlParameter("@OfficeID", SqlDbType.Int);
                sqlParams[3].Value = 1; // Harusnya ambil dari session

                sqlParams[4] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                sqlParams[4].Value = _ent.UserLogin;
                sqlParams[5] = new SqlParameter("@DtmCrt", SqlDbType.SmallDateTime);
                sqlParams[5].Value = DateTime.Now;
                sqlParams[6] = new SqlParameter("@CustID", SqlDbType.BigInt);
                sqlParams[6].Direction = ParameterDirection.Output;

                sqlParams[7] = new SqlParameter("@IsEdit", SqlDbType.Bit);
                sqlParams[7].Value = _ent.IsEdit;
                sqlParams[8] = new SqlParameter("@CustIDReff", SqlDbType.BigInt);
                sqlParams[8].Value = _ent.CustomerID;
                sqlParams[9] = new SqlParameter("@UsrUpd", SqlDbType.VarChar,50);
                sqlParams[9].Value = _ent.UserLogin;
                sqlParams[10] = new SqlParameter("@DtmUpd", SqlDbType.SmallDateTime);
                sqlParams[10].Value = DateTime.Now;

                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spCustSave", sqlParams);
                if (!_ent.IsEdit)
                {
                   _ent.CustomerID = (long)sqlParams[6].Value;
                }
                
                sqlParams = new SqlParameter[17];
                sqlParams[0] = new SqlParameter("@CustID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.CustomerID;
                sqlParams[1] = new SqlParameter("@CoyAddress", SqlDbType.VarChar, 50);
                sqlParams[1].Value = _ent.CompanyAddress;
                sqlParams[2] = new SqlParameter("@CoyRT", SqlDbType.VarChar, 4);
                sqlParams[2].Value = _ent.CompanyRT;
                sqlParams[3] = new SqlParameter("@CoyRW", SqlDbType.VarChar, 4);
                sqlParams[3].Value = _ent.CompanyRW;
                sqlParams[4] = new SqlParameter("@CoyKelurahan", SqlDbType.VarChar, 50);
                sqlParams[4].Value = _ent.CompanyKelurahan;
                sqlParams[5] = new SqlParameter("@CoyKecamatan", SqlDbType.VarChar, 50);
                sqlParams[5].Value = _ent.CompanyKecamatan;
                sqlParams[6] = new SqlParameter("@CoyCity", SqlDbType.VarChar, 50);
                sqlParams[6].Value = _ent.CompanyCity;
                sqlParams[7] = new SqlParameter("@CoyZipCode", SqlDbType.VarChar, 50);
                sqlParams[7].Value = _ent.CompanyZipCode;
                sqlParams[8] = new SqlParameter("@CoyNPWP", SqlDbType.VarChar, 20);
                sqlParams[8].Value = _ent.CompanyNPWP;
                sqlParams[9] = new SqlParameter("@CoySIUP", SqlDbType.VarChar, 50);
                sqlParams[9].Value = _ent.CompanySiup;
                sqlParams[10] = new SqlParameter("@CoyTDP", SqlDbType.VarChar, 50);
                sqlParams[10].Value = _ent.CompanyTDP;
                sqlParams[11] = new SqlParameter("@CoyNotary", SqlDbType.VarChar, 50);
                sqlParams[11].Value = _ent.CompanyNotary;

                sqlParams[12] = new SqlParameter("@UsrCrt", SqlDbType.VarChar, 50);
                sqlParams[12].Value = _ent.UserLogin;
                sqlParams[13] = new SqlParameter("@DtmCrt", SqlDbType.SmallDateTime);
                sqlParams[13].Value = DateTime.Now;
                sqlParams[14] = new SqlParameter("@IsEdit", SqlDbType.Bit);
                sqlParams[14].Value = _ent.IsEdit;
                sqlParams[15] = new SqlParameter("@UsrUpd", SqlDbType.VarChar, 50);
                sqlParams[15].Value = _ent.UserLogin;
                sqlParams[16] = new SqlParameter("@DtmUpd", SqlDbType.SmallDateTime);
                sqlParams[16].Value = DateTime.Now;
                

                #endregion
              
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spCustCoySave", sqlParams);
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
                    ClassName = "CustomerRegistrasi",
                    FunctionName = "CustomerCompanyRegistrasiAdd",
                    ExceptionNumber = 1,
                    EventSource = "CustomerRegistrasi",
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

        public virtual DocSolEntities CustomerCompanyRegistrasiView(DocSolEntities _ent)
        {
            SqlParameter[] sqlParams;
            SqlDataReader _rdr;
            try
            {
                #region "List Parameter SQL"
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@CustID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.CustomerID;

                _rdr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "spCustCoyView", sqlParams);
                
                while (_rdr.Read())
                {

                    _ent.CompanyName = (string)_rdr["CustName"];
                    _ent.CompanyAddress = (string)_rdr["Address"];
                     _ent.CompanyRT = (string)_rdr["RT"];
                     _ent.CompanyRW =(string)_rdr["RW"];
                     _ent.CompanyKelurahan = (string)_rdr["Kelurahan"];
                     _ent.CompanyKecamatan = (string)_rdr["Kecamatan"];
                     _ent.CompanyCity = (string)_rdr["City"];
                     _ent.CompanyZipCode = (string)_rdr["ZipCode"];
                     _ent.CompanyNPWP = (string)_rdr["NPWP"];
                     _ent.CompanySiup = (string)_rdr["SIUP"];
                     _ent.CompanyTDP = (string)_rdr["TDP"];
                     _ent.CompanyNotary = (string)_rdr["AkteNo"];
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
    }
}
