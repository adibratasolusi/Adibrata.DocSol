//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Adibrata.Database.Script.StoreProcedure;
using Microsoft.SqlServer.Server;

namespace Adibrata.Database.Script.StoreProcedure
{
   public class JournalPostClass
    {

        SqlCommand _cmd = new SqlCommand();
        JournalEntities _ent = new JournalEntities();
        //MasterSequenceClass _seqno = new MasterSequenceClass();
        #region "Prepare tempplate information from Journal Job, Journal Scheme Header and Journal Scheme Detail"

        #region "Get data from Journal Job at table jrnlJob"

        public void GetJob(int _jobid)
        {
            // Fungsi ini untuk mengambil data di table JrnlJob. Dari table Journal Job terdapat kolom journal scheme. Kolom journal scheme harus di isi setiap transaksi
            // Journal scheme merupakan template journal yang akan di pakai. 
            // Table yang dipakai dalam procedure ini adalah JrnlJob, JrnlSchmHdr, JrnlSchmDtl

            StringBuilder sb = new StringBuilder();
            //CommonClass _common = new CommonClass();
            sb.AppendLine("Select SequenceNo, A.ID as JobID, A.JobDate as JobDate, A.TrxID, A.JrnlSchmID, A.CoaSchmID, A.CoyID, ");
            sb.AppendLine("B.AgrmntID,  ");
            sb.AppendLine("B.IsCreatePaymentHistory, B.IsCreateJournal, B.IsCreateCashBankMutation, B.WOP, B.OfficeID, B.OfficeID_X, B.JrnlFlags, B.JrnlTrxID, B.TableTrx, C.DepartID,  ");
            sb.AppendLine("C.COAName, C.COASourceTable, C.IsCoaHeader, C.Post, C.TblSourceDtl, C.ColSourceDtl, C.IsCreatePaymentHistoryDetail, C.IsMultipleDtl, C.ColFilterDtlID, ");
            sb.AppendLine("C.IsCreateCashBankMutationDetail, C.IsCreateJournalDetail, B.PostingDt,B.ReceivedFrom, B.ReceiptNo, B.AmountTrx, B.Trx_Code, ");
            sb.AppendLine("B.ValueDt, B.reffno, B.CurrID, B.CurrRate, B.RcvDisbFlag, B.CashierID, B.CashierOpen, B.BankAccId ");
            sb.AppendLine("FROM JrnlJob a WITH (NOLOCK) Inner Join JrnlSchmHdr B With (nolock) on A.JrnlSchmID = B.ID ");
            sb.AppendLine("Inner Join JrnlSchmDtl C with (nolock) on B.ID = C.JrnlSchmHdrID Where A.ID = @JobID and JobStatus = 'NE' ");
            sb.AppendLine("order by C.SequenceNo ");

            #region "Execute query"
            SqlDataReader _rdr;
            DataTable _dtjob = new DataTable();

            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    try
                    {
                        _cmd.CommandText = sb.ToString();
                        _cmd.Parameters.AddWithValue("@JobID", (int)_jobid);
                        _rdr = _cmd.ExecuteReader();
                        _dtjob.Load(_rdr);
                        _ent.dtTempJob = _dtjob;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                    finally
                    {
                        if (_conn.State == ConnectionState.Open) { _conn.Close(); }
                    }
                }
            }
            
            #endregion 

            _ent = GetValueTransaction(_ent);

            _ent = FillDataTempJournal(_ent);

            #region "Start Process to create Journal Information"
            if (_dtjob.Rows[1]["IsCreateJournal"].ToString() == "1")
            {
                MasterSequenceClass _proc = new MasterSequenceClass();
                _ent.JournalCodeNumber = _proc.GetJournalNo(_ent.OfficeID, _ent.JournalTransactionID, _ent.PostingDate); //Journal No

                SaveJournal(_ent);
            }
            #endregion

            #region "Start Process Create Cash Bank Mutation Information"
            if (_dtjob.Rows[1]["IsCreateCashBankMutation"].ToString() == "1")
            {
                MasterSequenceClass _proc = new MasterSequenceClass();
                _ent.VoucherNo = _proc.GetVoucherNo(_ent.BankAccountID, _ent.PostingDate);

                SaveCashBankTransaction(_ent);
            }
            #endregion

            #region "Start Process Create PaymentHistory Header Information"
            if (_dtjob.Rows[1]["IsCreatePaymentHistory"].ToString() == "1")
            {
                MasterSequenceClass _proc = new MasterSequenceClass();
                _ent.HistorySequenceNo = _proc.GetMaxHistorySequenceNo(_ent.AgrmntID);
                SavePaymentHistory(_ent);
            }
            #endregion 


        }

        #endregion 

        #region "Getting data from table transaction source, that already set at journal scheme header and journal scheme detail"

