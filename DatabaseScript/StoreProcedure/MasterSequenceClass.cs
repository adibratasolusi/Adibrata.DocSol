//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Server;

namespace Alpha.Database.Script.StoreProcedure
{
    public static class MasterSequenceClass
    {

      static  string _sequenceno;
      static string _formatsequence;
      static string _prefix;
      static string _suffix;
      static string _isoffice;
      static string _isyear;
      static string _ismonth;

      static string _month;
      static string _year;
      static string _office;
      static int _officeid; 
      static int _seqno;
      static string _strseqno;

      static int _lengthnumber;
      static char _fill = '0';
        

        public static string GetSequenceNo(int OfficeID, string SequenceID, string PostingDate)
        {
            SqlParameter oParam = new SqlParameter();
            StringBuilder sb = new StringBuilder();
            SqlDataReader _rdr;
            //CommonClass _common = new CommonClass();
        

            sb.Append("Select Seq_No, Length_Number, Prefix, Suffix, ");
            sb.Append("IsOffice, IsYear, IsMonth, FormatNumber from Ms_Sequence with (nolock) where OfficeID = @OfficeID and MsSequenceID = @SequenceID");
            
            _sequenceno = "";
            using (SqlConnection _con = new SqlConnection("context connection=true"))
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); };

                using (SqlCommand _cmd = _con.CreateCommand())
                {
                    _fill = '0';
                    try
                    {
                        _cmd.CommandText = sb.ToString();
                        _cmd.Connection = _con;
                        _cmd.Parameters.AddWithValue("@OfficeID", OfficeID);
                        _cmd.Parameters.AddWithValue("@SequenceID", SequenceID);
                        _rdr = _cmd.ExecuteReader();
                        while (_rdr.Read())
                        {
                            _seqno = (int)_rdr["Seq_No"];
                            _lengthnumber = (int)_rdr["Length_Number"];
                            _prefix = (string)_rdr["Prefix"];
                            _suffix = (string)_rdr["Suffix"];
                            _isoffice = (string)_rdr["IsOffice"];
                            _isyear = (string)_rdr["IsYear"];
                            _ismonth = (string)_rdr["IsMonth"];
                            _formatsequence = (string)_rdr["FormatNumber"];
                        }
                        _rdr.Close();
                    }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                    finally
                    {
                        if (_con.State == ConnectionState.Open) { _con.Close(); };
                    }

                    #region "Start Sequence No Generated"
                    _strseqno = _seqno.ToString().Trim().PadLeft(_lengthnumber, _fill);

                    if (_ismonth == "1")
                    {
                        _month = DateTime.Now.Month.ToString().Trim().PadLeft(2, _fill);
                    }
                    else { _month = ""; }

                    if (_isyear == "1")
                    {
                        _year = DateTime.Now.Year.ToString().Trim().PadLeft(4, _fill);
                    }
                    else { _year = ""; }

                    if (_isoffice == "1")
                    {
                        _office = CommonClass.GetOfficeCode(OfficeID);
                    }
                    else { _office = ""; }
                    _sequenceno = _formatsequence;
                    _sequenceno = _sequenceno.ToUpper().Replace("{MONTH}", _month);
                    _sequenceno = _sequenceno.ToUpper().Replace("{YEAR}", _year);
                    _sequenceno = _sequenceno.ToUpper().Replace("{OFFICEID}", _office);
                    _sequenceno = _sequenceno.ToUpper().Replace("{SEQNO}", _strseqno);
                    _sequenceno = _sequenceno.ToUpper().Replace("{PREFIX}", _prefix);
                    _sequenceno = _sequenceno.ToUpper().Replace("{SUFFIX}", _suffix);
                    #endregion

                    sb.Clear();
                    sb.Append("Update Ms_Sequence set Seq_No = Seq_No + 1 where OfficeID = @OfficeID and MsSequenceID = @SequenceID");

                    _cmd.Connection = _con;

                    _cmd.CommandText = sb.ToString();
                    _cmd.Parameters.AddWithValue("@OfficeID", OfficeID);
                    _cmd.Parameters.AddWithValue("@SequenceID", SequenceID);

                    if (_con.State == ConnectionState.Closed) { _con.Open(); };

                    try { int rowaffected = _cmd.ExecuteNonQuery(); }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                    finally
                    {
                        if (_con.State == ConnectionState.Open) { _con.Close(); };
                    }
                }
            }
            sb.Clear();
            return _sequenceno;
        }


        public static string GetJournalNo( int OfficeID, int JournalTransactionID, string PostingDate)
        {
            SqlParameter oParam = new SqlParameter();
            StringBuilder sb = new StringBuilder();
            SqlDataReader _rdr;
            //CommonClass _common = new CommonClass();
         
            sb.Append("Select Seq_No, Length_Number, Prefix, Suffix, ");
            sb.Append("IsOffice, IsYear, IsMonth, FormatNumber from Ms_Journal_Seq with (nolock) where OfficeID = @OfficeID and JrnlTrxID = @SequenceID");
        
            _sequenceno = "";

            using (SqlConnection _con = new SqlConnection("context connection=true"))
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); };
                using (SqlCommand _cmd = _con.CreateCommand())
                {
                    try
                    {
                        _cmd.CommandText = sb.ToString();
                        _cmd.Connection = _con;
                        _cmd.Parameters.AddWithValue("@OfficeID", OfficeID);
                        _cmd.Parameters.AddWithValue("@SequenceID", JournalTransactionID);
                        _rdr = _cmd.ExecuteReader();
                        while (_rdr.Read())
                        {
                            _seqno = (int)_rdr["Seq_No"];
                            _lengthnumber = (int)_rdr["Length_Number"];
                            _prefix = (string)_rdr["Prefix"];
                            _suffix = (string)_rdr["Suffix"];
                            _isoffice = (string)_rdr["IsOffice"];
                            _isyear = (string)_rdr["IsYear"];
                            _ismonth = (string)_rdr["IsMonth"];
                            _formatsequence = (string)_rdr["FormatNumber"];
                        }
                        _rdr.Close();
                    }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                    finally
                    {
                        if (_con.State == ConnectionState.Open) { _con.Close(); };
                    }
       
                    _strseqno = _seqno.ToString().Trim().PadLeft(_lengthnumber, _fill);

                    if (_ismonth == "1")
                    {
                        _month = Convert.ToDateTime(PostingDate).Month.ToString().Trim().PadLeft(2, _fill);
                    }
                    else { _month = ""; }

                    if (_isyear == "1")
                    {
                        _year = Convert.ToDateTime(PostingDate).Year.ToString().Trim().PadLeft(4, _fill);
                    }
                    else { _year = ""; }

                    if (_isoffice == "1")
                    {
                        _office = CommonClass.GetOfficeCode(OfficeID);
                    }
                    else { _office = ""; }
                    _sequenceno = _formatsequence;
                    _sequenceno = _sequenceno.ToUpper().Replace("{MONTH}", _month);
                    _sequenceno = _sequenceno.ToUpper().Replace("{YEAR}", _year);
                    _sequenceno = _sequenceno.ToUpper().Replace("{OFFICEID}", _office);
                    _sequenceno = _sequenceno.ToUpper().Replace("{SEQNO}", _strseqno);
                    _sequenceno = _sequenceno.ToUpper().Replace("{PREFIX}", _prefix);
                    _sequenceno = _sequenceno.ToUpper().Replace("{SUFFIX}", _suffix);

                    sb.Clear();
                    sb.Append("Update Ms_Journal_Seq set Seq_No = Seq_No + 1 where OfficeID = @OfficeID and JrnlTrxID = @SequenceID");
                    _cmd.Connection = _con;

                    _cmd.CommandText = sb.ToString();

                    if (_con.State == ConnectionState.Closed) { _con.Open(); };

                    try { int rowaffected = _cmd.ExecuteNonQuery(); }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                    finally
                    {
                        if (_con.State == ConnectionState.Open) { _con.Close(); };
                    }

                }
            }
            //sb.Clear();
            return _sequenceno;
        }

        public static string GetVoucherNo(int BankAccountID, string PostingDate)
        {
            SqlParameter oParam = new SqlParameter();
            StringBuilder sb = new StringBuilder();
            SqlDataReader _rdr;
            //CommonClass _common = new CommonClass();
          
            sb.Append("Select OfficeID, Seq_No, Length_Number, Prefix, Suffix, ");
            sb.Append("IsOffice, IsYear, IsMonth, FormatNumber from Ms_BankAccount with (nolock) where ID = @SequenceID");
            
            _sequenceno = "";
            using (SqlConnection _con = new SqlConnection("context connection=true"))
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); };

                using (SqlCommand _cmd = _con.CreateCommand())
                {
                    _fill = '0';
                    _cmd.CommandText = sb.ToString();
                    _cmd.Parameters.AddWithValue("@SequenceID", BankAccountID);
                    _cmd.ExecuteNonQuery();
                    _rdr = _cmd.ExecuteReader();

                    try
                    {
                        while (_rdr.Read())
                        {
                            _seqno = (int)_rdr["Seq_No"];
                            _lengthnumber = (int)_rdr["Length_Number"];
                            _prefix = (string)_rdr["Prefix"];
                            _suffix = (string)_rdr["Suffix"];
                            _isoffice = (string)_rdr["IsOffice"];
                            _isyear = (string)_rdr["IsYear"];
                            _ismonth = (string)_rdr["IsMonth"];
                            _formatsequence = (string)_rdr["FormatNumber"];
                            _officeid = (int)_rdr["OfficeID"];
                        }
                    }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                    finally
                    {
                        if (_con.State == ConnectionState.Open) { _con.Close(); };
                    }
                    _rdr.Close();

                    _strseqno = _seqno.ToString().Trim().PadLeft(_lengthnumber, _fill);

                    if (_ismonth == "1")
                    {
                        _month = Convert.ToDateTime(PostingDate).Month.ToString().Trim().PadLeft(2, _fill);
                    }
                    else { _month = ""; }

                    if (_isyear == "1")
                    {
                        _year = Convert.ToDateTime(PostingDate).Year.ToString().Trim().PadLeft(4, _fill);
                    }
                    else { _year = ""; }

                    if (_isoffice == "1")
                    {
                        _office = CommonClass.GetOfficeCode(_officeid);
                    }
                    else { _office = ""; }
                    _sequenceno = _formatsequence;
                    _sequenceno = _sequenceno.ToUpper().Replace("{MONTH}", _month);
                    _sequenceno = _sequenceno.ToUpper().Replace("{YEAR}", _year);
                    _sequenceno = _sequenceno.ToUpper().Replace("{OFFICEID}", _office);
                    _sequenceno = _sequenceno.ToUpper().Replace("{SEQNO}", _strseqno);
                    _sequenceno = _sequenceno.ToUpper().Replace("{PREFIX}", _prefix.Trim());
                    _sequenceno = _sequenceno.ToUpper().Replace("{SUFFIX}", _suffix.Trim());

                    sb.Clear();
                    sb.Append("Update Ms_BankAccount set Seq_No = Seq_No + 1 where ID = @SequenceID");
                    _cmd.CommandText = sb.ToString();

                    if (_con.State == ConnectionState.Closed) { _con.Open(); };
                    _cmd.Connection = _con;
                    try { int rowaffected = _cmd.ExecuteNonQuery(); }
                    catch (Exception exp) { throw new Exception(exp.Message); }
                    finally { if (_con.State == ConnectionState.Open) { _con.Close(); } }

                }
            }
            sb.Clear();
            return _sequenceno;
        }

        public static int GetMaxHistorySequenceNo(long _agrmntid)
        {
            int _histseqno;
            using (SqlConnection _con = new SqlConnection("context connection=true"))
            {

                if (_con.State == ConnectionState.Closed) { _con.Open(); };

                using (SqlCommand _cmd = _con.CreateCommand())
                {
                    try
                    {
                        _cmd.CommandText = "Select Isnull(Max(HistSeqNo),0) + 1 as HistorySequenceNo From PayHistHdr with (nolock) where AgrmntID = @AgrmntID";

                        _cmd.Connection = _con;
                        _cmd.Parameters.AddWithValue("@AgrmntID", _agrmntid);
                        _histseqno = Convert.ToInt32(_cmd.ExecuteScalar());
                    }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                    finally
                    {
                        if (_con.State == ConnectionState.Open) { _con.Close(); };
                    }
                }
            }
            return _histseqno;
        }

  
        public static  string GetAgreementNo(string OfficeID, string ProductID)
        {
            SqlParameter oParam = new SqlParameter();
            StringBuilder sb = new StringBuilder();
            SqlDataReader _rdr;
            
            sb.Append("Select Seq_No, Length_Number, Prefix, Suffix, ");
            sb.Append("IsOffice, IsYear, IsMonth, FormatNumber from Ms_BankAccount with (nolock) where OfficeID = @OfficeID and ProductID = @SequenceID");
            _sequenceno = "";
            using (SqlConnection _con = new SqlConnection("context connection=true"))
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); };

                using (SqlCommand _cmd = _con.CreateCommand())
                {

                    _fill = '0';
                    try
                    {
                        _cmd.CommandText = sb.ToString();
                        _cmd.Connection = _con;
                        _cmd.Parameters.AddWithValue("@OfficeID", OfficeID);
                        _cmd.Parameters.AddWithValue("@SequenceID", ProductID);
                        _rdr = _cmd.ExecuteReader();

                        try
                        {
                            while (_rdr.Read())
                            {
                                _seqno = (int)_rdr["Seq_No"];
                                _lengthnumber = (int)_rdr["Length_Number"];
                                _prefix = (string)_rdr["Prefix"];
                                _suffix = (string)_rdr["Suffix"];
                                _isoffice = (string)_rdr["IsOffice"];
                                _isyear = (string)_rdr["IsYear"];
                                _ismonth = (string)_rdr["IsMonth"];
                                _formatsequence = (string)_rdr["FormatNumber"];
                            }
                        }
                        catch (Exception exp) { throw new Exception(exp.Message); }
                        finally{_rdr.Close();      if (_con.State == ConnectionState.Open) { _con.Close(); };}
                        

                        _strseqno = _seqno.ToString().Trim().PadLeft(_lengthnumber, _fill);

                        if (_ismonth == "1")
                        {
                            _month = DateTime.Now.Month.ToString().Trim().PadLeft(2, _fill);
                        }
                        else { _month = ""; }

                        if (_isyear == "1")
                        {
                            _year = DateTime.Now.Year.ToString().Trim().PadLeft(4, _fill);
                        }
                        else { _year = ""; }

                        if (_isoffice == "1")
                        {
                            _office = OfficeID.Trim();
                        }
                        else { _office = ""; }
                        _sequenceno = _formatsequence;
                        _sequenceno = _sequenceno.ToUpper().Replace("{MONTH}", _month);
                        _sequenceno = _sequenceno.ToUpper().Replace("{YEAR}", _year);
                        _sequenceno = _sequenceno.ToUpper().Replace("{OFFICEID}", _office);
                        _sequenceno = _sequenceno.ToUpper().Replace("{SEQNO}", _strseqno);
                        _sequenceno = _sequenceno.ToUpper().Replace("{PREFIX}", _prefix);
                        _sequenceno = _sequenceno.ToUpper().Replace("{SUFFIX}", _suffix);

                        sb.Clear();
                        sb.Append("Update Ms_Journal_Seq set Seq_No = Seq_No + 1 where OfficeID = @OfficeID and JournalID = @SequenceID");
                        _cmd.CommandText = sb.ToString();
                        int rowaffected = _cmd.ExecuteNonQuery();
                    }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                    finally
                    {
                        if (_con.State == ConnectionState.Open) { _con.Close(); };
                    }
                }
            }
            sb.Clear();

            return _sequenceno;
        }



    }
}