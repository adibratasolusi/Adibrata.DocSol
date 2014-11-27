//------------------------------------------------------------------------------
// <copyright file="CSSqlStoredProcedure.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Adibrata.Database.Script.StoreProcedure;
using Microsoft.SqlServer.Server;


public class DailyAging
{

    private DailyAgingEntities DailyAgingReport(DailyAgingEntities _ent)
    {
        SqlConnection _conn = new SqlConnection("context connection=true");
        SqlCommand _cmd = new SqlCommand();
        StringBuilder sb_Bucket = new StringBuilder();
        StringBuilder sb_PercentDP = new StringBuilder();
        StringBuilder sb_PercentEffective = new StringBuilder();
        StringBuilder sb_AmountFinance = new StringBuilder();
        StringBuilder sb_AmountInstallment = new StringBuilder();

        SqlDataReader _rdr;


        #region "query AgingBucket"
        sb_Bucket.Clear();
        sb_Bucket.AppendLine("Select  ReportID, BucketName, Bucket1_from, Bucket1_to, Bucket1_text, Bucket2_from, Bucket2_to, Bucket2_text, Bucket3_from, Bucket3_to, Bucket3_text, ");
        sb_Bucket.AppendLine(" Bucket4_from, Bucket4_to, Bucket4_text, Bucket5_from, Bucket5_to, Bucket5_text, Bucket6_from, Bucket6_to, Bucket6_text, Bucket7_from, Bucket7_to, ");
        sb_Bucket.AppendLine(" Bucket7_text, Bucket8_from, Bucket8_to, Bucket8_text, Bucket9_from, Bucket9_to, Bucket9_text, Bucket10_from, Bucket10_to, Bucket10_text ");
        sb_Bucket.AppendLine("FROM            dbo.AgingBucket with (nolock) where ReportID = 1 ");
        #endregion

        #region "Query Percent DP"
        sb_PercentDP.Clear();
        sb_PercentDP.AppendLine("SELECT        ReportID, ReportName, PercentDP1From, PercentDP1To, PercentDP1Text, PercentDP2From, PercentDP2To, PercentDP2Text, PercentDP3From, ");
        sb_PercentDP.AppendLine("                 PercentDP3To, PercentDP3Text, PercentDP4From, PercentDP4To, PercentDP4Text, PercentDP5From, PercentDP5To, PercentDP5Text, PercentDP6From, ");
        sb_PercentDP.AppendLine("                 PercentDP6To, PercentDP6Text, PercentDP7From, PercentDP7To, PercentDP7Text, PercentDP8From, PercentDP8To, PercentDP8Text, PercentDP9From, ");
        sb_PercentDP.AppendLine("                 PercentDP9To, PercentDP9Text, PercentDP10From, PercentDP10To, PercentDP10Text  ");
        sb_PercentDP.AppendLine("FROM            dbo.ReportPercentDP with (nolock) where ReportID = 1 ");
        #endregion

        #region"Query PercentEffective"
        sb_PercentEffective.Clear();
        sb_PercentEffective.AppendLine(" SELECT        ReportID, ReportName, PercentEffective1From, PercentEffective1To, PercentEffective1Text, PercentEffective2From, PercentEffective2To, ");
        sb_PercentEffective.AppendLine("                  PercentEffective2Text, PercentEffective3From, PercentEffective3To, PercentEffective3Text, PercentEffective4From, PercentEffective4To, ");
        sb_PercentEffective.AppendLine("                  PercentEffective4Text, PercentEffective5From, PercentEffective5To, PercentEffective5Text, PercentEffective6From, PercentEffective6To,");
        sb_PercentEffective.AppendLine("                  PercentEffective6Text, PercentEffective7From, PercentEffective7To, PercentEffective7Text, PercentEffective8From, PercentEffective8To,");
        sb_PercentEffective.AppendLine("                  PercentEffective8Text, PercentEffective9From, PercentEffective9To, PercentEffective9Text, PercentEffective10From, PercentEffective10To, ");
        sb_PercentEffective.AppendLine("                  PercentEffective10Text  ");
        sb_PercentEffective.AppendLine("FROM            dbo.ReportPercentEffective with (nolock) where ReportID = 1");
        #endregion

        #region "Query Report Amount Finance"
        sb_AmountFinance.Clear();
        sb_AmountFinance.AppendLine(" SELECT        ReportID, ReportName, AmountFinance1From, AmountFinance1To, AmountFinance1Text, AmountFinance2From, AmountFinance2To, AmountFinance2Text, ");
        sb_AmountFinance.AppendLine("                  AmountFinance3From, AmountFinance3To, AmountFinance3Text, AmountFinance4From, AmountFinance4To, AmountFinance4Text, AmountFinance5From, ");
        sb_AmountFinance.AppendLine("                  AmountFinance5To, AmountFinance5Text, AmountFinance6From, AmountFinance6To, AmountFinance6Text, AmountFinance7From, AmountFinance7To, ");
        sb_AmountFinance.AppendLine("                  AmountFinance7Text, AmountFinance8From, AmountFinance8To, AmountFinance8Text, AmountFinance9From, AmountFinance9To, AmountFinance9Text, ");
        sb_AmountFinance.AppendLine("                  AmountFinance10From, AmountFinance10To, AmountFinance10Text ");
        sb_AmountFinance.AppendLine(" FROM            dbo.ReportAmountFinance  with (nolock) where ReportID = 1");
        #endregion

        #region "Query Report Installment"
        sb_AmountInstallment.Clear();
        sb_AmountInstallment.AppendLine(" SELECT        ReportID, ReportName, AmountInstallment1From, AmountInstallment1To, AmountInstallment1Text, AmountInstallment2From, AmountInstallment2To, ");
        sb_AmountInstallment.AppendLine("                AmountInstallment2Text, AmountInstallment3From, AmountInstallment3To, AmountInstallment3Text, AmountInstallment4From, AmountInstallment4To, ");
        sb_AmountInstallment.AppendLine("                AmountInstallment4Text, AmountInstallment5From, AmountInstallment5To, AmountInstallment5Text, AmountInstallment6From, AmountInstallment6To, ");
        sb_AmountInstallment.AppendLine("               AmountInstallment6Text, AmountInstallment7From, AmountInstallment7To, AmountInstallment7Text, AmountInstallment8From, AmountInstallment8To, ");
        sb_AmountInstallment.AppendLine("                AmountInstallment8Text, AmountInstallment9From, AmountInstallment9To, AmountInstallment9Text, AmountInstallment10From, AmountInstallment10To, ");
        sb_AmountInstallment.AppendLine("                AmountInstallment10Text ");
        sb_AmountInstallment.AppendLine("FROM            dbo.ReportInstallmentAmount with (nolock) where ReportID = 1");
        #endregion

        using (_conn)
        {
            if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
            using (_cmd = _conn.CreateCommand())
            {
                #region "================================Report Aging Bucket==================================="

                _cmd.CommandText = sb_Bucket.ToString();
                _rdr = _cmd.ExecuteReader();
                while (_rdr.Read())
                {
                    _ent.BucketTo1 = (int)_rdr["Bucket1_To"];
                    _ent.BucketTo2 = (int)_rdr["Bucket2_To"];
                    _ent.BucketTo3 = (int)_rdr["Bucket3_To"];
                    _ent.BucketTo4 = (int)_rdr["Bucket4_To"];
                    _ent.BucketTo5 = (int)_rdr["Bucket5_To"];
                    _ent.BucketTo6 = (int)_rdr["Bucket6_To"];
                    _ent.BucketTo7 = (int)_rdr["Bucket7_To"];
                    _ent.BucketTo8 = (int)_rdr["Bucket8_To"];
                    _ent.BucketTo9 = (int)_rdr["Bucket9_To"];
                    _ent.BucketTo10 = (int)_rdr["Bucket10_To"];

                    _ent.BucketFrom1 = (int)_rdr["Bucket1_From"];
                    _ent.BucketFrom2 = (int)_rdr["Bucket2_From"];
                    _ent.BucketFrom3 = (int)_rdr["Bucket3_From"];
                    _ent.BucketFrom4 = (int)_rdr["Bucket4_From"];
                    _ent.BucketFrom5 = (int)_rdr["Bucket5_From"];
                    _ent.BucketFrom6 = (int)_rdr["Bucket6_From"];
                    _ent.BucketFrom7 = (int)_rdr["Bucket7_From"];
                    _ent.BucketFrom8 = (int)_rdr["Bucket8_From"];
                    _ent.BucketFrom9 = (int)_rdr["Bucket9_From"];
                    _ent.BucketFrom10 = (int)_rdr["Bucket10_From"];

                    _ent.BucketText1 = (string)_rdr["Bucket1_Text"];
                    _ent.BucketText2 = (string)_rdr["Bucket2_Text"];
                    _ent.BucketText3 = (string)_rdr["Bucket3_Text"];
                    _ent.BucketText4 = (string)_rdr["Bucket4_Text"];
                    _ent.BucketText5 = (string)_rdr["Bucket5_Text"];
                    _ent.BucketText6 = (string)_rdr["Bucket6_Text"];
                    _ent.BucketText7 = (string)_rdr["Bucket7_Text"];
                    _ent.BucketText8 = (string)_rdr["Bucket8_Text"];
                    _ent.BucketText9 = (string)_rdr["Bucket9_Text"];
                    _ent.BucketText10 = (string)_rdr["Bucket10_Text"];
                }
                _rdr.Close();
                #endregion

                #region "==============================Report Percent DP ==============================="

                _cmd.CommandText = sb_PercentDP.ToString();
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                _cmd.Connection = _conn;

                _rdr = _cmd.ExecuteReader();
                while (_rdr.Read())
                {
                    _ent.PercentDPFrom1 = (decimal)_rdr["PercentDP1From"];
                    _ent.PercentDPFrom2 = (decimal)_rdr["PercentDP2From"];
                    _ent.PercentDPFrom3 = (decimal)_rdr["PercentDP3From"];
                    _ent.PercentDPFrom4 = (decimal)_rdr["PercentDP4From"];
                    _ent.PercentDPFrom5 = (decimal)_rdr["PercentDP5From"];
                    _ent.PercentDPFrom6 = (decimal)_rdr["PercentDP6From"];
                    _ent.PercentDPFrom7 = (decimal)_rdr["PercentDP7From"];
                    _ent.PercentDPFrom8 = (decimal)_rdr["PercentDP8From"];
                    _ent.PercentDPFrom9 = (decimal)_rdr["PercentDP9From"];
                    _ent.PercentDPFrom10 = (decimal)_rdr["PercentDP10From"];

                    _ent.PercentDPTo1 = (decimal)_rdr["PercentDP1To"];
                    _ent.PercentDPTo2 = (decimal)_rdr["PercentDP2To"];
                    _ent.PercentDPTo3 = (decimal)_rdr["PercentDP3To"];
                    _ent.PercentDPTo4 = (decimal)_rdr["PercentDP4To"];
                    _ent.PercentDPTo5 = (decimal)_rdr["PercentDP5To"];
                    _ent.PercentDPTo6 = (decimal)_rdr["PercentDP6To"];
                    _ent.PercentDPTo7 = (decimal)_rdr["PercentDP7To"];
                    _ent.PercentDPTo8 = (decimal)_rdr["PercentDP8To"];
                    _ent.PercentDPTo9 = (decimal)_rdr["PercentDP9To"];
                    _ent.PercentDPTo10 = (decimal)_rdr["PercentDP10To"];


                    _ent.PercentDPText1 = (string)_rdr["PercentDP1Text"];
                    _ent.PercentDPText2 = (string)_rdr["PercentDP2Text"];
                    _ent.PercentDPText3 = (string)_rdr["PercentDP3Text"];
                    _ent.PercentDPText4 = (string)_rdr["PercentDP4Text"];
                    _ent.PercentDPText5 = (string)_rdr["PercentDP5Text"];
                    _ent.PercentDPText6 = (string)_rdr["PercentDP6Text"];
                    _ent.PercentDPText7 = (string)_rdr["PercentDP7Text"];
                    _ent.PercentDPText8 = (string)_rdr["PercentDP8Text"];
                    _ent.PercentDPText9 = (string)_rdr["PercentDP9Text"];
                    _ent.PercentDPText10 = (string)_rdr["PercentDP10Text"];
                }
                _rdr.Close();
                #endregion

                #region "==============================Report Percent Effective ==============================="

                _cmd.CommandText = sb_PercentEffective.ToString();
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                _cmd.Connection = _conn;
                _rdr = _cmd.ExecuteReader();
                while (_rdr.Read())
                {
                    _ent.PercentEffectiveFrom1 = (decimal)_rdr["PercentEffective1From"];
                    _ent.PercentEffectiveFrom2 = (decimal)_rdr["PercentEffective2From"];
                    _ent.PercentEffectiveFrom3 = (decimal)_rdr["PercentEffective3From"];
                    _ent.PercentEffectiveFrom4 = (decimal)_rdr["PercentEffective4From"];
                    _ent.PercentEffectiveFrom5 = (decimal)_rdr["PercentEffective5From"];
                    _ent.PercentEffectiveFrom6 = (decimal)_rdr["PercentEffective6From"];
                    _ent.PercentEffectiveFrom7 = (decimal)_rdr["PercentEffective7From"];
                    _ent.PercentEffectiveFrom8 = (decimal)_rdr["PercentEffective8From"];
                    _ent.PercentEffectiveFrom9 = (decimal)_rdr["PercentEffective9From"];
                    _ent.PercentEffectiveFrom10 = (decimal)_rdr["PercentEffective10From"];

                    _ent.PercentEffectiveTo1 = (decimal)_rdr["PercentEffective1To"];
                    _ent.PercentEffectiveTo2 = (decimal)_rdr["PercentEffective2To"];
                    _ent.PercentEffectiveTo3 = (decimal)_rdr["PercentEffective3To"];
                    _ent.PercentEffectiveTo4 = (decimal)_rdr["PercentEffective4To"];
                    _ent.PercentEffectiveTo5 = (decimal)_rdr["PercentEffective5To"];
                    _ent.PercentEffectiveTo6 = (decimal)_rdr["PercentEffective6To"];
                    _ent.PercentEffectiveTo7 = (decimal)_rdr["PercentEffective7To"];
                    _ent.PercentEffectiveTo8 = (decimal)_rdr["PercentEffective8To"];
                    _ent.PercentEffectiveTo9 = (decimal)_rdr["PercentEffective9To"];
                    _ent.PercentEffectiveTo10 = (decimal)_rdr["PercentEffective10To"];


                    _ent.PercentEffectiveText1 = (string)_rdr["PercentEffective1Text"];
                    _ent.PercentEffectiveText2 = (string)_rdr["PercentEffective2Text"];
                    _ent.PercentEffectiveText3 = (string)_rdr["PercentEffective3Text"];
                    _ent.PercentEffectiveText4 = (string)_rdr["PercentEffective4Text"];
                    _ent.PercentEffectiveText5 = (string)_rdr["PercentEffective5Text"];
                    _ent.PercentEffectiveText6 = (string)_rdr["PercentEffective6Text"];
                    _ent.PercentEffectiveText7 = (string)_rdr["PercentEffective7Text"];
                    _ent.PercentEffectiveText8 = (string)_rdr["PercentEffective8Text"];
                    _ent.PercentEffectiveText9 = (string)_rdr["PercentEffective9Text"];
                    _ent.PercentEffectiveText10 = (string)_rdr["PercentEffective10Text"];
                }
                _rdr.Close();
                #endregion

                #region "==============================Report Amount Finance ==============================="
                _cmd.CommandText = sb_AmountFinance.ToString();
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                _cmd.Connection = _conn;
                _rdr = _cmd.ExecuteReader();
                while (_rdr.Read())
                {
                    _ent.AmountFinanceFrom1 = (decimal)_rdr["AmountFinance1From"];
                    _ent.AmountFinanceFrom2 = (decimal)_rdr["AmountFinance2From"];
                    _ent.AmountFinanceFrom3 = (decimal)_rdr["AmountFinance3From"];
                    _ent.AmountFinanceFrom4 = (decimal)_rdr["AmountFinance4From"];
                    _ent.AmountFinanceFrom5 = (decimal)_rdr["AmountFinance5From"];
                    _ent.AmountFinanceFrom6 = (decimal)_rdr["AmountFinance6From"];
                    _ent.AmountFinanceFrom7 = (decimal)_rdr["AmountFinance7From"];
                    _ent.AmountFinanceFrom8 = (decimal)_rdr["AmountFinance8From"];
                    _ent.AmountFinanceFrom9 = (decimal)_rdr["AmountFinance9From"];
                    _ent.AmountFinanceFrom10 = (decimal)_rdr["AmountFinance10From"];


                    _ent.AmountFinanceTo1 = (decimal)_rdr["AmountFinance1To"];
                    _ent.AmountFinanceTo2 = (decimal)_rdr["AmountFinance2To"];
                    _ent.AmountFinanceTo3 = (decimal)_rdr["AmountFinance3To"];
                    _ent.AmountFinanceTo4 = (decimal)_rdr["AmountFinance4To"];
                    _ent.AmountFinanceTo5 = (decimal)_rdr["AmountFinance5To"];
                    _ent.AmountFinanceTo6 = (decimal)_rdr["AmountFinance6To"];
                    _ent.AmountFinanceTo7 = (decimal)_rdr["AmountFinance7To"];
                    _ent.AmountFinanceTo8 = (decimal)_rdr["AmountFinance8To"];
                    _ent.AmountFinanceTo9 = (decimal)_rdr["AmountFinance9To"];
                    _ent.AmountFinanceTo10 = (decimal)_rdr["AmountFinance10To"];

                    _ent.AmountFinanceText1 = (string)_rdr["AmountFinance1Text"];
                    _ent.AmountFinanceText2 = (string)_rdr["AmountFinance2Text"];
                    _ent.AmountFinanceText3 = (string)_rdr["AmountFinance3Text"];
                    _ent.AmountFinanceText4 = (string)_rdr["AmountFinance4Text"];
                    _ent.AmountFinanceText5 = (string)_rdr["AmountFinance5Text"];
                    _ent.AmountFinanceText6 = (string)_rdr["AmountFinance6Text"];
                    _ent.AmountFinanceText7 = (string)_rdr["AmountFinance7Text"];
                    _ent.AmountFinanceText8 = (string)_rdr["AmountFinance8Text"];
                    _ent.AmountFinanceText9 = (string)_rdr["AmountFinance9Text"];
                    _ent.AmountFinanceText10 = (string)_rdr["AmountFinance10Text"];
                }
                _rdr.Close();
                #endregion


                #region "==============================Report Amount Installment ==============================="
                _cmd.CommandText = sb_AmountInstallment.ToString();
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                _cmd.Connection = _conn;
                _rdr = _cmd.ExecuteReader();
                while (_rdr.Read())
                {
                    _ent.AmountInstallmentFrom1 = (decimal)_rdr["AmountInstallment1From"];
                    _ent.AmountInstallmentFrom2 = (decimal)_rdr["AmountInstallment2From"];
                    _ent.AmountInstallmentFrom3 = (decimal)_rdr["AmountInstallment3From"];
                    _ent.AmountInstallmentFrom4 = (decimal)_rdr["AmountInstallment4From"];
                    _ent.AmountInstallmentFrom5 = (decimal)_rdr["AmountInstallment5From"];
                    _ent.AmountInstallmentFrom6 = (decimal)_rdr["AmountInstallment6From"];
                    _ent.AmountInstallmentFrom7 = (decimal)_rdr["AmountInstallment7From"];
                    _ent.AmountInstallmentFrom8 = (decimal)_rdr["AmountInstallment8From"];
                    _ent.AmountInstallmentFrom9 = (decimal)_rdr["AmountInstallment9From"];
                    _ent.AmountInstallmentFrom10 = (decimal)_rdr["AmountInstallment10From"];


                    _ent.AmountInstallmentTo1 = (decimal)_rdr["AmountInstallment1To"];
                    _ent.AmountInstallmentTo2 = (decimal)_rdr["AmountInstallment2To"];
                    _ent.AmountInstallmentTo3 = (decimal)_rdr["AmountInstallment3To"];
                    _ent.AmountInstallmentTo4 = (decimal)_rdr["AmountInstallment4To"];
                    _ent.AmountInstallmentTo5 = (decimal)_rdr["AmountInstallment5To"];
                    _ent.AmountInstallmentTo6 = (decimal)_rdr["AmountInstallment6To"];
                    _ent.AmountInstallmentTo7 = (decimal)_rdr["AmountInstallment7To"];
                    _ent.AmountInstallmentTo8 = (decimal)_rdr["AmountInstallment8To"];
                    _ent.AmountInstallmentTo9 = (decimal)_rdr["AmountInstallment9To"];
                    _ent.AmountInstallmentTo10 = (decimal)_rdr["AmountInstallment10To"];

                    _ent.AmountInstallmentText1 = (string)_rdr["AmountInstallment1Text"];
                    _ent.AmountInstallmentText2 = (string)_rdr["AmountInstallment2Text"];
                    _ent.AmountInstallmentText3 = (string)_rdr["AmountInstallment3Text"];
                    _ent.AmountInstallmentText4 = (string)_rdr["AmountInstallment4Text"];
                    _ent.AmountInstallmentText5 = (string)_rdr["AmountInstallment5Text"];
                    _ent.AmountInstallmentText6 = (string)_rdr["AmountInstallment6Text"];
                    _ent.AmountInstallmentText7 = (string)_rdr["AmountInstallment7Text"];
                    _ent.AmountInstallmentText8 = (string)_rdr["AmountInstallment8Text"];
                    _ent.AmountInstallmentText9 = (string)_rdr["AmountInstallment9Text"];
                    _ent.AmountInstallmentText10 = (string)_rdr["AmountInstallment10Text"];
                }
                _rdr.Close();
                #endregion
            }
        }
        return _ent;
    }
    private string GetPercentEffectiveText(DailyAgingEntities _ent)
    {
        string _percenteffectivetext;
        _percenteffectivetext = "";
        if (_ent.EffectiveRate >= _ent.PercentEffectiveFrom1 && _ent.EffectiveRate < _ent.PercentEffectiveTo1) { _percenteffectivetext = _ent.PercentEffectiveText1; }
        else
            if (_ent.EffectiveRate >= _ent.PercentEffectiveFrom2 && _ent.EffectiveRate < _ent.PercentEffectiveTo2) { _percenteffectivetext = _ent.PercentEffectiveText2; }
            else
                if (_ent.EffectiveRate >= _ent.PercentEffectiveFrom3 && _ent.EffectiveRate < _ent.PercentEffectiveTo3) { _percenteffectivetext = _ent.PercentEffectiveText3; }
                else
                    if (_ent.EffectiveRate >= _ent.PercentEffectiveFrom4 && _ent.EffectiveRate < _ent.PercentEffectiveTo4) { _percenteffectivetext = _ent.PercentEffectiveText4; }
                    else
                        if (_ent.EffectiveRate >= _ent.PercentEffectiveFrom5 && _ent.EffectiveRate < _ent.PercentEffectiveTo5) { _percenteffectivetext = _ent.PercentEffectiveText5; }
                        else
                            if (_ent.EffectiveRate >= _ent.PercentEffectiveFrom6 && _ent.EffectiveRate < _ent.PercentEffectiveTo6) { _percenteffectivetext = _ent.PercentEffectiveText6; }
                            else
                                if (_ent.EffectiveRate >= _ent.PercentEffectiveFrom7 && _ent.EffectiveRate < _ent.PercentEffectiveTo7) { _percenteffectivetext = _ent.PercentEffectiveText7; }
                                else
                                    if (_ent.EffectiveRate >= _ent.PercentEffectiveFrom8 && _ent.EffectiveRate < _ent.PercentEffectiveTo8) { _percenteffectivetext = _ent.PercentEffectiveText8; }
                                    else
                                        if (_ent.EffectiveRate >= _ent.PercentEffectiveFrom9 && _ent.EffectiveRate < _ent.PercentEffectiveTo9) { _percenteffectivetext = _ent.PercentEffectiveText9; }
                                        else
                                            if (_ent.EffectiveRate >= _ent.PercentEffectiveFrom10 && _ent.EffectiveRate < _ent.PercentEffectiveTo10) { _percenteffectivetext = _ent.PercentEffectiveText10; }
        return _percenteffectivetext;
    }
    private string GetPercentDPText(DailyAgingEntities _ent)
    {
        string _percentDPtext;
        _percentDPtext = "";
        if (_ent.PercentDP >= _ent.PercentDPFrom1 && _ent.PercentDP < _ent.PercentDPTo1) { _percentDPtext = _ent.PercentDPText1; }
        else
            if (_ent.PercentDP >= _ent.PercentDPFrom2 && _ent.PercentDP < _ent.PercentDPTo2) { _percentDPtext = _ent.PercentDPText2; }
            else
                if (_ent.PercentDP >= _ent.PercentDPFrom3 && _ent.PercentDP < _ent.PercentDPTo3) { _percentDPtext = _ent.PercentDPText3; }
                else
                    if (_ent.PercentDP >= _ent.PercentDPFrom4 && _ent.PercentDP < _ent.PercentDPTo4) { _percentDPtext = _ent.PercentDPText4; }
                    else
                        if (_ent.PercentDP >= _ent.PercentDPFrom5 && _ent.PercentDP < _ent.PercentDPTo5) { _percentDPtext = _ent.PercentDPText5; }
                        else
                            if (_ent.PercentDP >= _ent.PercentDPFrom6 && _ent.PercentDP < _ent.PercentDPTo6) { _percentDPtext = _ent.PercentDPText6; }
                            else
                                if (_ent.PercentDP >= _ent.PercentDPFrom7 && _ent.PercentDP < _ent.PercentDPTo7) { _percentDPtext = _ent.PercentDPText7; }
                                else
                                    if (_ent.PercentDP >= _ent.PercentDPFrom8 && _ent.PercentDP < _ent.PercentDPTo8) { _percentDPtext = _ent.PercentDPText8; }
                                    else
                                        if (_ent.PercentDP >= _ent.PercentDPFrom9 && _ent.PercentDP < _ent.PercentDPTo9) { _percentDPtext = _ent.PercentDPText9; }
                                        else
                                            if (_ent.PercentDP >= _ent.PercentDPFrom10 && _ent.PercentDP < _ent.PercentDPTo10) { _percentDPtext = _ent.PercentDPText10; }
        return _percentDPtext;
    }
    private string GetPercentAmountFinanceText(DailyAgingEntities _ent)
    {
        string _amountfinancetext;
        _amountfinancetext = "";
        if (_ent.NTF >= _ent.AmountFinanceFrom1 && _ent.NTF < _ent.AmountFinanceTo1) { _amountfinancetext = _ent.PercentEffectiveText1; }
        else
            if (_ent.NTF >= _ent.AmountFinanceFrom2 && _ent.NTF < _ent.AmountFinanceTo2) { _amountfinancetext = _ent.PercentEffectiveText2; }
            else
                if (_ent.NTF >= _ent.AmountFinanceFrom3 && _ent.NTF < _ent.AmountFinanceTo3) { _amountfinancetext = _ent.PercentEffectiveText3; }
                else
                    if (_ent.NTF >= _ent.AmountFinanceFrom4 && _ent.NTF < _ent.AmountFinanceTo4) { _amountfinancetext = _ent.PercentEffectiveText4; }
                    else
                        if (_ent.NTF >= _ent.AmountFinanceFrom5 && _ent.NTF < _ent.AmountFinanceTo5) { _amountfinancetext = _ent.PercentEffectiveText5; }
                        else
                            if (_ent.NTF >= _ent.AmountFinanceFrom6 && _ent.NTF < _ent.AmountFinanceTo6) { _amountfinancetext = _ent.PercentEffectiveText6; }
                            else
                                if (_ent.NTF >= _ent.AmountFinanceFrom7 && _ent.NTF < _ent.AmountFinanceTo7) { _amountfinancetext = _ent.PercentEffectiveText7; }
                                else
                                    if (_ent.NTF >= _ent.AmountFinanceFrom8 && _ent.NTF < _ent.AmountFinanceTo8) { _amountfinancetext = _ent.PercentEffectiveText8; }
                                    else
                                        if (_ent.NTF >= _ent.AmountFinanceFrom9 && _ent.NTF < _ent.AmountFinanceTo9) { _amountfinancetext = _ent.PercentEffectiveText9; }
                                        else
                                            if (_ent.NTF >= _ent.AmountFinanceFrom10 && _ent.NTF < _ent.AmountFinanceTo10) { _amountfinancetext = _ent.PercentEffectiveText10; }
        return _amountfinancetext;
    }
    private string GetPercentAmountInstallmentText(DailyAgingEntities _ent)
    {
        string _amountinstallmenttext;
        _amountinstallmenttext = "";
        if (_ent.InstallmentAmount >= _ent.AmountInstallmentFrom1 && _ent.InstallmentAmount < _ent.AmountInstallmentTo1) { _amountinstallmenttext = _ent.AmountInstallmentText1; }
        else
            if (_ent.InstallmentAmount >= _ent.AmountInstallmentFrom2 && _ent.InstallmentAmount < _ent.AmountInstallmentTo2) { _amountinstallmenttext = _ent.AmountInstallmentText2; }
            else
                if (_ent.InstallmentAmount >= _ent.AmountInstallmentFrom3 && _ent.InstallmentAmount < _ent.AmountInstallmentTo3) { _amountinstallmenttext = _ent.AmountInstallmentText3; }
                else
                    if (_ent.InstallmentAmount >= _ent.AmountInstallmentFrom4 && _ent.InstallmentAmount < _ent.AmountInstallmentTo4) { _amountinstallmenttext = _ent.AmountInstallmentText4; }
                    else
                        if (_ent.InstallmentAmount >= _ent.AmountInstallmentFrom5 && _ent.InstallmentAmount < _ent.AmountInstallmentTo5) { _amountinstallmenttext = _ent.AmountInstallmentText5; }
                        else
                            if (_ent.InstallmentAmount >= _ent.AmountInstallmentFrom6 && _ent.InstallmentAmount < _ent.AmountInstallmentTo6) { _amountinstallmenttext = _ent.AmountInstallmentText6; }
                            else
                                if (_ent.InstallmentAmount >= _ent.AmountInstallmentFrom7 && _ent.InstallmentAmount < _ent.AmountInstallmentTo7) { _amountinstallmenttext = _ent.AmountInstallmentText7; }
                                else
                                    if (_ent.InstallmentAmount >= _ent.AmountInstallmentFrom8 && _ent.InstallmentAmount < _ent.AmountInstallmentTo8) { _amountinstallmenttext = _ent.AmountInstallmentText8; }
                                    else
                                        if (_ent.InstallmentAmount >= _ent.AmountInstallmentFrom9 && _ent.InstallmentAmount < _ent.AmountInstallmentTo9) { _amountinstallmenttext = _ent.AmountInstallmentText9; }
                                        else
                                            if (_ent.InstallmentAmount >= _ent.AmountInstallmentFrom10 && _ent.InstallmentAmount < _ent.AmountInstallmentTo10) { _amountinstallmenttext = _ent.AmountInstallmentText10; }
        return _amountinstallmenttext;
    }
    private decimal GetAmountOverDueGross(DailyAgingEntities _ent)
    {
        decimal _amountoverduegross;

        _amountoverduegross = _ent.BucketGross1 +
                                   _ent.BucketGross2 +
                                   _ent.BucketGross3 +
                                   _ent.BucketGross4 +
                                   _ent.BucketGross5 +
                                   _ent.BucketGross6 +
                                   _ent.BucketGross7 +
                                   _ent.BucketGross8 +
                                   _ent.BucketGross9 +
                                   _ent.BucketGross10;
        return _amountoverduegross;
    }
    private decimal GetAmountOverduePrinciple(DailyAgingEntities _ent)
    {
        decimal _amountoverduegross;
        _amountoverduegross = _ent.BucketPrincipal1 +
                                   _ent.BucketPrincipal2 +
                                   _ent.BucketPrincipal3 +
                                   _ent.BucketPrincipal4 +
                                   _ent.BucketPrincipal5 +
                                   _ent.BucketPrincipal6 +
                                   _ent.BucketPrincipal7 +
                                   _ent.BucketPrincipal8 +
                                   _ent.BucketPrincipal9 +
                                   _ent.BucketPrincipal10;
        return _amountoverduegross;
    }