        private JournalEntities GetValueTransaction(JournalEntities _ent)
        {
            // TrxCode adalah id dari table transaksi (TableTrx)
            // Table Transaksi diambil dari journal scheme header
 
            StringBuilder sb = new StringBuilder();
            DataTable _dtjob = new DataTable();
            SqlDataReader _rdr;
            //MasterSequenceClass _seqno = new MasterSequenceClass();
            //CommonClass _common = new CommonClass();
            _dtjob = _ent.dtTempJob;

            if (_dtjob.Rows.Count > 0)
            {
                DataRow _row;
                _row = _dtjob.Rows[1];
                sb.AppendLine("Select ");
                sb.Append(_row["PostingDt"].ToString()); sb.AppendLine(" As PostingDate, ");
                sb.Append(_row["ValueDt"].ToString()); sb.AppendLine(" As ValueDate, ");
                if (_row["AgrmntID"].ToString() != "") { sb.Append(_row["AgrmntID"].ToString()); } else { sb.Append("''"); } sb.AppendLine(" As AgrmntID, ");

                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["ReffNo"].ToString());} else { sb.Append("''"); }  sb.AppendLine(" As ReffNo, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["BankAccId"].ToString());} else { sb.Append("''"); }  sb.AppendLine(" As BankAccountID, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["WOP"].ToString()); } else { sb.Append("''"); } sb.AppendLine(" As WayOfPayment, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["CurrID"].ToString()); } else { sb.Append("''"); } sb.AppendLine(" As CurrencyID, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["CurrRate"].ToString()); } else { sb.Append("''"); } sb.AppendLine(" As CurrencyRate, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["CashierID"].ToString());} else { sb.Append("''"); }  sb.AppendLine(" AS CashierID, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["CashierOpen"].ToString()); } else { sb.Append("''"); } sb.AppendLine(" AS OpeningCloseCashier, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["OfficeID"].ToString());} else { sb.Append("''"); }  sb.AppendLine(" AS OfficeID, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["OfficeID_X"].ToString()); } else { sb.Append("''"); } sb.AppendLine(" AS OfficeID_X, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["ReceivedFrom"].ToString()); } else { sb.Append("''"); } sb.AppendLine(" AS ReceivedFrom, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["ReceiptNo"].ToString());} else { sb.Append("''"); }  sb.AppendLine(" AS ReceiptNo, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["ReffNo"].ToString());} else { sb.Append("''"); }  sb.AppendLine(" AS ReferenceNo, ");
                if (_row["AgrmntID"].ToString() != "") {sb.Append(_row["Trx_Code"].ToString()); } else { sb.Append("''"); } sb.AppendLine(" AS TransactionCode, ");
                if (_row["AgrmntID"].ToString() != "") { sb.Append(_row["AmountTrx"].ToString()); } else { sb.Append("0"); } sb.AppendLine(" AS AmountTransaction "); 
                sb.Append(" From  ");
                sb.AppendLine(_row["TableTrx"].ToString()); sb.AppendLine(" with (nolock)  ");
                sb.Append(" Where ID = "); sb.Append(_row["TrxID"].ToString()); 
            }
            
            DataTable _dtoutput = new DataTable();
           
              using (SqlConnection _conn = new SqlConnection("context connection=true"))
              {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }

                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.CommandText = sb.ToString();


                    _rdr = _cmd.ExecuteReader();
                    try
                    {
                        while (_rdr.Read())
                        {
                            _ent.OfficeID = (int)_rdr["OfficeID"];
                            _ent.OfficeID_X = (int)_rdr["OfficeID_X"];
                            _ent.AgrmntID = (long)_rdr["AgrmntID"];

                            _ent.PostingDate = Convert.ToDateTime(_rdr["PostingDate"]).ToString();
                            _ent.ValueDate = Convert.ToDateTime(_rdr["ValueDate"]).ToString();

                            _ent.BankAccountID = (int)_rdr["BankAccountID"];
                            _ent.CurrencyID = (int)_rdr["CurrencyID"];
                            _ent.CurrencyRate = (decimal)_rdr["CurrencyRate"];

                            _ent.RefferenceNo = (string)_rdr["ReferenceNo"];
                            _ent.CashierID = (int)_rdr["CashierID"];
                            _ent.OpeningCloseCashier = (int)_rdr["OpeningCloseCashier"];
                            _ent.AmountTransaction = (decimal)_rdr["AmountTransaction"];
                            _ent.ReceiveFrom = (string)_rdr["ReceivedFrom"];
                            _ent.WayOfPayment = (string)_rdr["WayOfPayment"];
                    
                            _ent.IsActive = "1";
                            _ent.IsValid = "1";
                            _ent.Status_Tr = "OP";


                            _ent.PeriodMonth = Convert.ToDateTime(_ent.PostingDate).Month.ToString();
                            _ent.PeriodYear = Convert.ToDateTime(_ent.PostingDate).Year.ToString();
                        

                          
                            _ent.JournalTransactionID = (int)_dtjob.Rows[1]["JrnlTrxID"];

                            _ent.JobId = _dtjob.Rows[1]["JobID"].ToString();

                            _ent.UsrCrt = "ENGINE"; // Inputan parameter
                            _ent.DtmCrt = DateTime.Now.ToString();
                            _ent.JournalSchemeHeader = (int)_dtjob.Rows[1]["JrnlSchmID"];
                            _ent.ReceiveDisburseFlag = _dtjob.Rows[1]["RcvDisbFlag"].ToString();
                            _ent.JournalFlags = _dtjob.Rows[1]["JrnlFlags"].ToString();
                        }
                        _rdr.Close();
                    }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                    finally
                    {
                        if (_conn.State == ConnectionState.Open) { _conn.Close(); }
                    }
                    _ent = GetTransactionDescription(_ent);
                    
                    AgreementEntities _commonagrmnt = new AgreementEntities();
                    _commonagrmnt = CommonClass.AgreementStatus(_ent.AgrmntID);
                    _ent.ProductID = _commonagrmnt.ProductID;
                    _ent.CoyID = _commonagrmnt.CompanyID;

                }
          }
            return _ent;
        }

        #endregion 

        #endregion 
        
        #region "Get Coa Code"
       

        public JournalEntities GetCoaCode(JournalEntities _ent)
        {
            _ent.CoaCode = "";
       
            if (_ent.CoaSourceTable == "CoaSchmHdr")
            {
                
                _ent = GetCoaCodeFromCoaScheme(_ent);
            }
            else if (_ent.CoaSourceTable == "MS_BankAccount")
            { _ent = GetCoaCodeFromBankAccount(_ent); }
            else if (_ent.CoaSourceTable == "MS_InsuranceCoy")
            { }
            else if (_ent.CoaSourceTable == "MS_Funding")
            { }

            return _ent;
        }

        private JournalEntities GetCoaCodeFromCoaScheme(JournalEntities _ent)
        {
            _ent.CoaCode = ""; 
            StringBuilder sb = new StringBuilder();
            SqlDataReader _rdr;
            if (_ent.CoaSchemeID == 0)
            {
                sb.Append("Select CoaCode, CoaDesc from MS_Coa with (nolock) where CoaName = @CoaName ");
            }
            else
            {
                sb.Append("Select CoaCode, CoaDesc from CoaSchmDtl with (nolock) where CoaName = @CoaName and CoaSchmHdrID = @coaschmid");
            }
           
            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.Parameters.AddWithValue("@CoaName", _ent.CoaName);
                    if (_ent.CoaSchemeID != 0) { _cmd.Parameters.AddWithValue("@coaschmid", _ent.CoaSchemeID); }

                    _cmd.CommandText = sb.ToString();

                    _rdr = _cmd.ExecuteReader();
                    while (_rdr.Read())
                    {
                        _ent.CoaCode = (string)_rdr["CoaCode"];
                        _ent.CoaDescription = (string)_rdr["CoaDesc"];

                    }
                    _rdr.Close();

                    if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                }
            }
            return _ent;

        }
        JournalEntities GetCoaCodeFromBankAccount(JournalEntities _ent)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader _rdr;
            sb.Append("Select CoaCode from MS_BankAccount with (nolock) where ID = @BankAccountID");
            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.Parameters.AddWithValue("@BankAccountID", _ent.BankAccountID); 
                    _cmd.CommandText = sb.ToString();

                    _rdr = _cmd.ExecuteReader();
                    while (_rdr.Read())
                    {
                        _ent.CoaCode = (string)_rdr["CoaCode"];
                    

                    }
                    _rdr.Close();

                    if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                }
            }
            sb.Clear();
            sb.Append("Select TrxDesc from MS_JrnlTrx with (nolock) where ID = @ID");
            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.Parameters.AddWithValue("@ID", _ent.JournalTransactionID);
                    _cmd.CommandText = sb.ToString();


                    _ent.CoaDescription = _cmd.ExecuteScalar().ToString();

                    if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                }
            }

            //_ent.CoaDescription = (string)_rdr["CoaDesc"];
            return _ent;
        }


        string GetCoaCodeFromFunding()
        {
            return "";
        }

        string GetCoaCodeFromInsurance()
        {
            return "";

        }


       private  JournalEntities GetTransactionDescription (JournalEntities _ent)
        {
            StringBuilder sb = new StringBuilder();
            string _desc; 
            sb.Append("Select TrxDesc From MS_JrnlTrx with (nolock) where ID = ");
            sb.Append(_ent.JournalTransactionID);
            SqlCommand _cmd = new SqlCommand();
           
            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.CommandText = sb.ToString();

                    _desc =  _cmd.ExecuteScalar().ToString();

                    if (_conn.State == ConnectionState.Open) { _conn.Close(); }
                }
            }
            //SqlContext.Pipe.Send(_desc);
           sb.Clear();
           sb.Append (_desc); sb.Append(" - ") ; sb.Append (_ent.RefferenceNo);

            _ent.JorunalHeaderDescription = sb.ToString();
            _ent.CashBankDescription = sb.ToString();
            _ent.PaymentHistoryDescription = sb.ToString();
            sb.Clear();
            return _ent;
        }
        #endregion

        #region "------------- Prepare Temporary Table for detail transaction ------------------------------"

        #region "Create Table Temporary Detail"

        private DataTable PrepareTempJournal()
        {
            DataTable _dtTempJournalDetail = new DataTable();
            _dtTempJournalDetail.Columns.Add("SequenceNo", typeof(int));
            _dtTempJournalDetail.Columns.Add("CoaCode", typeof(String));
            _dtTempJournalDetail.Columns.Add("CoaName", typeof(String));
            _dtTempJournalDetail.Columns.Add("CoaCoy", typeof(int));
            _dtTempJournalDetail.Columns.Add("CoaOffice", typeof(String));
            _dtTempJournalDetail.Columns.Add("TrDesc", typeof(String));

            _dtTempJournalDetail.Columns.Add("Post", typeof(String));
            _dtTempJournalDetail.Columns.Add("AmountTrx", typeof(Decimal));
            _dtTempJournalDetail.Columns.Add("AmountOriTrx", typeof(Decimal));
            _dtTempJournalDetail.Columns.Add("ReferenceNo", typeof(String));
            _dtTempJournalDetail.Columns.Add("DepartId");
            _dtTempJournalDetail.Columns.Add("CoaCode_X", typeof(String));
            _dtTempJournalDetail.Columns.Add("CoaOffice_X", typeof(String));
            _dtTempJournalDetail.Columns.Add("CashBankDesc", typeof(String));
            _dtTempJournalDetail.Columns.Add("DebitAmount", typeof(Decimal));
            _dtTempJournalDetail.Columns.Add("CreditAmount", typeof(Decimal));
            _dtTempJournalDetail.Columns.Add("IsCreatePaymentHistoryDetail", typeof(String));
            _dtTempJournalDetail.Columns.Add("IsCreateCashBankMutationDetail", typeof(String));
            _dtTempJournalDetail.Columns.Add("IsCreateJournalDetail", typeof(String));
            return _dtTempJournalDetail;
        }
        #endregion 

        private JournalEntities GetDataDetail(JournalEntities _ent)
        {
          
            StringBuilder sb = new StringBuilder();
            //CommonClass _common = new CommonClass();
            SqlDataReader _rdr;
            DataTable _dtjrnldetail = new DataTable();
            DataTable _dttemp;
            _dttemp = _ent.dtJournalDetail;

         
            sb.Append("Select CoaName, Isnull(");
            sb.Append(_ent.ColSourceDetail); sb.AppendLine(",0) As Amount ");
            sb.Append(" From  ");
            sb.Append(_ent.TableSourceDetail); sb.AppendLine(" with (nolock)  ");
            sb.Append(" Where ");
            sb.Append(_ent.FilterSourceDetail); sb.Append(" = "); sb.Append(_ent.TransactionID);

            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.CommandText = sb.ToString();
                    _rdr = _cmd.ExecuteReader();
                    _dtjrnldetail.Load(_rdr);

                    _rdr.Close();
                    if (_conn.State == ConnectionState.Open) { _conn.Close(); }
                }
            }
            sb.Clear();
            DataRow _rowtemp;
            
            if (_dtjrnldetail.Rows.Count > 0)
            {
                foreach (DataRow _row in _dtjrnldetail.Rows)
                {
                      _rowtemp = _dttemp.NewRow();
                      _rowtemp["SequenceNo"] = _ent.Counter;
                      _rowtemp["CoaName"] = _row["CoaName"];
                      _ent.CoaName = (string)_row["CoaName"];
                      _ent = GetCoaCode(_ent); // Call function to get coa
                       _rowtemp["CoaCode"] = _ent.CoaCode;

                        // "*************Get Coa Description *****************"

                        _rowtemp["TrDesc"] = _ent.CoaDescription + "-" + _ent.RefferenceNo;
                        _rowtemp["IsCreatePaymentHistoryDetail"] = _ent.IsCreatePaymentHistoryDetail;
                        _rowtemp["IsCreateCashBankMutationDetail"] = _ent.IsCreateCashBankTransactionDetail;
                        _rowtemp["IsCreateJournalDetail"] = _ent.IsCreateJournalDetail;

                        _rowtemp["CoaOffice"] = _ent.OfficeID;
                        _rowtemp["Post"] = _ent.Post;

                        _rowtemp["AmountTrx"] = (decimal)_row["Amount"] * _ent.CurrencyRate;
                        _rowtemp["AmountOriTrx"] = (decimal)_row["Amount"];
                        _rowtemp["ReferenceNo"] = _ent.RefferenceNo;
                        //SqlContext.Pipe.Send(_rowtemp["AmountTrx"].ToString());
                        _rowtemp["CoaCode_X"] = _ent.CoaCode_X;
                        _rowtemp["CoaOffice_X"] = _ent.OfficeID_X;
                        _rowtemp["CashBankDesc"] = "";
                        if ((decimal)_row["Amount"] < 0) _rowtemp["CreditAmount"] = (decimal)_row["Amount"]; else _rowtemp["DebitAmount"] = (decimal)_row["Amount"];

                        _rowtemp["CreditAmount"] = 0;
                        if (Convert.ToDecimal(_rowtemp["AmountOriTrx"]) != 0)
                        {

                            if (_rowtemp["Post"].ToString() == "D") _ent.TotalDebit += (decimal)_row["Amount"]; else _ent.TotalCredit += (decimal)_row["Amount"];
                            _ent.Counter += 1;
                            _dttemp.Rows.Add(_rowtemp);
                        }

                      
                }
            }          
            _dttemp.AcceptChanges();

            
            _ent.dtJournalDetail = _dttemp;
            return _ent;
        }

        private JournalEntities FillDataTempJournal(JournalEntities _ent)
        {
            DataTable _dtjrnlschemedetail;
            DataTable _dttemp;
            StringBuilder sb = new StringBuilder();
            //CommonClass _common = new CommonClass();
            DataRow _rowtemp;
            SqlDataReader _rdr;
            _ent.TotalDebit = 0;
            _ent.TotalCredit = 0; 

            _dtjrnlschemedetail = _ent.dtTempJob; // _dtJrnlSchemedetail di isi dari table journal job, journal scheme header dan journal scheme detail

            _dttemp = PrepareTempJournal();
            _ent.Counter = 1;
            
            if (_dtjrnlschemedetail.Rows.Count > 0)
            {
                foreach (DataRow _row in _dtjrnlschemedetail.Rows)
                {
                    _rowtemp = _dttemp.NewRow();
                    _ent.CoaSourceTable = _row["CoaSourceTable"].ToString();
                    _ent.TableSourceDetail = _row["TblSourceDtl"].ToString();
                    _ent.ColSourceDetail = _row["ColSourceDtl"].ToString();
                    _ent.FilterSourceDetail = _row["ColFilterDtlID"].ToString();
                    _ent.TransactionID = _row["TrxID"].ToString(); 
                    if (_row["CoaSchmID"] == System.DBNull.Value) { _ent.CoaSchemeID = 0; } else { _ent.CoaSchemeID = (int)_row["CoaSchmID"]; }
                    _rowtemp["IsCreatePaymentHistoryDetail"] = _row["IsCreatePaymentHistoryDetail"];
                    _rowtemp["IsCreateCashBankMutationDetail"] = _row["IsCreateCashBankMutationDetail"];
                    _rowtemp["IsCreateJournalDetail"] = _row["IsCreateJournalDetail"];
                    _ent.IsCreateCashBankTransactionDetail = (string)_row["IsCreateCashBankMutationDetail"];
                    _ent.IsCreateJournalDetail = (string)_row["IsCreateJournalDetail"];
                    _ent.IsCreatePaymentHistoryDetail = (string)_row["IsCreatePaymentHistoryDetail"];
                    _ent.Post = (string)_row["Post"];
                    _rowtemp["CoaOffice"] = _ent.OfficeID;
                  
                    if (_row["IsMultipleDtl"].ToString() == "")
                    {
                        // ***********************Prepare COA CODE And Description *****************************"
                        _ent.CoaName = _row["CoaName"].ToString();
                        _ent = GetCoaCode(_ent); // Call function to get coa
                        _rowtemp["CoaCode"] = _ent.CoaCode;
                        if (_row["IsCoaHeader"].ToString() == "1")
                        { _ent.CoaCode_X = _ent.CoaCode; }

                        // "*************Get Coa Description *****************"
                        _rowtemp["TrDesc"] = _ent.CoaDescription + "-" + _ent.RefferenceNo;
                        sb.AppendLine("Select Isnull (");
                        sb.Append(_ent.ColSourceDetail); sb.AppendLine(",0) As Amount ");
                        if (_row["DepartID"].ToString() != "")
                        {
                            sb.Append(",");
                            sb.Append(_row["DepartID"].ToString());
                        }
                        sb.Append(" From  "); sb.Append(_ent.TableSourceDetail); sb.Append(" with (nolock)  ");
                        sb.Append(" Where ID = "); sb.Append( _ent.TransactionID );
                        
                        using (SqlConnection _conn = new SqlConnection("context connection=true"))
                        {
                            if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                            using (_cmd = _conn.CreateCommand())
                            {
                                _cmd.CommandText = sb.ToString();
                                _rdr = _cmd.ExecuteReader();
                                while (_rdr.Read())
                                {
                                    _ent.AmountDetail = (decimal)_rdr["Amount"];
                                    if (_row["DepartID"].ToString() != "")
                                    { _rowtemp["DepartId"]  = (string)_rdr["DepartID"]; }
                                }
                                _rdr.Close();
                                if (_conn.State == ConnectionState.Open) { _conn.Close(); }
                            }
                        }
                        sb.Clear();
                   
                        _rowtemp["SequenceNo"] = _ent.Counter;

                        _rowtemp["CoaName"] = _row["CoaName"].ToString();
                        _rowtemp["AmountTrx"] = _ent.AmountDetail * _ent.CurrencyRate;
                        _rowtemp["Post"] = _row["Post"].ToString();
                        if (_ent.AmountDetail < 0)
                        {
                            if (_row["Post"].ToString() =="D") _rowtemp["Post"] = "C"; 
                            else if (_row["Post"].ToString() == "C") _rowtemp["Post"] = "D"; 
                            
                        }

                        _rowtemp["AmountOriTrx"] = _ent.AmountDetail;
                        _rowtemp["ReferenceNo"] = _ent.RefferenceNo;
                       
                        _rowtemp["CoaCode_X"] = _ent.CoaCode_X;
                        _rowtemp["CoaOffice_X"] = _ent.OfficeID_X;

                        _rowtemp["CashBankDesc"] = "";
                        if (_ent.AmountDetail < 0) _rowtemp["CreditAmount"] = _ent.AmountDetail; else _rowtemp["DebitAmount"] = _ent.AmountDetail; 

                        _rowtemp["CreditAmount"] = 0;
                        if (Convert.ToDecimal(_rowtemp["AmountOriTrx"]) != 0)
                        {
                            if (_rowtemp["Post"].ToString() == "D") _ent.TotalDebit += _ent.AmountDetail; else _ent.TotalCredit += _ent.AmountDetail; 
                            _ent.Counter += 1;
                            _dttemp.Rows.Add(_rowtemp);
                        }
                    }
                    else {
                        _ent.dtJournalDetail = _dttemp;
                        _ent = GetDataDetail(_ent); 
                    }

                }
            }
        
            if (_ent.TotalDebit !=  _ent.TotalCredit) { throw new Exception("Journal Not Balance"); }

            _dttemp.AcceptChanges();
            
            if (_dttemp.Rows.Count <= 0) { throw new Exception("No Transaction Detail, please check transaction detail"); }
            _ent.dtTempJournalDetail = _dttemp;
            return _ent;
        }

        #endregion
     
        #region "Posting Journal Transaction"

        private void SaveJournal(JournalEntities _ent)
        {
            StringBuilder _sjournalheader = new StringBuilder();
            StringBuilder _sjournaldetail = new StringBuilder();
            StringBuilder _sjrnlhdrid = new StringBuilder();
            //CommonClass _common = new CommonClass();
            #region "Saving Journal Header"
            _sjournalheader.AppendLine(" Insert Into Jrnl_Trx_Hdr ([OfficeID], [JrnlCode], [PeriodYear], [PeriodMonth], [TrxDate], [Reff_No], [Reff_Date], ");
            _sjournalheader.AppendLine(" [TrxDesc], [JrnlAmt], [Status_Tr], [Flag], [IsActive], [IsValid], [AgrmntID], [JrnlTrxID], [JrnlSchmHdrId], ");
            _sjournalheader.AppendLine(" [JobID], [UsrCrt] ,[DtmCrt]) ");
            _sjournalheader.AppendLine(" Values (@OfficeID, @JrnlCode, @PeriodYear, @PeriodMonth, @TrxDate, @ReffNo, @ReffDate,");
            _sjournalheader.AppendLine(" @TrxDesc, @JrnlAmt, @Status_Tr, @Flags, @IsActive, @IsValid, @AgrmntID, @JrnlTrxID, @JrnlSchmHdrID, @JobId, ");
            _sjournalheader.AppendLine(" @UsrCrt, @DtmCrt) ");

            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.CommandText = _sjournalheader.ToString();
                    _cmd.Connection = _conn;

                    _cmd.Parameters.AddWithValue("@OfficeID", _ent.OfficeID);
                    _cmd.Parameters.AddWithValue("@JrnlCode", _ent.JournalCodeNumber);
                    _cmd.Parameters.AddWithValue("@PeriodYear", _ent.PeriodYear);
                    _cmd.Parameters.AddWithValue("@PeriodMonth", _ent.PeriodMonth);
                    _cmd.Parameters.AddWithValue("@TrxDate", _ent.PostingDate);
                    _cmd.Parameters.AddWithValue("@ReffNo", _ent.RefferenceNo);
                    _cmd.Parameters.AddWithValue("@ReffDate", _ent.ValueDate);
                    _cmd.Parameters.AddWithValue("@TrxDesc", _ent.JorunalHeaderDescription);
                    _cmd.Parameters.AddWithValue("@JrnlAmt", _ent.AmountTransaction);
                    
                    _cmd.Parameters.AddWithValue("@Status_Tr", _ent.Status_Tr);
                    _cmd.Parameters.AddWithValue("@IsActive", _ent.IsActive);
                    _cmd.Parameters.AddWithValue("@IsValid", _ent.IsValid);
                    _cmd.Parameters.AddWithValue("@Flags", _ent.JournalFlags);
                    _cmd.Parameters.AddWithValue("@AgrmntID", _ent.AgrmntID);
                    _cmd.Parameters.AddWithValue("@JrnlTrxID", _ent.JournalTransactionID);
                    _cmd.Parameters.AddWithValue("@JrnlSchmHdrID", _ent.JournalSchemeHeader);
                    _cmd.Parameters.AddWithValue("@JobId", _ent.JobId);
                    _cmd.Parameters.AddWithValue("@UsrCrt", _ent.UsrCrt);
                    _cmd.Parameters.AddWithValue("@DtmCrt", _ent.DtmCrt);
                    try { int _rowaffected = _cmd.ExecuteNonQuery(); }
                    catch (Exception exp) { throw new Exception(exp.Message); }
                    finally { if (_conn.State == ConnectionState.Open) { _conn.Close(); } }
                }
            }
            #endregion

            #region "Getting Journal Header ID"

            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    try
                    {
                        _cmd.CommandText = "Select ID From Jrnl_Trx_Hdr With (nolock) where JrnlCode = @JrnlCode ";
                        _cmd.Parameters.AddWithValue("@JrnlCode", _ent.JournalCodeNumber);
                        _ent.JournalHeaderID = Convert.ToInt64(_cmd.ExecuteScalar());
                    }
                    catch (Exception exp) { throw new Exception(exp.Message); }
                    finally { if (_conn.State == ConnectionState.Open) { _conn.Close(); } }
                }
            }

            #endregion

            #region "Saving Journal Detail"

            _sjournaldetail.Append("Insert Into Jrnl_Trx_Dtl ([Jrnl_Trx_Hdr_ID], [SequenceNo], [CoaCoy], [CoaOffice], [CoaCode],  [JrnlTrxID], [Tr_Desc], [Post], [CurrID], ");
            _sjournaldetail.AppendLine("[Curr_Rate], [Amt], [AmtOri], [CoaCode_X], [CoaOffice_X], [CoaName], [ProductId], [DepartID], [UsrCrt], [DtmCrt])");
            _sjournaldetail.Append("values (@JrnlTrxHdrID, @SequenceNo, @CoaCoy, @CoaOffice, @CoaCode, @JrnlTrxID, @TrDesc, @Post, @CurrId, @CurrRate, @Amount, @AmountOri, ");
            _sjournaldetail.AppendLine("@CoaCode_X, @CoaOffice_X, @CoaName, @ProductID, @DepartId, @UsrCrt, @DtmCrt)");

            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {

                using (_cmd = _conn.CreateCommand())
                {
                    if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                    _cmd.CommandText = _sjournaldetail.ToString();
                    _cmd.Connection = _conn;
                    try
                    {
                        if (_ent.dtTempJournalDetail.Rows.Count > 0)
                        {
                            foreach (DataRow _row in _ent.dtTempJournalDetail.Rows)
                            {
                               
                                _cmd.Parameters.AddWithValue("@JrnlTrxHdrID", _ent.JournalHeaderID);
                                _cmd.Parameters.AddWithValue("@SequenceNo", (int)_row["SequenceNo"]);
                                //if (_row["CoaCoy"] == System.DBNull.Value) _cmd.Parameters.AddWithValue("@CoaCoy", System.DBNull.Value); else _cmd.Parameters.AddWithValue("@CoaCoy", (string)_row["CoaCoy"]);
                                _cmd.Parameters.AddWithValue("@CoaCoy", _ent.CoyID);
                                
                                _cmd.Parameters.AddWithValue("@CoaOffice", (string)_row["CoaOffice"]);
                                _cmd.Parameters.AddWithValue("@CoaCode", (string)_row["CoaCode"]);
                                _cmd.Parameters.AddWithValue("@JrnlTrxID", _ent.JournalTransactionID);
                                _cmd.Parameters.AddWithValue("@TrDesc", (string)_row["TrDesc"]);
                                _cmd.Parameters.AddWithValue("@Post", (string)_row["Post"]);
                                _cmd.Parameters.AddWithValue("@CurrId", _ent.CurrencyID);
                                _cmd.Parameters.AddWithValue("@CurrRate", _ent.CurrencyRate);

                                _cmd.Parameters.AddWithValue("@Amount", (decimal)_row["AmountTrx"]);
                                _cmd.Parameters.AddWithValue("@AmountOri", (decimal)_row["AmountOriTrx"]);
                                _cmd.Parameters.AddWithValue("@CoaCode_X", (string)_row["CoaCode_X"]);
                                _cmd.Parameters.AddWithValue("@CoaOffice_X", (string)_row["CoaOffice_X"]);
                                _cmd.Parameters.AddWithValue("@CoaName", (string)_row["CoaName"]);
                                _cmd.Parameters.AddWithValue("@ProductID", _ent.ProductID);
                                if (_row["DepartID"] == System.DBNull.Value) _cmd.Parameters.AddWithValue("@DepartId", System.DBNull.Value);  else _cmd.Parameters.AddWithValue("@DepartId", (int)_row["DepartId"]);
                                
                                _cmd.Parameters.AddWithValue("@UsrCrt", _ent.UsrCrt);
                                _cmd.Parameters.AddWithValue("@DtmCrt", _ent.DtmCrt);
                                int _rowaffected = _cmd.ExecuteNonQuery();
                                _cmd.Parameters.Clear();
                            }
                        }
                    }
                    catch (Exception exp) { throw new Exception(exp.Message); }

                    finally { if (_conn.State == ConnectionState.Open) { _conn.Close(); } }

                }
            }


            #endregion
        }

        #endregion

        #region "Saving Data to Cash Bank Mutation at CashBankMutHdr and CashBankMutDtl"
        private void SaveCashBankTransaction(JournalEntities _ent)
        {
            StringBuilder _scashbankheader = new StringBuilder();
            StringBuilder _scashbankldetail = new StringBuilder();
            StringBuilder _scashbankhdrid = new StringBuilder();
            //MasterSequenceClass _seqno = new MasterSequenceClass();
            #region "Saving Cash Bank Transaction Header"
            _scashbankheader.Append("Insert Into [CashBankMutHdr] ([OfficeID], [VoucherNo], [ValueDt], [PostingDt], [Description], ");
            _scashbankheader.Append("[RcvDsbFlag], [WOP], [Amount] , [RcvFrom], [ReffNo], [ReceiptNo], ");
            _scashbankheader.Append("[BankAccID], [CurrID], [CashierId], [OpeningSequence], [OfficeID_X],");
            _scashbankheader.Append("[JrnlTrxID], [AgrmntID], [JrnlShmHdrID],");
            _scashbankheader.AppendLine("[JrnlCode], [JrnlJobID], [UsrCrt], [DtmCrt])");
            _scashbankheader.Append("        Values (@OfficeID, @VoucherNo, @ValueDate, @PostingDate, @Description,  ");
            _scashbankheader.Append("                 @RcvDsbFlag, @WOP, @Amount, @RcvFrom , @ReffNo, @ReceiptNo, ");
            _scashbankheader.Append("                  @BankAccID, @CurrId, @CashierID, @OpeningSequence, @OfficeID_X, ");
            _scashbankheader.Append("                 @JrnlTrxID, @AgrmntID, @JrnlSchmHdrID, ");
            _scashbankheader.Append("              @JrnlCode, @JrnlJobID, @UsrCrt, @DtmCrt)");

            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.CommandText = _scashbankheader.ToString();
                    _cmd.Connection = _conn;

                    _cmd.Parameters.AddWithValue("@OfficeID", _ent.OfficeID);
                    _cmd.Parameters.AddWithValue("@VoucherNo", _ent.VoucherNo);
                    _cmd.Parameters.AddWithValue("@ValueDate", _ent.ValueDate);
                    _cmd.Parameters.AddWithValue("@PostingDate", _ent.PostingDate);
                    _cmd.Parameters.AddWithValue("@Description", _ent.CashBankDescription);
                    _cmd.Parameters.AddWithValue("@RcvDsbFlag", _ent.ReceiveDisburseFlag);
                    _cmd.Parameters.AddWithValue("@WOP", _ent.WayOfPayment);
                    _cmd.Parameters.AddWithValue("@Amount", _ent.AmountTransaction);
                    _cmd.Parameters.AddWithValue("@RcvFrom", _ent.ReceiveFrom);

                    _cmd.Parameters.AddWithValue("@ReffNo", _ent.VoucherNo);
                    _cmd.Parameters.AddWithValue("@ReceiptNo", _ent.ReceiptNo);


                    _cmd.Parameters.AddWithValue("@BankAccID", _ent.BankAccountID);
                    _cmd.Parameters.AddWithValue("@CurrId", _ent.CurrencyID);

                    _cmd.Parameters.AddWithValue("@CashierID", _ent.CashierID);
                    _cmd.Parameters.AddWithValue("@OpeningSequence", _ent.OpeningCloseCashier);

                    _cmd.Parameters.AddWithValue("@JrnlTrxID", _ent.JournalTransactionID);

                    _cmd.Parameters.AddWithValue("@AgrmntID", _ent.AgrmntID);

                    _cmd.Parameters.AddWithValue("@JrnlSchmHdrID", _ent.JournalSchemeHeader);

                    _cmd.Parameters.AddWithValue("@OfficeID_X", _ent.OfficeID_X);
                    _cmd.Parameters.AddWithValue("@JrnlCode", _ent.JournalCodeNumber);
                    _cmd.Parameters.AddWithValue("@JrnlJobID", _ent.JobId);
                    _cmd.Parameters.AddWithValue("@UsrCrt", _ent.UsrCrt);
                    _cmd.Parameters.AddWithValue("@DtmCrt", _ent.DtmCrt);
                    try { int _rowaffected = _cmd.ExecuteNonQuery(); }
                    catch (Exception exp) { throw new Exception(exp.Message); }
                    finally { if (_conn.State == ConnectionState.Open) { _conn.Close(); } }
                }
            }
            #endregion

            #region "Getting Cash Bank Mutation Header"
            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    try
                    {
                        _cmd.CommandText = "Select ID From CashBankMutHdr With (nolock) where VoucherNo = @VoucherNo ";
                        _cmd.Parameters.AddWithValue("@VoucherNo", _ent.VoucherNo);
                        _ent.CashBankMutationHeaderID = Convert.ToInt64 (_cmd.ExecuteScalar());
                        
                    }
                    catch (Exception exp) { throw new Exception(exp.Message); }
                    finally { if (_conn.State == ConnectionState.Open) { _conn.Close(); } }
                }
            }
            #endregion

            #region "Saving Cash Bank Mutation Detail"
            _scashbankldetail.AppendLine("Insert Into  [CashBankMutDtl] ([CashBankMutHdrID], [DepartID], [CoaName],  [CoaCode], [Description], [DebitAmt], [CreditAmt], [UsrCrt], [DtmCrt])");
            _scashbankldetail.AppendLine("Values (@CashBankMutHdrID, @DepartID, @CoaName, @CoaCode, @Decription, @DebitAmount, @CreditAmount, @UsrCrt, @DtmCrt)");

            //CommonClass _common = new CommonClass();
            
            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {

                using (_cmd = _conn.CreateCommand())
                {
                    if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                    _cmd.CommandText = _scashbankldetail.ToString();
                    _cmd.Connection = _conn;
                    try
                    {
                        if (_ent.dtTempJournalDetail.Rows.Count > 0)
                        {
                            foreach (DataRow _row in _ent.dtTempJournalDetail.Rows)
                            {
                                if ((string)_row["CoaName"] != "BANKACCOUNT")
                                {
                                    _cmd.Parameters.AddWithValue("@CashBankMutHdrID", _ent.CashBankMutationHeaderID);
                                    if (_row["DepartID"] == System.DBNull.Value)
                                        _cmd.Parameters.AddWithValue("@DepartID", System.DBNull.Value);
                                    else
                                        _cmd.Parameters.AddWithValue("@DepartID", (int) _row["DepartID"]);
                                    _cmd.Parameters.AddWithValue("@CoaName", (string)_row["CoaName"]);
                                    _cmd.Parameters.AddWithValue("@CoaCode", (string)_row["CoaCode"]);
                                    _cmd.Parameters.AddWithValue("@Decription", (string)_row["CashBankDesc"]);
                                    _cmd.Parameters.AddWithValue("@DebitAmount", (decimal)_row["DebitAmount"]);
                                    _cmd.Parameters.AddWithValue("@CreditAmount", (decimal)_row["CreditAmount"]);

                                    _cmd.Parameters.AddWithValue("@UsrCrt", _ent.UsrCrt);
                                    _cmd.Parameters.AddWithValue("@DtmCrt", _ent.DtmCrt);
                                    int _rowaffected = _cmd.ExecuteNonQuery();
                                    _cmd.Parameters.Clear();
                                }
                            }
                        }
                    }
                    catch (Exception exp) { throw new Exception(exp.Message); }

                    finally { if (_conn.State == ConnectionState.Open) { _conn.Close(); } }

                }
            }

            #endregion
        }
        #endregion 

        #region "Saving Payment History at PayHistHdr and PayHistDtl"

        private DataTable PrepareTempPaymentHistoryDetail ()
        {
            DataTable _dttempphd = new DataTable();

            _dttempphd.Columns.Add("CoaName", typeof(string));
            _dttempphd.Columns.Add("InstSeqNo", typeof(int));
            _dttempphd.Columns.Add("AssetSeqNo", typeof(int));
            _dttempphd.Columns.Add("YearNum", typeof(int));
            _dttempphd.Columns.Add("Description", typeof(String));
            _dttempphd.Columns.Add("DebitAmt", typeof(decimal));
            _dttempphd.Columns.Add("CreditAmt", typeof(decimal));
            _dttempphd.Columns.Add("LCAmt", typeof(decimal));
            _dttempphd.Columns.Add("LCDays", typeof(int));
            return _dttempphd;
        }

        private DataTable FillDataTempPaymentHistoryDetail (JournalEntities _ent, DataTable _dttempphd)
        {
            StringBuilder sb = new StringBuilder();
            DataTable _dtpayrcvdtl = new DataTable ();
            SqlDataReader _rdr;
            DataRow _rowphd;
      
            
            if (_ent.dtTempJournalDetail.Rows.Count > 0)
            {
                foreach (DataRow _row in _ent.dtTempJournalDetail.Rows)
                {

                    if (_row["CoaName"].ToString() == "INSTALLPAID")
                    {
                        sb.Clear();
                        sb.Append("Select InstSeqNo, AssetSeqNo, YearNum, AmtRcv,  LCAmt, LCDays from PayRcvDtl with (nolock) where PayRcvId = ");
                        sb.Append(_ent.TransactionID);
                        sb.Append(" and IsInstallPaid = 1");
                        
                        using (SqlConnection _conn = new SqlConnection("context connection=true"))
                        {
                            using (_cmd = _conn.CreateCommand())
                            {

                                if (_conn.State == ConnectionState.Closed) _conn.Open();
                                _cmd.CommandText = sb.ToString();
                                _cmd.Connection = _conn;
                                try
                                {
                                    _dtpayrcvdtl.Clear();
                                    _rdr = _cmd.ExecuteReader();
                                    _dtpayrcvdtl.Load(_rdr);
                                    _rdr.Close();
                                }
                                catch (Exception Exp) { throw new Exception(Exp.Message); }
                                finally { if (_conn.State == ConnectionState.Open)  _conn.Close(); }
                                
                                foreach (DataRow _rowpayrcvdtl in _dtpayrcvdtl.Rows)
                                {
                                    _rowphd = _dttempphd.NewRow();
                                    _rowphd["CoaName"] = _row["CoaName"];
                                    _rowphd["InstSeqNo"] = _rowpayrcvdtl["InstSeqNo"];
                                    _rowphd["AssetSeqNo"] = 0;
                                    _rowphd["YearNum"] = 0;
                                    _rowphd["DebitAmt"] = 0;
                                    _rowphd["CreditAmt"] = 0;
                                    if ((decimal)_rowpayrcvdtl["AmtRcv"] > 0) _rowphd["DebitAmt"] = _rowpayrcvdtl["AmtRcv"]; else _rowphd["CreditAmt"] = -1 * (decimal)_rowpayrcvdtl["AmtRcv"];
                                    _rowphd["LCAmt"] = _rowpayrcvdtl["LCAmt"];
                                    _rowphd["LCDays"] = _rowpayrcvdtl["LCDays"];
                                    _dttempphd.Rows.Add(_rowphd);
                                }
                                _dttempphd.AcceptChanges();
                            }
                        }
                    }
                    else if (_row["CoaName"].ToString() == "INSURPAID")
                    {
                        sb.Clear();
                        sb.Append("Select InstSeqNo, AssetSeqNo, YearNum, AmtRcv,  LCAmt, LCDays from PayRcvDtl with (nolock) where PayRcvId = ");
                        sb.Append(_ent.TransactionID);
                        sb.Append(" and IsInsurancePaid = 1");

                        using (SqlConnection _conn = new SqlConnection("context connection=true"))
                        {
                            using (_cmd = _conn.CreateCommand())
                            {

                                if (_conn.State == ConnectionState.Closed) _conn.Open();
                                _cmd.CommandText = sb.ToString();
                                _cmd.Connection = _conn;
                                try
                                {
                                    _dtpayrcvdtl.Clear();
                                    _rdr = _cmd.ExecuteReader();
                                    _dtpayrcvdtl.Load(_rdr);
                                    _rdr.Close();
                                }
                                catch (Exception Exp) { throw new Exception(Exp.Message); }
                                finally { if (_conn.State == ConnectionState.Open)  _conn.Close(); }
                                foreach (DataRow _rowpayrcvdtl in _dtpayrcvdtl.Rows)
                                {
                                    _rowphd = _dttempphd.NewRow();
                                    _rowphd["CoaName"] = _row["CoaName"];
                                    _rowphd["InstSeqNo"] = 0;
                                    _rowphd["AssetSeqNo"] = _rowpayrcvdtl["AssetSeqNo"];
                                    _rowphd["YearNum"] = _rowpayrcvdtl["YearNum"];
                                    _rowphd["DebitAmt"] = 0;
                                    _rowphd["CreditAmt"] = 0;
                                    if ((decimal)_rowpayrcvdtl["AmtRcv"] > 0) _rowphd["DebitAmt"] = _rowpayrcvdtl["AmtRcv"]; else _rowphd["CreditAmt"] = -1 * (decimal)_rowpayrcvdtl["AmtRcv"];
                                    _rowphd["LCAmt"] = _rowpayrcvdtl["LCAmt"];
                                    _rowphd["LCDays"] = _rowpayrcvdtl["LCDays"];
                                    _dttempphd.Rows.Add(_rowphd);
                                }
                                _dttempphd.AcceptChanges();
                            }
                        }
                    }
                    else
                    {
                        if (_row["CoaName"].ToString () != "BANKACCOUNT")
                        {
                            _rowphd = _dttempphd.NewRow();
                            _rowphd["CoaName"] = _row["CoaName"];
                            _rowphd["InstSeqNo"] = 0;
                            _rowphd["AssetSeqNo"] = 0;
                            _rowphd["YearNum"] = 0;

                            _rowphd["DebitAmt"] = 0;
                            _rowphd["CreditAmt"] = 0;
                            if ((decimal)_row["AmountOriTrx"] > 0) _rowphd["DebitAmt"] = _row["AmountOriTrx"]; else _rowphd["CreditAmt"] = -1 * (decimal)_row["AmountOriTrx"];
                            _rowphd["LCAmt"] = 0;
                            _rowphd["LCDays"] = 0;
                            _dttempphd.Rows.Add(_rowphd);
                        }
                    }
        
                }
            }
            return _dttempphd;
        }
        private void SavePaymentHistory(JournalEntities _ent)
        {
            //MasterSequenceClass _seqno = new MasterSequenceClass();
            StringBuilder _spaymenthistoryheader = new StringBuilder();
            StringBuilder _spaymenthistorydetail = new StringBuilder();
            StringBuilder _spaymenthistoryhdrid = new StringBuilder();
            //CommonClass _common = new CommonClass();
            AgreementEntities _agrmntentities = new AgreementEntities();
            DataTable _dttempphd;


            #region "Saving Payment History Header"
            _spaymenthistoryheader.AppendLine("Insert Into PayHistHdr ([AgrmntID], [HistSeqNo], [ValueDt], [PostingDt], ");
            _spaymenthistoryheader.AppendLine("                        [Amt], [WOP], [OfficeID_X], [BankAccID],  [IsCorrection], [CorrHistSeq],  ");
            _spaymenthistoryheader.AppendLine("                        [JrnlCode], [VoucherNo], [JrnlTrxID], [DefaultStat], [JrnlJobID], [UsrCrt], [DtmCrt]) ");
            _spaymenthistoryheader.AppendLine("                 Values (@AgrmntID, @HistSeqNo, @ValueDate, @PostingDate, ");
            _spaymenthistoryheader.AppendLine("                         @Amount, @WOP, @OfficeID_X, @BankkAccountID, @IsCorrection, @CorrectionHistorySeq, ");
            _spaymenthistoryheader.AppendLine("                         @JournalCode, @VoucherNo, @JrnlTrxID, @DefaultStat, @JrnlJobId, @UsrCrt, @DtmCrt )");
            string _defaultstatus = CommonClass.AgreementStatus(_ent.AgrmntID).DefaultStatus;

            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }


                using (_cmd = _conn.CreateCommand())
                {
                    _cmd.CommandText = _spaymenthistoryheader.ToString();
                    _cmd.Connection = _conn;
                    _cmd.Parameters.AddWithValue("@AgrmntID", _ent.AgrmntID);
                    _cmd.Parameters.AddWithValue("@HistSeqNo", _ent.HistorySequenceNo);
                    _cmd.Parameters.AddWithValue("@ValueDate", _ent.ValueDate);
                    _cmd.Parameters.AddWithValue("@PostingDate", _ent.PostingDate);
                    _cmd.Parameters.AddWithValue("@Amount", _ent.AmountTransaction);
                    _cmd.Parameters.AddWithValue("@WOP", _ent.WayOfPayment);
                    _cmd.Parameters.AddWithValue("@OfficeID_X", _ent.OfficeID_X);

                    _cmd.Parameters.AddWithValue("@BankkAccountID", _ent.BankAccountID);
                    _cmd.Parameters.AddWithValue("@IsCorrection", "0");
                    _cmd.Parameters.AddWithValue("@CorrectionHistorySeq", "0");
                    _cmd.Parameters.AddWithValue("@JournalCode", _ent.JournalCodeNumber);
                    _cmd.Parameters.AddWithValue("@JrnlTrxID", _ent.JournalTransactionID);
                    _cmd.Parameters.AddWithValue("@VoucherNo", _ent.VoucherNo);
                 
                    _cmd.Parameters.AddWithValue("@DefaultStat", _defaultstatus);
                    _cmd.Parameters.AddWithValue("@JrnlJobId", _ent.JobId);
                    _cmd.Parameters.AddWithValue("@UsrCrt", _ent.UsrCrt);
                    _cmd.Parameters.AddWithValue("@DtmCrt", _ent.DtmCrt);
                    try { int _rowaffected = _cmd.ExecuteNonQuery(); }
                    catch (Exception exp) { throw new Exception(exp.Message); }
                    finally { if (_conn.State == ConnectionState.Open) { _conn.Close(); } }
                }
            }
            #endregion

            #region "Getting Payment History Header ID"
            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                using (_cmd = _conn.CreateCommand())
                {
                    try
                    {
                        _cmd.CommandText = "Select ID From PayHistHdr With (nolock) where [AgrmntID] = @AgrmntID and HistSeqNo = @HistSeqNo ";
                        _cmd.Parameters.AddWithValue("@AgrmntID", _ent.AgrmntID);
                        _cmd.Parameters.AddWithValue("@HistSeqNo", _ent.HistorySequenceNo);
                        _ent.PaymentHistorySequenceNoHeader = Convert.ToInt32(_cmd.ExecuteScalar());
                    }
                    catch (Exception exp) { throw new Exception(exp.Message); }
                    finally { if (_conn.State == ConnectionState.Open) { _conn.Close(); } }
                }
            }
            #endregion

            #region "Saving Payment History Detail"
            _spaymenthistorydetail.AppendLine(" Insert Into [PayHistDtl] ([PayHistHdrID], [CoaName], [InstSeqNo], [AssetSeqNo], [YearNum], [Description], ");
            _spaymenthistorydetail.AppendLine("                           [DebitAmt], [CreditAmt], [LCAmt], [LCDays], [UsrCrt], [DtmCrt]) ");
            _spaymenthistorydetail.AppendLine("                Values    (@PayHistHdrId, @CoaName, @InstSeqNo, @AssetSeqNo, @YearNum, @Description, ");
            _spaymenthistorydetail.AppendLine("                           @DebitAmount, @CreditAmount, @LCAmount, @LCDays, @UsrCrt, @DtmCrt)");
            

            _dttempphd = PrepareTempPaymentHistoryDetail();
            _dttempphd = FillDataTempPaymentHistoryDetail(_ent, _dttempphd);
            
            using (SqlConnection _conn = new SqlConnection("context connection=true"))
            {

                using (_cmd = _conn.CreateCommand())
                {
                    if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                    _cmd.CommandText = _spaymenthistorydetail.ToString();
                    _cmd.Connection = _conn;
                    try
                    {
                        if (_dttempphd.Rows.Count > 0)
                        {
                            foreach (DataRow _row in _dttempphd.Rows)
                            {
                                _cmd.Parameters.AddWithValue("@PayHistHdrId", _ent.PaymentHistorySequenceNoHeader);

                                _cmd.Parameters.AddWithValue("@CoaName", (string)_row["CoaName"]);
                                _cmd.Parameters.AddWithValue("@InstSeqNo", (int)_row["InstSeqNo"]);
                                _cmd.Parameters.AddWithValue("@AssetSeqNo", (int)_row["AssetSeqNo"]);
                                _cmd.Parameters.AddWithValue("@YearNum", (int)_row["YearNum"]);

                                _cmd.Parameters.AddWithValue("@Description", "-");
                                _cmd.Parameters.AddWithValue("@DebitAmount", (decimal)_row["DebitAmt"]);
                                _cmd.Parameters.AddWithValue("@CreditAmount", (decimal)_row["CreditAmt"]);
                                _cmd.Parameters.AddWithValue("@LCAmount", (decimal)_row["LCAmt"]);
                                _cmd.Parameters.AddWithValue("@LCDays", (int)_row["LCDays"]);

                                _cmd.Parameters.AddWithValue("@UsrCrt", _ent.UsrCrt);
                                _cmd.Parameters.AddWithValue("@DtmCrt", _ent.DtmCrt);
                                int _rowaffected = _cmd.ExecuteNonQuery();
                                _cmd.Parameters.Clear();
                            }
                        }
                    }
                    catch (Exception exp) { throw new Exception(exp.Message); }

                    finally { if (_conn.State == ConnectionState.Open) { _conn.Close(); } }
                }
            }


            #endregion
        }
        #endregion

    }

      
}