    private DailyAgingEntities PrepareTempTable (DailyAgingEntities _ent)
    {
        DataTable _dtagreement = new DataTable();
        _ent.dtAging = _dtagreement;
        _ent.dtAging.Columns.Add("AgingDate", typeof(DateTime));
        _ent.dtAging.Columns.Add("BranchID", typeof(String));
        _ent.dtAging.Columns.Add("ApplicationID", typeof(String));
        _ent.dtAging.Columns.Add("DailyMonthly", typeof(String));
        _ent.dtAging.Columns.Add("ReportID", typeof(String));
        _ent.dtAging.Columns.Add("AgreementNo", typeof(String));
        _ent.dtAging.Columns.Add("CustomerID", typeof(String));
        _ent.dtAging.Columns.Add("DaysOverdue", typeof(int));
        _ent.dtAging.Columns.Add("CurrencyID", typeof(String));
        _ent.dtAging.Columns.Add("AmountOverDueGross", typeof(Decimal));
        _ent.dtAging.Columns.Add("AmountOverDuePrinciple", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket1_gross", typeof (Decimal));
        _ent.dtAging.Columns.Add("Bucket1_principle", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket2_gross", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket2_principle", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket3_gross", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket3_principle", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket4_gross", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket4_principle", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket5_gross", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket5_principle", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket6_gross", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket6_principle", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket7_Gross", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket7_principle", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket8_Gross", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket8_principle", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket9_Gross", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket9_principle", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket10_Gross", typeof(Decimal));
        _ent.dtAging.Columns.Add("Bucket10_principle", typeof(Decimal));
        _ent.dtAging.Columns.Add("PercentDP", typeof(Decimal));
        _ent.dtAging.Columns.Add("PercentDP_text", typeof(String));
        _ent.dtAging.Columns.Add("EffectiveRate", typeof(Decimal));
        _ent.dtAging.Columns.Add("EffectiveRate_text", typeof(String));
        _ent.dtAging.Columns.Add("AmountFinance", typeof(Decimal));
        _ent.dtAging.Columns.Add("AmountFinance_text", typeof(String));
        _ent.dtAging.Columns.Add("Installmentamount", typeof(Decimal));
        _ent.dtAging.Columns.Add("AmountInstallment_text", typeof(String));

        _ent.dtAging.Columns.Add("TotalOSPrincipal", typeof(Decimal));
        _ent.dtAging.Columns.Add("TotalOSInterest", typeof(Decimal));
        _ent.dtAging.Columns.Add("TotalOSPrincipalUndue", typeof(Decimal));
        _ent.dtAging.Columns.Add("TotalOSInterestUndue", typeof(Decimal));
        _ent.dtAging.Columns.Add("ContractPrepaidAmount", typeof(Decimal));
        _ent.dtAging.Columns.Add("LCInstallment", typeof(Decimal));
        _ent.dtAging.Columns.Add("LCInstallmentPaid", typeof(Decimal));
        _ent.dtAging.Columns.Add("LCInstallmentWaived", typeof(Decimal));
        _ent.dtAging.Columns.Add("LastLCInstallmentCalcDate", typeof(DateTime));
        _ent.dtAging.Columns.Add("LCInstallmentCurrent", typeof(Decimal));
        _ent.dtAging.Columns.Add("LCInsurance", typeof(Decimal));
        _ent.dtAging.Columns.Add("LCInsurancePaid", typeof(Decimal));
        _ent.dtAging.Columns.Add("LCInsuranceWaived", typeof(Decimal));
        _ent.dtAging.Columns.Add("LastLCInsuranceCalcDate", typeof(DateTime));
        _ent.dtAging.Columns.Add("InstallmentDue", typeof(Decimal));
        _ent.dtAging.Columns.Add("InstallmentDuePaid", typeof(Decimal));
        _ent.dtAging.Columns.Add("InstallmentDueWaived", typeof(Decimal));
        _ent.dtAging.Columns.Add("InsuranceDue", typeof(Decimal));
        _ent.dtAging.Columns.Add("InsuranceDuePaid", typeof(Decimal));
        _ent.dtAging.Columns.Add("InsuranceDueWaived", typeof(Decimal));
        _ent.dtAging.Columns.Add("PDCBounceFee", typeof(Decimal));
        _ent.dtAging.Columns.Add("PDCBounceFeePaid", typeof(Decimal));
        _ent.dtAging.Columns.Add("PDCBounceFeeWaived", typeof(Decimal));
        _ent.dtAging.Columns.Add("CollectionExpense", typeof(Decimal));
        _ent.dtAging.Columns.Add("CollectionExpensePaid", typeof(Decimal));
        _ent.dtAging.Columns.Add("CollectionExpenseWaived", typeof(Decimal));
        _ent.dtAging.Columns.Add("NADate", typeof(DateTime));
        _ent.dtAging.Columns.Add("NAAmount", typeof(Decimal));
        _ent.dtAging.Columns.Add("NAPaid", typeof(Decimal));
        _ent.dtAging.Columns.Add("NAWaived", typeof(Decimal));
        _ent.dtAging.Columns.Add("WODate", typeof(DateTime));
        _ent.dtAging.Columns.Add("WOAmount", typeof(Decimal));
        _ent.dtAging.Columns.Add("WOPaid", typeof(Decimal));
        _ent.dtAging.Columns.Add("WOWaived", typeof(Decimal));
        _ent.dtAging.Columns.Add("InstallmentNo", typeof(int));
        _ent.dtAging.Columns.Add("InstallmentDate", typeof(DateTime));
        _ent.dtAging.Columns.Add("ContractStatus", typeof(String));
        _ent.dtAging.Columns.Add("DefaultStatus", typeof(String));
        _ent.dtAging.Columns.Add("NumOfAssetUnit", typeof(int));
        _ent.dtAging.Columns.Add("CollectibilityId", typeof(String));
        _ent.dtAging.Columns.Add("NextInstallmentDueNumber", typeof(int));
        _ent.dtAging.Columns.Add("NextInstallmentDueDate",typeof(DateTime));
        _ent.dtAging.Columns.Add("AOID", typeof(String));
        _ent.dtAging.Columns.Add("SurveyorID", typeof(String));
        _ent.dtAging.Columns.Add("CAID", typeof(String));
        _ent.dtAging.Columns.Add("IsFPD", typeof(String));
        _ent.dtAging.Columns.Add("ProductID", typeof(String));
        _ent.dtAging.Columns.Add("ProductOfferingID", typeof(String));
        _ent.dtAging.Columns.Add("NTF", typeof(Decimal));
        _ent.dtAging.Columns.Add("PickUpFee", typeof(Decimal));
        _ent.dtAging.Columns.Add("VisitFee", typeof(Decimal));
        _ent.dtAging.Columns.Add("PickUpFeeCharges", typeof(Decimal));
        _ent.dtAging.Columns.Add("PickUpFeePaid", typeof(Decimal));
        _ent.dtAging.Columns.Add("PickUpFeeWaived", typeof(Decimal));
        _ent.dtAging.Columns.Add("VisitFeeCharges", typeof(Decimal));
        _ent.dtAging.Columns.Add("VisitFeePaid", typeof(Decimal));
        _ent.dtAging.Columns.Add("VisitFeeWaived", typeof(Decimal));
        _ent.dtAging.Columns.Add("Distance_ID", typeof(int));
        _ent.dtAging.Columns.Add("Distance_Desc", typeof(String));

        return _ent;
    }

    private void DailyAgingSave(DailyAgingEntities _ent)
    {
        SqlConnection _conn = new SqlConnection("context connection=true");
        SqlCommand _cmd = new SqlCommand();
        StringBuilder sb_queryinsert = new StringBuilder();
        //CommonClass _common = new CommonClass();

        // Total OS Principal = Agreement.OutStandingPrincipal
        // Total OS Interest = Agreement.Outstanding Interest
        // Total OS Principal Undue = Agreement.OutStandingPrincipalUndue
        // Total OS Interest Undue = Agreement.Outstanding Interest Undue

        sb_queryinsert.AppendLine("Insert Into DailyAging (AgingDate, BranchID, ApplicationID, DailyMonthly, ReportID, AgreementNo,");
        sb_queryinsert.AppendLine (" CustomerID, DaysOverdue, CurrencyID, AmountOverDueGross, ");
        sb_queryinsert.AppendLine("AmountOverDuePrinciple, Bucket1_gross, Bucket1_principle, Bucket2_gross, Bucket2_principle, Bucket3_gross, Bucket3_principle, Bucket4_gross, ");
        sb_queryinsert.AppendLine("Bucket4_principle, Bucket5_gross, Bucket5_principle, Bucket6_gross, Bucket6_principle, Bucket7_Gross, Bucket7_principle, Bucket8_Gross, ");
        sb_queryinsert.AppendLine("Bucket8_principle, Bucket9_Gross, Bucket9_principle, Bucket10_Gross, Bucket10_principle, PercentDP, PercentDP_text, EffectiveRate, EffectiveRate_text, ");
        sb_queryinsert.AppendLine("AmountFinance, AmountFinance_text, AmountInstallment, AmountInstallment_text,  ");
        sb_queryinsert.AppendLine("TotalOSPrincipal, TotalOSInterest, TotalOSPrincipalUndue, TotalOSInterestUndue, ContractPrepaidAmount, ");
        sb_queryinsert.AppendLine("LCInstallment, LCInstallmentPaid, LCInstallmentWaived, LastLCInstallmentCalcDate, LCInstallmentCurrent, ");
        sb_queryinsert.AppendLine("LCInsurance, LCInsurancePaid, LCInsuranceWaived, LastLCInsuranceCalcDate, ");
        sb_queryinsert.AppendLine("InstallmentDue, InstallmentDuePaid, InstallmentDueWaived,  ");
        sb_queryinsert.AppendLine("InsuranceDue, InsuranceDuePaid, InsuranceDueWaived, ");
        sb_queryinsert.AppendLine("PDCBounceFee, PDCBounceFeePaid, PDCBounceFeeWaived, ");
        sb_queryinsert.AppendLine("CollectionExpense, CollectionExpensePaid,  CollectionExpenseWaived,");
        sb_queryinsert.AppendLine("NADate, NAAmount, NAPaid, NAWaived, WODate, WOAmount, WOPaid, WOWaived, ");
        sb_queryinsert.AppendLine("InstallmentNo, InstallmentDate, ");
        sb_queryinsert.AppendLine("ContractStatus, DefaultStatus, ");
        
        sb_queryinsert.AppendLine("NumOfAssetUnit, ");
        sb_queryinsert.AppendLine(" NextInstallmentDueNumber, NextInstallmentDueDate, ");
        sb_queryinsert.AppendLine("AOID, SurveyorID, CAID, IsFPD, ProductID, ProductOfferingID, NTF, ");
        sb_queryinsert.AppendLine("PickUpFee, VisitFee, PickUpFeeCharges, PickUpFeePaid, PickUpFeeWaived, ");
        sb_queryinsert.AppendLine("VisitFeeCharges, VisitFeePaid, VisitFeeWaived, Distance_ID, Distance_Desc, UsrUpd, DtmUpd) ");
        sb_queryinsert.AppendLine(" Values (@AgingDate, @BranchID, @ApplicationID, @DailyMonthly, @ReportID, @AgreementNo, @CustomerID, @DaysOverdue, "); //, @CustomerID, @DaysOverdue, @CurrencyID, @AmountOverDueGross, ");
        sb_queryinsert.AppendLine(" AmountOverDueGross, @AmountOverDuePrinciple, @Bucket1_gross, @Bucket1_principle, @Bucket2_gross, @Bucket2_principle, @Bucket3_gross, @Bucket3_principle, @Bucket4_gross, ");
        sb_queryinsert.AppendLine(" @Bucket4_principle, @Bucket5_gross, @Bucket5_principle, @Bucket6_gross, @Bucket6_principle, @Bucket7_Gross, @Bucket7_principle, @Bucket8_Gross, ");
        sb_queryinsert.AppendLine(" @Bucket8_principle, @Bucket9_Gross, @Bucket9_principle, @Bucket10_Gross, @Bucket10_principle, @PercentDP, @PercentDP_text, @EffectiveRate, @EffectiveRate_text, ");
        sb_queryinsert.AppendLine(" @AmountFinance, @AmountFinance_text, @InstallmentAmount, @AmountInstallment_text, "); //, ");
        sb_queryinsert.AppendLine(" @TotalOSPrincipal, @TotalOSInterest, @TotalOSPrincipalUndue, @TotalOSInterestUndue, @ContractPrepaidAmount, ");
        sb_queryinsert.AppendLine(" @LCInstallment, @LCInstallmentPaid, @LCInstallmentWaived, @LastLCInstallmentCalcDate, @LCInstallmentCurrent, ");
        sb_queryinsert.AppendLine(" @LCInsurance, @LCInsurancePaid, @LCInsuranceWaived, @LastLCInsuranceCalcDate, ");

        sb_queryinsert.AppendLine(" @InstallmentDue, @InstallmentDuePaid, @InstallmentDueWaived, ");
        sb_queryinsert.AppendLine(" @InsuranceDue, @InsuranceDuePaid, @InsuranceDueWaived, ");
        sb_queryinsert.AppendLine(" @PDCBounceFee, @PDCBounceFeePaid, @PDCBounceFeeWaived,  ");
        sb_queryinsert.AppendLine(" @CollectionExpense, @CollectionExpensePaid, @CollectionExpenseWaived,");
        sb_queryinsert.AppendLine(" @NADate, @NAAmount, @NAPaid, @NAWaived, @WODate, @WOAmount, @WOPaid, @WOWaived, ");
        sb_queryinsert.AppendLine(" @InstallmentNo,    @InstallmentDate, ");
        sb_queryinsert.AppendLine(" @ContractStatus, @DefaultStatus,  ");
        sb_queryinsert.AppendLine(" @NumOfAssetUnit, ");
        sb_queryinsert.AppendLine(" @NextInstallmentDueNumber, @NextInstallmentDueDate, ");
        sb_queryinsert.AppendLine(" @AOID, @SurveyorID, @CAID, @IsFPD, @ProductID, @ProductOfferingID, @NTF,  ");
        sb_queryinsert.AppendLine(" @PickUpFee, @VisitFee, @PickUpFeeCharges, @PickUpFeePaid, @PickUpFeeWaived, ");
        sb_queryinsert.AppendLine(" @VisitFeeCharges, @VisitFeePaid, @VisitFeeWaived, @Distance_ID, @Distance_Desc, @UsrCrt, @DtmCrt)");


        sb_queryinsert.Clear();
        sb_queryinsert.Append(" Exec spDailyAgingTestCLR @AgingDate, @BranchID, @ApplicationID, @DailyMonthly, @ReportID, @AgreementNo, @CustomerID, @DaysOverdue, @CurrencyID, ");
        sb_queryinsert.Append(" @AmountOverDueGross, @AmountOverDuePrinciple, ");
        sb_queryinsert.Append(" @Bucket1_gross, @Bucket1_principle, @Bucket2_gross, @Bucket2_principle, @Bucket3_gross, @Bucket3_principle, @Bucket4_gross, ");
        sb_queryinsert.Append(" @Bucket4_principle, @Bucket5_gross, @Bucket5_principle, @Bucket6_gross, @Bucket6_principle, @Bucket7_Gross, @Bucket7_principle, @Bucket8_Gross, ");
        sb_queryinsert.Append(" @Bucket8_principle, @Bucket9_Gross, @Bucket9_principle, @Bucket10_Gross, @Bucket10_principle --, @PercentDP, @PercentDP_text, @EffectiveRate, @EffectiveRate_text, ");
        sb_queryinsert.Append(" @AmountFinance, @AmountFinance_text, @InstallmentAmount, @AmountInstallment_text, ");
        sb_queryinsert.Append(" @TotalOSPrincipal, @TotalOSInterest, @TotalOSPrincipalUndue, @TotalOSInterestUndue, @ContractPrepaidAmount, ");
        sb_queryinsert.Append(" @LCInstallment, @LCInstallmentPaid, @LCInstallmentWaived, @LastLCInstallmentCalcDate, @LCInstallmentCurrent, ");
        sb_queryinsert.Append(" @LCInsurance, @LCInsurancePaid, @LCInsuranceWaived, @LastLCInsuranceCalcDate, ");

        sb_queryinsert.Append(" @InstallmentDue, @InstallmentDuePaid, @InstallmentDueWaived, ");
        sb_queryinsert.Append(" @InsuranceDue, @InsuranceDuePaid, @InsuranceDueWaived, ");
        sb_queryinsert.Append(" @PDCBounceFee, @PDCBounceFeePaid, @PDCBounceFeeWaived,  ");
        sb_queryinsert.Append(" @CollectionExpense, @CollectionExpensePaid, @CollectionExpenseWaived,");
        sb_queryinsert.Append(" @NADate, @NAAmount, @NAPaid, @NAWaived, @WODate, @WOAmount, @WOPaid, @WOWaived , ");
        sb_queryinsert.Append(" @InstallmentNo,    @InstallmentDate, ");
        sb_queryinsert.Append(" @ContractStatus, @DefaultStatus,  ");
        sb_queryinsert.Append(" @NumOfAssetUnit, ");
        sb_queryinsert.Append(" @NextInstallmentDueNumber, @NextInstallmentDueDate, ");
        sb_queryinsert.Append(" @AOID, @SurveyorID, @CAID, @IsFPD, @ProductID, @ProductOfferingID, @NTF,  ");
        sb_queryinsert.Append(" @PickUpFee, @VisitFee, @PickUpFeeCharges, @PickUpFeePaid, @PickUpFeeWaived, ");
        sb_queryinsert.Append(" @VisitFeeCharges, @VisitFeePaid, @VisitFeeWaived, @Distance_ID, @Distance_Desc, @UsrCrt, @DtmCrt");
        using (_conn)
        {
            if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
                if (_ent.dtAging.Rows.Count >0)
                {
                    
                foreach (DataRow _row in _ent.dtAging.Rows)
                {
    
                    using (_cmd = _conn.CreateCommand())
                    {
                        _cmd.CommandText = sb_queryinsert.ToString();
                        _cmd.Connection = _conn;
                        _cmd.Parameters.AddWithValue("@AgingDate", (DateTime)_row["AgingDate"]);
                        _cmd.Parameters.AddWithValue("@BranchID", (String)_row["BranchID"]);
                        _cmd.Parameters.AddWithValue("@ApplicationID", (String)_row["ApplicationID"]);
                        _cmd.Parameters.AddWithValue("@DailyMonthly", (String)_row["DailyMonthly"]);

                        _cmd.Parameters.AddWithValue("@ReportID", (String)_row["ReportID"]);
                        //_cmd.Parameters.AddWithValue("@ReportID", 1);

                        _cmd.Parameters.AddWithValue("@AgreementNo", (String)_row["AgreementNo"]);
                        _cmd.Parameters.AddWithValue("@CustomerID", (String)_row["CustomerID"]);
                        _cmd.Parameters.AddWithValue("@DaysOverdue", (Int32)_row["DaysOverdue"]);
                        _cmd.Parameters.AddWithValue("@CurrencyID", (String)_row["CurrencyID"]);

                        //_cmd.Parameters.Add("@AmountOverDueGross", SqlDbType.Money).Value = (Decimal)_row["AmountOverDueGross"];


                        _cmd.Parameters.AddWithValue("@AmountOverDueGross", (Decimal)_row["AmountOverDueGross"]);
                        _cmd.Parameters.AddWithValue("@AmountOverDuePrinciple", (Decimal)_row["AmountOverDuePrinciple"]);

                        _cmd.Parameters.AddWithValue("@Bucket1_gross", (Decimal)_row["Bucket1_gross"]);
                        _cmd.Parameters.AddWithValue("@Bucket2_gross", (Decimal)_row["Bucket2_gross"]);
                        _cmd.Parameters.AddWithValue("@Bucket3_gross", (Decimal)_row["Bucket3_gross"]);
                        _cmd.Parameters.AddWithValue("@Bucket4_gross", (Decimal)_row["Bucket4_gross"]);
                        _cmd.Parameters.AddWithValue("@Bucket5_gross", (Decimal)_row["Bucket5_gross"]);
                        _cmd.Parameters.AddWithValue("@Bucket6_gross", (Decimal)_row["Bucket6_gross"]);
                        _cmd.Parameters.AddWithValue("@Bucket7_Gross", (Decimal)_row["Bucket7_Gross"]);
                        _cmd.Parameters.AddWithValue("@Bucket8_Gross", (Decimal)_row["Bucket8_Gross"]);
                        _cmd.Parameters.AddWithValue("@Bucket9_Gross", (Decimal)_row["Bucket9_Gross"]);
                        _cmd.Parameters.AddWithValue("@Bucket10_Gross", (Decimal)_row["Bucket10_Gross"]);

                        _cmd.Parameters.AddWithValue("@Bucket1_principle", (Decimal)_row["Bucket1_principle"]);
                        _cmd.Parameters.AddWithValue("@Bucket2_principle", (Decimal)_row["Bucket2_principle"]);
                        _cmd.Parameters.AddWithValue("@Bucket3_principle", (Decimal)_row["Bucket3_principle"]);
                        _cmd.Parameters.AddWithValue("@Bucket4_principle", (Decimal)_row["Bucket4_principle"]);
                        _cmd.Parameters.AddWithValue("@Bucket5_principle", (Decimal)_row["Bucket5_principle"]);
                        _cmd.Parameters.AddWithValue("@Bucket6_principle", (Decimal)_row["Bucket6_principle"]);
                        _cmd.Parameters.AddWithValue("@Bucket7_principle", (Decimal)_row["Bucket7_principle"]);
                        _cmd.Parameters.AddWithValue("@Bucket8_principle", (Decimal)_row["Bucket8_principle"]);
                        _cmd.Parameters.AddWithValue("@Bucket9_principle", (Decimal)_row["Bucket9_principle"]);
                        _cmd.Parameters.AddWithValue("@Bucket10_principle", (Decimal)_row["Bucket10_principle"]);

                        _cmd.Parameters.AddWithValue("@PercentDP", (Decimal)_row["PercentDP"]);
                        _cmd.Parameters.AddWithValue("@PercentDP_text", (String)_row["PercentDP_text"]);

                        _cmd.Parameters.AddWithValue("@EffectiveRate", (Decimal)_row["EffectiveRate"]);
                        _cmd.Parameters.AddWithValue("@EffectiveRate_text", (String)_row["EffectiveRate_text"]);

                        _cmd.Parameters.AddWithValue("@AmountFinance", (Decimal)_row["AmountFinance"]);
                        _cmd.Parameters.AddWithValue("@AmountFinance_text", (String)_row["AmountFinance_text"]);

                        _cmd.Parameters.AddWithValue("@InstallmentAmount", (Decimal)_row["InstallmentAmount"]);
                        _cmd.Parameters.AddWithValue("@AmountInstallment_text", (String)_row["AmountInstallment_text"]);

                        _cmd.Parameters.AddWithValue("@TotalOSPrincipal", (Decimal)_row["TotalOSPrincipal"]);
                        _cmd.Parameters.AddWithValue("@TotalOSInterest", (Decimal)_row["TotalOSInterest"]);
                        _cmd.Parameters.AddWithValue("@TotalOSPrincipalUndue", (Decimal)_row["TotalOSPrincipalUndue"]);
                        _cmd.Parameters.AddWithValue("@TotalOSInterestUndue", (Decimal)_row["TotalOSInterestUndue"]);
                        _cmd.Parameters.AddWithValue("@ContractPrepaidAmount", (Decimal)_row["ContractPrepaidAmount"]);

                        _cmd.Parameters.AddWithValue("@LCInstallment", (Decimal)_row["LCInstallment"]);
                        _cmd.Parameters.AddWithValue("@LCInstallmentPaid", (Decimal)_row["LCInstallmentPaid"]);
                        _cmd.Parameters.AddWithValue("@LCInstallmentWaived", (Decimal)_row["LCInstallmentWaived"]);
                     
                        if (_row["LastLCInstallmentCalcDate"].ToString() == "")
                        { _cmd.Parameters.AddWithValue("@LastLCInstallmentCalcDate", System.DBNull.Value); }
                        else
                        { _cmd.Parameters.AddWithValue("@LastLCInstallmentCalcDate", (DateTime)_row["LastLCInstallmentCalcDate"]); }
                        

                        _cmd.Parameters.AddWithValue("@LCInstallmentCurrent", (Decimal)_row["LCInstallmentCurrent"]);
                        
                        _cmd.Parameters.AddWithValue("@LCInsurance", (Decimal)_row["LCInsurance"]);
                        _cmd.Parameters.AddWithValue("@LCInsurancePaid", (Decimal)_row["LCInsurancePaid"]);
                        _cmd.Parameters.AddWithValue("@LCInsuranceWaived", (Decimal)_row["LCInsuranceWaived"]);


                        if (_row["LastLCInsuranceCalcDate"].ToString() == "")
                        { _cmd.Parameters.AddWithValue("@LastLCInsuranceCalcDate", System.DBNull.Value); }
                        else
                        { _cmd.Parameters.AddWithValue("@LastLCInsuranceCalcDate", (DateTime)_row["LastLCInsuranceCalcDate"]); }
                        

                        _cmd.Parameters.AddWithValue("@InstallmentDue", (Decimal)_row["InstallmentDue"]);
                        _cmd.Parameters.AddWithValue("@InstallmentDuePaid", (Decimal)_row["InstallmentDuePaid"]);
                        _cmd.Parameters.AddWithValue("@InstallmentDueWaived", (Decimal)_row["InstallmentDueWaived"]);
                        _cmd.Parameters.AddWithValue("@InsuranceDue", (Decimal)_row["InsuranceDue"]);
                        _cmd.Parameters.AddWithValue("@InsuranceDuePaid", (Decimal)_row["InsuranceDuePaid"]);
                        _cmd.Parameters.AddWithValue("@InsuranceDueWaived", (Decimal)_row["InsuranceDueWaived"]);
                        _cmd.Parameters.AddWithValue("@PDCBounceFee", (Decimal)_row["PDCBounceFee"]);
                        _cmd.Parameters.AddWithValue("@PDCBounceFeePaid", (Decimal)_row["PDCBounceFeePaid"]);
                        _cmd.Parameters.AddWithValue("@PDCBounceFeeWaived", (Decimal)_row["PDCBounceFeeWaived"]);
                        _cmd.Parameters.AddWithValue("@CollectionExpense", (Decimal)_row["CollectionExpense"]);
                        _cmd.Parameters.AddWithValue("@CollectionExpensePaid", (Decimal)_row["CollectionExpensePaid"]);
                        _cmd.Parameters.AddWithValue("@CollectionExpenseWaived", (Decimal)_row["CollectionExpenseWaived"]);

                        if (_row["NADate"].ToString() == "")
                        { _cmd.Parameters.AddWithValue("@NADate", System.DBNull.Value); }
                        else
                        { _cmd.Parameters.AddWithValue("@NADate", (DateTime)_row["NADate"]); }


                        _cmd.Parameters.AddWithValue("@NAAmount ", (Decimal)_row["NAAmount"]);
                        _cmd.Parameters.AddWithValue("@NAPaid", (Decimal)_row["NAPaid"]);
                        _cmd.Parameters.AddWithValue("@NAWaived", (Decimal)_row["NAWaived"]);

                        if (_row["WODate"].ToString() == "")
                        { _cmd.Parameters.AddWithValue("@WODate", System.DBNull.Value); }
                        else
                        { _cmd.Parameters.AddWithValue("@WODate", (DateTime)_row["WODate"]); }

                        
                        _cmd.Parameters.AddWithValue("@WOAmount", (Decimal)_row["WOAmount"]);
                        _cmd.Parameters.AddWithValue("@WOPaid", (Decimal)_row["WOPaid"]);
                        _cmd.Parameters.AddWithValue("@WOWaived", (Decimal)_row["WOWaived"]);

                        _cmd.Parameters.AddWithValue("@InstallmentNo", (int)_row["InstallmentNo"]);

                        if (_row["InstallmentDate"].ToString() == "")
                        { _cmd.Parameters.AddWithValue("@InstallmentDate", System.DBNull.Value); }
                        else
                        { _cmd.Parameters.AddWithValue("@InstallmentDate", (DateTime)_row["InstallmentDate"]); }
                        

                        _cmd.Parameters.AddWithValue("@ContractStatus", (String)_row["ContractStatus"]);
                        _cmd.Parameters.AddWithValue("@DefaultStatus", (String)_row["DefaultStatus"]);
                        _cmd.Parameters.AddWithValue("@NumOfAssetUnit", (int)_row["NumOfAssetUnit"]);
                        _cmd.Parameters.AddWithValue("@NextInstallmentDueNumber", (int)_row["NextInstallmentDueNumber"]);

                        if (_row["NextInstallmentDueDate"].ToString() == "")
                        {  _cmd.Parameters.AddWithValue("@NextInstallmentDueDate",System.DBNull.Value); }
                        else
                        {_cmd.Parameters.AddWithValue("@NextInstallmentDueDate", (DateTime)_row["NextInstallmentDueDate"]); }

                        _cmd.Parameters.AddWithValue("@AOID", (String)_row["AOID"]);
                        _cmd.Parameters.AddWithValue("@SurveyorID", (String)_row["SurveyorID"]);
                        _cmd.Parameters.AddWithValue("@CAID", (String)_row["CAID"]);
                        
                        if (_row["IsFPD"].ToString() == "False")
                        {
                            _cmd.Parameters.AddWithValue("@IsFPD","False");
                        }
                        else
                        {
                            _cmd.Parameters.AddWithValue("@IsFPD", "True");
                        }

                        _cmd.Parameters.AddWithValue("@ProductID", (String)_row["ProductID"]);
                        _cmd.Parameters.AddWithValue("@ProductOfferingID", (String)_row["ProductOfferingID"]);

                        _cmd.Parameters.AddWithValue("@NTF", (Decimal)_row["NTF"]);
                        _cmd.Parameters.AddWithValue("@PickUpFee", (Decimal)_row["PickUpFee"]);
                        _cmd.Parameters.AddWithValue("@VisitFee", (Decimal)_row["VisitFee"]);
                        _cmd.Parameters.AddWithValue("@PickUpFeeCharges", (Decimal)_row["PickUpFeeCharges"]);
                        _cmd.Parameters.AddWithValue("@PickUpFeePaid", (Decimal)_row["PickUpFeePaid"]);
                        _cmd.Parameters.AddWithValue("@PickUpFeeWaived", (Decimal)_row["PickUpFeeWaived"]);
                        _cmd.Parameters.AddWithValue("@VisitFeeCharges", (Decimal)_row["VisitFeeCharges"]);
                        _cmd.Parameters.AddWithValue("@VisitFeePaid", (Decimal)_row["VisitFeePaid"]);
                        _cmd.Parameters.AddWithValue("@VisitFeeWaived", (Decimal)_row["VisitFeeWaived"]);

                        _cmd.Parameters.AddWithValue("@Distance_ID", (int)_row["Distance_ID"]);
                        _cmd.Parameters.AddWithValue("@Distance_Desc", (String)_row["Distance_Desc"]);
                        _cmd.Parameters.AddWithValue("@UsrCrt", "EOD");
                        _cmd.Parameters.AddWithValue("@DtmCrt", DateTime.Now.ToString());
                        
                        _cmd.ExecuteNonQuery();
                        _cmd.Parameters.Clear();
                    }
                }

            }
        }
    }
    
    public void DailyAgingProc(string BranchID, DateTime BusinessDate)
    {
        // Put your code here
        DailyAgingEntities _ent = new DailyAgingEntities();
        SqlConnection _conn = new SqlConnection("context connection=true");
        SqlCommand _cmd = new SqlCommand();
        SqlDataReader _rdr;

        _ent = DailyAgingReport(_ent);
        _ent = PrepareTempTable(_ent);
        _conn.Open();
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        StringBuilder sb3 = new StringBuilder();
        StringBuilder sb4 = new StringBuilder();
        DateTime _today =   BusinessDate;
        DateTime _agingdate = _today.AddDays(1);
        _ent.DailyMonthly = "M";
        if (_agingdate.Month.ToString() == _today.Month.ToString())
        {
            _ent.DailyMonthly = "D";
        }

        sb.AppendLine("SELECT A.BranchID, A.ApplicationID, AgreementNo, CustomerID,  CurrencyID,  ");
        sb.AppendLine(" DueDate, ");
        sb.AppendLine(" (Case When DaysOverDue < 0 Then 0 Else DaysOverDue End) as DaysOverDue,");
        sb.AppendLine("    Bucket1_principle, Bucket2_principle, Bucket3_principle, Bucket4_principle, Bucket5_principle, Bucket6_principle,");
        sb.AppendLine("    Bucket7_principle, Bucket8_principle, Bucket9_principle, Bucket10_principle,");
        sb.AppendLine("    Bucket1_gross, Bucket2_gross, Bucket3_gross, Bucket4_gross, Bucket5_gross, Bucket6_gross, Bucket7_gross, Bucket8_gross,");
        sb.AppendLine("    Bucket9_gross, Bucket10_gross, NumOfAsset,");
        sb.AppendLine (" EffectiveRate, ContractPrepaidAmount, LCInstallment, LCInstallmentPaid, LCInstallmentWaived, LastLCInstallmentCalcDate, ");
        sb.AppendLine("LCInsurance, LCInsurancePaid, LCInsuranceWaived, LastLCInsuranceCalcDate, ");
        sb.AppendLine("dbo.FnCalculationForLCInstallment(A.Applicationid, @Today) as LCInstallmentCurrent, ");
        sb.AppendLine("InstallmentDue, InstallmentDuePaid, InstallmentDueWaived, InsuranceDue,  ");
        sb.AppendLine("InsuranceDuePaid, InsuranceDueWaived, PDCBounceFee, PDCBounceFeePaid, PDCBounceFeeWaived,  ");
        sb.AppendLine(" OutstandingPrincipal as TotalOSPrincipal, ");
        sb.AppendLine("   ([OutstandingInterest])as TotalOSInterest, ");
        sb.AppendLine("	([OutstandingPrincipalUndue])as TotalOSPrincipalUndue, ");
        sb.AppendLine("	([OutstandingInterestUndue])as  TotalOSInterestUndue, ");
        sb.AppendLine(" CollectionExpense, CollectionExpensePaid,  ");
        sb.AppendLine("CollectionExpenseWaived, NADate, NAAmount, NAPaid, NAWaived, WODate, WOAmount, WOPaid, WOWaived, ContractStatus, DefaultStatus,  ");
        sb.AppendLine(" NumOfAssetUnit,  ");
        sb.AppendLine("NextInstallmentNumber, NextInstallmentDate, ");
        sb.AppendLine("isnull((select case when (InsSeqNo+1) > A.Numofinstallment then A.Numofinstallment ");
        sb.AppendLine("		else InsSeqNo+1 end from installmentschedule with (nolock) ");
        sb.AppendLine("	where installmentschedule.BranchID = A.BranchID And installmentschedule.ApplicationID = A.ApplicationID and duedate = DateAdd(dd,1,@today)) ");
        sb.AppendLine("       , NextInstallmentDueNumber) as NextInstallmentDueNumber, ");
        sb.AppendLine(" (select Top 1  MIN(duedate) Over (Partition By BranchID, ApplicationID, DueDate) from installmentschedule with (nolock) ");
        sb.AppendLine("where applicationid = A.applicationid and branchid = A.branchid and ");
        sb.AppendLine(" duedate > DateAdd(dd,1,@today))  as NextInstallmentDueDate,	");
        sb.AppendLine("AOID, SurveyorID, CAID, IsFPD, ProductID, ProductOfferingID, NTF as AmountFinance,  ");
        sb.AppendLine("IsNull(PickUpFee,0) as PickUpFee, IsNull(VisitFee,0) as VisitFee, IsNull(PickUpFeeCharges,0) as PickUpFeeCharges, IsNull(PickUpFeePaid,0) as PickUpFeePaid, IsNull(PickUpFeeWaived, 0) as PickUpFeeWaived, ");
        sb.AppendLine("IsNull(VisitFeeCharges,0) as VisitFeeCharges, IsNull(VisitFeePaid,0) as VisitFeePaid, IsNull(VisitFeeWaived,0) as VisitFeeWaived, Isnull(Distance_ID,0) as Distance_ID, ");
        sb.AppendLine(" IsNull(Distance_Desc,0) as Distance_Desc,  ");
        sb.AppendLine("((case when totalOTR <> 0 then DownPayment/TotalOTR else 0 end )*100)as PercentDP, Installmentamount ");
        sb.AppendLine("FROM dbo.Agreement A With (nolock) ");
        #region "Query Bucket Gross and Principal"
        sb2.AppendLine("Inner Join ( ");
        sb2.AppendLine("Select	Distinct ");
        sb2.AppendLine("    QryAging1.ApplicationID, QryAging1.BranchID, DueDate, ");
        sb2.AppendLine (" (Case When DaysOverDue < 0 Then 0 Else DaysOverDue End) as DaysOverDue,");
        sb2.AppendLine("    Bucket1_principle, Bucket2_principle, Bucket3_principle, Bucket4_principle, Bucket5_principle, Bucket6_principle,");
        sb2.AppendLine("    Bucket7_principle, Bucket8_principle, Bucket9_principle, Bucket10_principle,");
        sb2.AppendLine("    Bucket1_gross, Bucket2_gross, Bucket3_gross, Bucket4_gross, Bucket5_gross, Bucket6_gross, Bucket7_gross, Bucket8_gross,");
        sb2.AppendLine("    Bucket9_gross, Bucket10_gross, QryAging1.NumOfAsset");
        sb2.AppendLine(" From(  Select ApplicationID,BranchID, Max(DaysOverDue) Over (Partition by ApplicationID,BranchID) as DaysOverDue, ");
        sb2.AppendLine("Min(DueDate) Over (Partition by ApplicationID,BranchID )  DueDate,");

        sb2.AppendLine("Sum(Case When DaysOverDue >= @Bucket1_from and DaysOverDue <= @Bucket1_to Then AgingPrinciple Else 0 End)  ");
        sb2.AppendLine("     Over (Partition by ApplicationID,BranchID)  AS Bucket1_principle,");
        sb2.AppendLine("Sum(Case When DaysOverDue >= @Bucket1_from and DaysOverDue <= @Bucket1_to Then AgingGross Else 0 End)  ");
        sb2.AppendLine("     Over (Partition by ApplicationID,BranchID)  AS Bucket1_gross,");

        sb2.AppendLine("Sum(Case When DaysOverDue >= @Bucket2_from and DaysOverDue <= @Bucket2_to Then AgingPrinciple Else 0 End) ");
        sb2.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket2_principle,");
        sb2.AppendLine("Sum(Case When DaysOverDue >= @Bucket2_from and DaysOverDue <= @Bucket2_to Then AgingGross Else 0 End)  ");
        sb2.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket2_gross,");

        sb2.AppendLine("Sum(Case When DaysOverDue >= @Bucket3_from and DaysOverDue <= @Bucket3_to Then AgingPrinciple Else 0 End) ");
        sb2.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket3_principle,");
        sb2.AppendLine("Sum(Case When DaysOverDue >= @Bucket3_from and DaysOverDue <= @Bucket3_to Then AgingGross Else 0 End)");
        sb2.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket3_gross,");

        sb2.AppendLine("Sum(Case When DaysOverDue >= @Bucket4_from and DaysOverDue <= @Bucket4_to Then AgingPrinciple Else 0 End) ");
        sb2.AppendLine(" Over (Partition by ApplicationID,BranchID) AS Bucket4_principle,");
        sb2.AppendLine("Sum(Case When DaysOverDue >= @Bucket4_from and DaysOverDue <= @Bucket4_to Then AgingGross Else 0 End) ");
        sb3.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket4_gross,");
        
        sb3.AppendLine("Sum(Case When DaysOverDue >= @Bucket5_from and DaysOverDue <= @Bucket5_to Then AgingPrinciple Else 0 End) ");
        sb3.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket5_principle,");
        sb3.AppendLine("Sum(Case When DaysOverDue >= @Bucket5_from and DaysOverDue <= @Bucket5_to Then AgingGross Else 0 End) ");
        sb3.AppendLine("Over (Partition by ApplicationID,BranchID)  AS Bucket5_gross,");
        
        sb3.AppendLine("Sum(Case When DaysOverDue >= @Bucket6_from and DaysOverDue <= @Bucket6_to Then AgingPrinciple Else 0 End) ");
        sb3.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket6_principle,");
        sb3.AppendLine("Sum(Case When DaysOverDue >= @Bucket6_from and DaysOverDue <= @Bucket6_to Then AgingGross Else 0 End)");
        sb3.AppendLine("Over (Partition by ApplicationID,BranchID)  AS Bucket6_gross,");
        
        sb3.AppendLine("Sum(Case When DaysOverDue >= @Bucket7_from and DaysOverDue <= @Bucket7_to Then AgingPrinciple Else 0 End) ");
        sb3.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket7_principle,");
        sb3.AppendLine("Sum(Case When DaysOverDue >= @Bucket7_from and DaysOverDue <= @Bucket7_to Then AgingGross Else 0 End) ");
        sb3.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket7_gross,");
        
        sb3.AppendLine("Sum(Case When DaysOverDue >= @Bucket8_from and DaysOverDue <= @Bucket8_to Then AgingPrinciple Else 0 End) ");
        sb3.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket8_principle,");
        sb3.AppendLine("Sum(Case When DaysOverDue >= @Bucket8_from and DaysOverDue <= @Bucket8_to Then AgingGross Else 0 End) ");
        sb3.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket8_gross,");
        
        sb3.AppendLine("Sum(Case When DaysOverDue >= @Bucket9_from and DaysOverDue <= @Bucket9_to Then AgingPrinciple Else 0 End) ");
        sb3.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket9_principle,");
        sb3.AppendLine("Sum(Case When DaysOverDue >= @Bucket9_from and DaysOverDue <= @Bucket9_to Then AgingGross Else 0 End) ");
        sb3.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket9_gross,");
        
        sb4.AppendLine("Sum(Case When DaysOverDue >= @Bucket10_from and DaysOverDue <= @Bucket10_to Then AgingPrinciple Else 0 End) ");
        sb4.AppendLine("Over (Partition by ApplicationID,BranchID) AS Bucket10_principle,");
        sb4.AppendLine("Sum(Case When DaysOverDue >= @Bucket10_from and DaysOverDue <= @Bucket10_to Then AgingGross Else 0 End) ");
        sb4.AppendLine(" Over (Partition by ApplicationID,BranchID) AS Bucket10_gross,");
        
        sb4.AppendLine("QryAging.NumOfAsset From( ");
        sb4.AppendLine("Select A.BranchID, A.ApplicationID, DueDate,");
        sb4.AppendLine("Isnull((Case when DateDiff(dd, DueDate, @AgingDate) < 0 then 0 else DateDiff(dd, DueDate, @AgingDate) end),0) as DaysOverDue,");
        sb4.AppendLine("isnull((S.PrincipalAmount * (S.InstallmentAmount - PaidAmount - WaivedAmount)) / S.InstallmentAmount,0) AgingPrinciple,");
        sb4.AppendLine("isnull(S.InstallmentAmount - PaidAmount - WaivedAmount,0) as AgingGross, ");
        sb4.AppendLine("    ((case when totalotr <> 0 then DownPayment/TotalOTR else 0 end)*100)as PercentDP, A.ContractStatus,");
        sb4.AppendLine("isnull(QAGA.NumOfAsset,0)as NumOfAsset ");
        sb4.AppendLine("From Agreement A with (nolock) ");
        sb4.AppendLine("Left Join InstallmentSchedule S with (nolock) On");
        sb4.AppendLine("S.BranchID =  A.BranchID  and A.ApplicationID = S.ApplicationID and S.InstallmentAmount - PaidAmount - WaivedAmount > 0 ");
        sb4.AppendLine("Left Join (");
        sb4.AppendLine("Select 	AgreementAsset.BranchID, AgreementAsset.ApplicationID, Count(AssetSeqNo) ");
        sb4.AppendLine("Over (Partition by  AgreementAsset.BranchID, AgreementAsset.ApplicationID");
        sb4.AppendLine("ORDER BY AgreementAsset.BranchID, AgreementAsset.ApplicationID rows between unbounded preceding and current row ) as NumOfAsset");
        sb4.AppendLine("From 	AgreementAsset with (nolock) ");
        sb4.AppendLine("Inner Join Agreement with (nolock) ON ");
        sb4.AppendLine("Agreement.BranchID = AgreementAsset.BranchID AND Agreement.ApplicationID = AgreementAsset.ApplicationID ");
        sb4.AppendLine("Where 	Agreement.ContractStatus In ('LIV', 'ICP', 'ICL') And");
        sb4.AppendLine("AgreementAsset.AssetStatus Not in ('PRP','RRD','RLS')");
        sb4.AppendLine("And Agreement.BranchID = @BranchID  )QAGA ON A.BranchID = QAGA.BranchID AND A.ApplicationID = QAGA.ApplicationID ");
        sb4.AppendLine("Where A.ContractStatus In ('LIV', 'ICP', 'ICL') And	A.BranchID = @BranchID) as QryAging ) as QryAging1");
        #endregion 
        sb4.AppendLine (" ) QryAging2  ON QryAging2.ApplicationID =  A.ApplicationID AND QryAging2.BranchID =  A.BranchID "); 
        _ent.DailyMonthly = "D";
        using (_conn)
        {
            if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
            using (_cmd = _conn.CreateCommand())
            {
                _cmd.CommandText = sb.ToString() + sb2.ToString() + sb3.ToString() + sb4.ToString();
                _cmd.Parameters.AddWithValue("@BranchID", BranchID);
                _cmd.Parameters.AddWithValue("@Today", BusinessDate);
                _cmd.Parameters.AddWithValue("@AgingDate", BusinessDate);
                _cmd.Parameters.AddWithValue("@Bucket1_from", _ent.BucketFrom1);
                _cmd.Parameters.AddWithValue("@Bucket2_from", _ent.BucketFrom2);
                _cmd.Parameters.AddWithValue("@Bucket3_from", _ent.BucketFrom3);
                _cmd.Parameters.AddWithValue("@Bucket4_from", _ent.BucketFrom4);
                _cmd.Parameters.AddWithValue("@Bucket5_from", _ent.BucketFrom5);
                _cmd.Parameters.AddWithValue("@Bucket6_from", _ent.BucketFrom6);
                _cmd.Parameters.AddWithValue("@Bucket7_from", _ent.BucketFrom7);
                _cmd.Parameters.AddWithValue("@Bucket8_from", _ent.BucketFrom8);
                _cmd.Parameters.AddWithValue("@Bucket9_from", _ent.BucketFrom9);
                _cmd.Parameters.AddWithValue("@Bucket10_from", _ent.BucketFrom10);
                _cmd.Parameters.AddWithValue("@Bucket1_To", _ent.BucketTo1);
                _cmd.Parameters.AddWithValue("@Bucket2_To", _ent.BucketTo2);
                _cmd.Parameters.AddWithValue("@Bucket3_To", _ent.BucketTo3);
                _cmd.Parameters.AddWithValue("@Bucket4_To", _ent.BucketTo4);
                _cmd.Parameters.AddWithValue("@Bucket5_To", _ent.BucketTo5);
                _cmd.Parameters.AddWithValue("@Bucket6_To", _ent.BucketTo6);
                _cmd.Parameters.AddWithValue("@Bucket7_To", _ent.BucketTo7);
                _cmd.Parameters.AddWithValue("@Bucket8_To", _ent.BucketTo8);
                _cmd.Parameters.AddWithValue("@Bucket9_To", _ent.BucketTo9);
                _cmd.Parameters.AddWithValue("@Bucket10_To", _ent.BucketTo10);

                _rdr = _cmd.ExecuteReader();
                while (_rdr.Read())
                {
                    DataRow _row = _ent.dtAging.NewRow();
                    _row["AgingDate"] = _agingdate;
                    _row["BranchID"] = (string)_rdr["BranchID"];
                    _row["ApplicationID"] = (string)_rdr["ApplicationID"];
                    _row["DailyMonthly"] = _ent.DailyMonthly;
                    _row["ReportID"] = "1";
                    _row["AgreementNo"] = (string)_rdr["AgreementNo"];
                    _row["CustomerID"] = (string)_rdr["CustomerID"];
                    _row["DaysOverdue"] = (int)_rdr["DaysOverDue"]; ;
                    _row["CurrencyID"] = (string)_rdr["CurrencyID"];

                    _row["Bucket1_gross"] = (decimal)_rdr["Bucket1_gross"];
                    _row["Bucket2_gross"] = (decimal)_rdr["Bucket2_gross"];
                    _row["Bucket3_gross"] = (decimal)_rdr["Bucket3_gross"];
                    _row["Bucket4_gross"] = (decimal)_rdr["Bucket4_gross"];
                    _row["Bucket5_gross"] = (decimal)_rdr["Bucket5_gross"];
                    _row["Bucket6_gross"] = (decimal)_rdr["Bucket6_gross"];
                    _row["Bucket7_Gross"] = (decimal)_rdr["Bucket7_Gross"];
                    _row["Bucket8_Gross"] = (decimal)_rdr["Bucket8_Gross"];
                    _row["Bucket9_Gross"] = (decimal)_rdr["Bucket9_Gross"];
                    _row["Bucket10_Gross"] = (decimal)_rdr["Bucket10_Gross"];

                    _row["Bucket1_principle"] = (decimal)_rdr["Bucket1_principle"];
                    _row["Bucket2_principle"] = (decimal)_rdr["Bucket2_principle"];
                    _row["Bucket3_principle"] = (decimal)_rdr["Bucket3_principle"];
                    _row["Bucket4_principle"] = (decimal)_rdr["Bucket4_principle"];
                    _row["Bucket5_principle"] = (decimal)_rdr["Bucket5_principle"];
                    _row["Bucket6_principle"] = (decimal)_rdr["Bucket6_principle"];
                    _row["Bucket7_principle"] = (decimal)_rdr["Bucket7_principle"];
                    _row["Bucket8_principle"] = (decimal)_rdr["Bucket8_principle"];
                    _row["Bucket9_principle"] = (decimal)_rdr["Bucket9_principle"];
                    _row["Bucket10_principle"] = (decimal)_rdr["Bucket10_principle"];

                    _ent.BucketGross1 = (decimal)_row["Bucket1_gross"];
                    _ent.BucketGross2 = (decimal)_row["Bucket2_gross"];
                    _ent.BucketGross3 = (decimal)_row["Bucket3_gross"];
                    _ent.BucketGross4 = (decimal)_row["Bucket4_gross"];
                    _ent.BucketGross5 = (decimal)_row["Bucket5_gross"];
                    _ent.BucketGross6 = (decimal)_row["Bucket6_gross"];
                    _ent.BucketGross7 = (decimal)_row["Bucket7_gross"];
                    _ent.BucketGross8 = (decimal)_row["Bucket8_gross"];
                    _ent.BucketGross9 = (decimal)_row["Bucket9_gross"];
                    _ent.BucketGross10 = (decimal)_row["Bucket10_gross"];

                    _ent.BucketPrincipal1 = (decimal)_row["Bucket1_principle"];
                    _ent.BucketPrincipal2 = (decimal)_row["Bucket2_principle"];
                    _ent.BucketPrincipal3 = (decimal)_row["Bucket3_principle"];
                    _ent.BucketPrincipal4 = (decimal)_row["Bucket4_principle"];
                    _ent.BucketPrincipal5 = (decimal)_row["Bucket5_principle"];
                    _ent.BucketPrincipal6 = (decimal)_row["Bucket6_principle"];
                    _ent.BucketPrincipal7 = (decimal)_row["Bucket7_principle"];
                    _ent.BucketPrincipal8 = (decimal)_row["Bucket8_principle"];
                    _ent.BucketPrincipal9 = (decimal)_row["Bucket9_principle"];
                    _ent.BucketPrincipal10 = (decimal)_row["Bucket10_principle"];

                    _row["AmountOverDueGross"] = GetAmountOverDueGross(_ent);
                    _row["AmountOverDuePrinciple"] = GetAmountOverduePrinciple(_ent);

                    _row["PercentDP"] = _rdr["PercentDP"];
                    _ent.PercentDP = (decimal)_rdr["PercentDP"];
                    _row["PercentDP_text"] = GetPercentDPText(_ent);

                    _row["EffectiveRate"] = _rdr["EffectiveRate"];
                    _ent.EffectiveRate = (decimal)_rdr["EffectiveRate"];
                    _row["EffectiveRate_text"] = GetPercentEffectiveText(_ent);

                    _row["AmountFinance"] = _rdr["AmountFinance"]; // di dalam daily aging ada field amount finance
                    _ent.NTF = (decimal)_rdr["AmountFinance"];
                    _row["AmountFinance_text"] = GetPercentAmountFinanceText(_ent);

                    _row["Installmentamount"] = _rdr["Installmentamount"];
                    _ent.InstallmentAmount = (decimal)_rdr["Installmentamount"];
                    _row["AmountInstallment_text"] = GetPercentAmountInstallmentText(_ent);


                    _row["InstallmentNo"] = _rdr["NextInstallmentNumber"];
                    _row["InstallmentDate"] = _rdr["NextInstallmentDate"];

                    _row["TotalOSPrincipal"] = _rdr["TotalOSPrincipal"];
                    _row["TotalOSInterest"] = _rdr["TotalOSInterest"];
                    _row["TotalOSPrincipalUndue"] = _rdr["TotalOSPrincipalUndue"];
                    _row["TotalOSInterestUndue"] = _rdr["TotalOSInterestUndue"];
                    _row["ContractPrepaidAmount"] = _rdr["ContractPrepaidAmount"];
                    _row["LCInstallment"] = _rdr["LCInstallment"];
                    _row["LCInstallmentPaid"] = _rdr["LCInstallmentPaid"];
                    _row["LCInstallmentWaived"] = _rdr["LCInstallmentWaived"];
                    _row["LastLCInstallmentCalcDate"] = _rdr["LastLCInstallmentCalcDate"];
                    _row["LCInstallmentCurrent"] = _rdr["LCInstallmentCurrent"];
                    _row["LCInsurance"] = _rdr["LCInsurance"];
                    _row["LCInsurancePaid"] = _rdr["LCInsurancePaid"];
                    _row["LCInsuranceWaived"] = _rdr["LCInsuranceWaived"];
                    _row["LastLCInsuranceCalcDate"] = _rdr["LastLCInsuranceCalcDate"];
                    _row["InstallmentDue"] = _rdr["InstallmentDue"];
                    _row["InstallmentDuePaid"] = _rdr["InstallmentDuePaid"];
                    _row["InstallmentDueWaived"] = _rdr["InstallmentDueWaived"];
                    _row["InsuranceDue"] = _rdr["InsuranceDue"];
                    _row["InsuranceDuePaid"] = _rdr["InsuranceDuePaid"];
                    _row["InsuranceDueWaived"] = _rdr["InsuranceDueWaived"];
                    _row["PDCBounceFee"] = _rdr["PDCBounceFee"];
                    _row["PDCBounceFeePaid"] = _rdr["PDCBounceFeePaid"];
                    _row["PDCBounceFeeWaived"] = _rdr["PDCBounceFeeWaived"];
                    _row["CollectionExpense"] = _rdr["CollectionExpense"];
                    _row["CollectionExpensePaid"] = _rdr["CollectionExpensePaid"];
                    _row["CollectionExpenseWaived"] = _rdr["CollectionExpenseWaived"];
                    _row["NADate"] = _rdr["NADate"];
                    _row["NAAmount"] = _rdr["NAAmount"];
                    _row["NAPaid"] = _rdr["NAPaid"];
                    _row["NAWaived"] = _rdr["NAWaived"];
                    _row["WODate"] = _rdr["WODate"];
                    _row["WOAmount"] = _rdr["WOAmount"];
                    _row["WOPaid"] = _rdr["WOPaid"];
                    _row["WOWaived"] = _rdr["WOWaived"];
                    _row["ContractStatus"] = _rdr["ContractStatus"];
                    _row["DefaultStatus"] = _rdr["DefaultStatus"];
                    _row["NumOfAssetUnit"] = _rdr["NumOfAssetUnit"];

                    _row["NextInstallmentDueNumber"] = _rdr["NextInstallmentDueNumber"];
                    
                    _row["NextInstallmentDueDate"] = _rdr["NextInstallmentDueDate"];
                    
                    _row["AOID"] = _rdr["AOID"];
                    _row["SurveyorID"] = _rdr["SurveyorID"];
                    _row["CAID"] = _rdr["CAID"];
                    _row["IsFPD"] = _rdr["IsFPD"];
                    _row["ProductID"] = _rdr["ProductID"];
                    _row["ProductOfferingID"] = _rdr["ProductOfferingID"];
                    _row["NTF"] = _rdr["AmountFinance"];
                    _row["PickUpFee"] = _rdr["PickUpFee"];
                    _row["VisitFee"] = _rdr["VisitFee"];
                    _row["PickUpFeeCharges"] = _rdr["PickUpFeeCharges"];
                    _row["PickUpFeePaid"] = _rdr["PickUpFeePaid"];
                    _row["PickUpFeeWaived"] = _rdr["PickUpFeeWaived"];
                    _row["VisitFeeCharges"] = _rdr["VisitFeeCharges"];
                    _row["VisitFeePaid"] = _rdr["VisitFeePaid"];
                    _row["VisitFeeWaived"] = _rdr["VisitFeeWaived"];
                    _row["Distance_ID"] = _rdr["Distance_ID"];
                    _row["Distance_Desc"] = _rdr["Distance_Desc"];
                    _ent.dtAging.Rows.Add(_row);
                }
            }
            _rdr.Close();
            _ent.dtAging.AcceptChanges();
        }
        sb.Clear();
        sb2.Clear();
        sb3.Clear();
        sb4.Clear();
        _cmd.Dispose();
        if (_conn.State == ConnectionState.Open) { _conn.Close(); }
        _conn.Dispose();
        
        //_common.SendDataTable(_ent.dtAging);
        DailyAgingSave(_ent);
    }
       
}

    public partial class StoredProcedures
    {
        [Microsoft.SqlServer.Server.SqlProcedure]
        public static void EODDailyAgingProc(String BranchID, DateTime BusinessDate)
        {
            DailyAging _proc = new DailyAging();
            _proc.DailyAgingProc(BranchID, BusinessDate);

        }
    }




        //if (_ent.dtAging.Rows.Count > 0 && _ent.dtAging != null)
        //{
        //    foreach (DataRow dr in _ent.dtAging.Rows)
        //    {
        //        dr["AgingDate"] = (DateTime)BusinessDate;
        //        dr["DailyMonthly"] = "D";
        //        dr["ReportID"] = "1";

        //        dr["DaysOverDue"] = "";
        //        dr["AmountOverDueGross"] = "";
        //        dr["AmountOverDuePrinciple"] = "";
        //        dr["BucketGross1"] = "";
        //        dr["BucketGross2"] = "";
        //        dr["BucketGross3"] = "";
        //        dr["BucketGross4"] = "";
        //        dr["BucketGross5"] = "";
        //        dr["BucketGross6"] = "";
        //        dr["BucketGross7"] = "";
        //        dr["BucketGross8"] = "";
        //        dr["BucketGross9"] = "";
        //        dr["BucketGross10"] = "";

        //        dr["BucketPrinciple1"] = "";
        //        dr["BucketPrinciple2"] = "";
        //        dr["BucketPrinciple3"] = "";
        //        dr["BucketPrinciple4"] = "";
        //        dr["BucketPrinciple5"] = "";
        //        dr["BucketPrinciple6"] = "";
        //        dr["BucketPrinciple7"] = "";
        //        dr["BucketPrinciple8"] = "";
        //        dr["BucketPrinciple9"] = "";
        //        dr["BucketPrinciple10"] = "";

        //        dr["PercentDPText"] = "";
        //        dr["EffectiveRateText"] = "";
        //        dr["AmountInstallment"] = "";
        //        dr["AmountInstallmentText"] = "";

        //        dr["AmountOverDueGross"] = "";
        //        dr["AmountOverDuePrinciple"] = "";
        //    }
        //}