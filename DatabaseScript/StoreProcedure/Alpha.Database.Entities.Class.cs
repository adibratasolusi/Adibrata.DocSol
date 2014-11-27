//------------------------------------------------------------------------------
// <copyright file="CSSqlCodeFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System; 
namespace Adibrata.Database.Script.StoreProcedure
{
    [Serializable]    
    public class AgreementEntities
    {
        public string DefaultStatus{get;set;}
        public string ContractStatus { get; set; }
        public string PrepaidHolStatus { get; set; }
        public int CurrencyID { get; set; }
        public long ProductID { get; set; }
        public int CompanyID { get; set; }
        public string UsrCrt { get; set; }
        public string DtmCrt { get; set; }
        public string DtmUpd { get; set; }
        public string UsrUpd { get; set; }
    }
    [Serializable]    
    public class BankAccount
    {
        public int BankMasterID{ get; set; }
        public int BankAccountID { get; set; }
        public decimal EndingBalance { get; set; }
        public decimal AmountTransaction { get; set; }
        public string UsrCrt { get; set; }
        public string DtmCrt { get; set; }
        public string DtmUpd { get; set; }
        public string UsrUpd { get; set; }
    }
    [Serializable]    
    public class Cashier
    {
        public int CashierID{ get; set; }
        public int OpeningSequence { get; set; }
        public decimal OpeningAmount{ get; set; }
        public decimal ClosingAmount{ get; set; }
        public string DtmUpd { get; set; }
        public string UsrUpd { get; set; }
    }


    [Serializable]
    public class CustomerEntities
    {
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string CustAddressComplete { get; set; }
        public string CustAddress { get; set; }
        public string CustRT { get; set; }
        public string CustRW {get; set; }
        public string CustKelurahan { get; set; }
        public string CustKecamatan { get; set; }
        public string CustZipCode { get; set; }
        public string CustAreaPhone { get; set; }
        public string CustNoPhone { get; set; }
        public string CustAreaFax { get; set; }
        public string CustnoFax { get; set; }
        public string UsrCrt { get; set; }
        public string DtmCrt { get; set; }
        public string DtmUpd { get; set; }
        public string UsrUpd { get; set; }
    }

    [Serializable]
    public class DailyAgingEntities
    {
        public decimal BucketPrincipal1 { get; set; }
        public decimal BucketPrincipal2 { get; set; }
        public decimal BucketPrincipal3 { get; set; }
        public decimal BucketPrincipal4 { get; set; }
        public decimal BucketPrincipal5 { get; set; }
        public decimal BucketPrincipal6 { get; set; }
        public decimal BucketPrincipal7 { get; set; }
        public decimal BucketPrincipal8 { get; set; }
        public decimal BucketPrincipal9 { get; set; }
        public decimal BucketPrincipal10 { get; set; }

        public decimal BucketGross1 { get; set; }
        public decimal BucketGross2 { get; set; }
        public decimal BucketGross3 { get; set; }
        public decimal BucketGross4 { get; set; }
        public decimal BucketGross5 { get; set; }
        public decimal BucketGross6 { get; set; }
        public decimal BucketGross7 { get; set; }
        public decimal BucketGross8 { get; set; }
        public decimal BucketGross9 { get; set; }
        public decimal BucketGross10 { get; set; }


        public int BucketFrom1 { get; set; }
        public int BucketFrom2 { get; set; }
        public int BucketFrom3 { get; set; }
        public int BucketFrom4 { get; set; }
        public int BucketFrom5 { get; set; }
        public int BucketFrom6 { get; set; }
        public int BucketFrom7 { get; set; }
        public int BucketFrom8 { get; set; }
        public int BucketFrom9 { get; set; }
        public int BucketFrom10 { get; set; }

        public int BucketTo1 { get; set; }
        public int BucketTo2 { get; set; }
        public int BucketTo3 { get; set; }
        public int BucketTo4 { get; set; }
        public int BucketTo5 { get; set; }
        public int BucketTo6 { get; set; }
        public int BucketTo7 { get; set; }
        public int BucketTo8 { get; set; }
        public int BucketTo9 { get; set; }
        public int BucketTo10 { get; set; }

        public string  BucketText1 { get; set; }
        public string BucketText2 { get; set; }
        public string BucketText3 { get; set; }
        public string BucketText4 { get; set; }
        public string BucketText5 { get; set; }
        public string BucketText6 { get; set; }
        public string BucketText7 { get; set; }
        public string BucketText8 { get; set; }
        public string BucketText9 { get; set; }
        public string BucketText10 { get; set; }

        public string PercentDPText1 { get; set; }
        public string PercentDPText2 { get; set; }
        public string PercentDPText3 { get; set; }
        public string PercentDPText4 { get; set; }
        public string PercentDPText5 { get; set; }
        public string PercentDPText6 { get; set; }
        public string PercentDPText7 { get; set; }
        public string PercentDPText8 { get; set; }
        public string PercentDPText9 { get; set; }
        public string PercentDPText10 { get; set; }

        public decimal PercentDPFrom1 { get; set; }
        public decimal PercentDPFrom2 { get; set; }
        public decimal PercentDPFrom3 { get; set; }
        public decimal PercentDPFrom4 { get; set; }
        public decimal PercentDPFrom5 { get; set; }
        public decimal PercentDPFrom6 { get; set; }
        public decimal PercentDPFrom7 { get; set; }
        public decimal PercentDPFrom8 { get; set; }
        public decimal PercentDPFrom9 { get; set; }
        public decimal PercentDPFrom10 { get; set; }

        public decimal PercentDPTo1 { get; set; }
        public decimal PercentDPTo2 { get; set; }
        public decimal PercentDPTo3 { get; set; }
        public decimal PercentDPTo4 { get; set; }
        public decimal PercentDPTo5 { get; set; }
        public decimal PercentDPTo6 { get; set; }
        public decimal PercentDPTo7 { get; set; }
        public decimal PercentDPTo8 { get; set; }
        public decimal PercentDPTo9 { get; set; }
        public decimal PercentDPTo10 { get; set; }

        public decimal PercentEffectiveFrom1 { get; set; }
        public decimal PercentEffectiveFrom2 { get; set; }
        public decimal PercentEffectiveFrom3 { get; set; }
        public decimal PercentEffectiveFrom4 { get; set; }
        public decimal PercentEffectiveFrom5 { get; set; }
        public decimal PercentEffectiveFrom6 { get; set; }
        public decimal PercentEffectiveFrom7 { get; set; }
        public decimal PercentEffectiveFrom8 { get; set; }
        public decimal PercentEffectiveFrom9 { get; set; }
        public decimal PercentEffectiveFrom10 { get; set; }

        public decimal PercentEffectiveTo1 { get; set; }
        public decimal PercentEffectiveTo2 { get; set; }
        public decimal PercentEffectiveTo3 { get; set; }
        public decimal PercentEffectiveTo4 { get; set; }
        public decimal PercentEffectiveTo5 { get; set; }
        public decimal PercentEffectiveTo6 { get; set; }
        public decimal PercentEffectiveTo7 { get; set; }
        public decimal PercentEffectiveTo8 { get; set; }
        public decimal PercentEffectiveTo9 { get; set; }
        public decimal PercentEffectiveTo10 { get; set; }

        public string PercentEffectiveText1 { get; set; }
        public string PercentEffectiveText2 { get; set; }
        public string PercentEffectiveText3 { get; set; }
        public string PercentEffectiveText4 { get; set; }
        public string PercentEffectiveText5 { get; set; }
        public string PercentEffectiveText6 { get; set; }
        public string PercentEffectiveText7 { get; set; }
        public string PercentEffectiveText8 { get; set; }
        public string PercentEffectiveText9 { get; set; }
        public string PercentEffectiveText10 { get; set; }

        public decimal AmountFinanceFrom1 { get; set; }
        public decimal AmountFinanceFrom2 { get; set; }
        public decimal AmountFinanceFrom3 { get; set; }
        public decimal AmountFinanceFrom4 { get; set; }
        public decimal AmountFinanceFrom5 { get; set; }
        public decimal AmountFinanceFrom6 { get; set; }
        public decimal AmountFinanceFrom7 { get; set; }
        public decimal AmountFinanceFrom8 { get; set; }
        public decimal AmountFinanceFrom9 { get; set; }
        public decimal AmountFinanceFrom10 { get; set; }


        public decimal AmountFinanceTo1 { get; set; }
        public decimal AmountFinanceTo2 { get; set; }
        public decimal AmountFinanceTo3 { get; set; }
        public decimal AmountFinanceTo4 { get; set; }
        public decimal AmountFinanceTo5 { get; set; }
        public decimal AmountFinanceTo6 { get; set; }
        public decimal AmountFinanceTo7 { get; set; }
        public decimal AmountFinanceTo8 { get; set; }
        public decimal AmountFinanceTo9 { get; set; }
        public decimal AmountFinanceTo10 { get; set; }

        public string AmountFinanceText1 { get; set; }
        public string AmountFinanceText2 { get; set; }
        public string AmountFinanceText3 { get; set; }
        public string AmountFinanceText4 { get; set; }
        public string AmountFinanceText5 { get; set; }
        public string AmountFinanceText6 { get; set; }
        public string AmountFinanceText7 { get; set; }
        public string AmountFinanceText8 { get; set; }
        public string AmountFinanceText9 { get; set; }
        public string AmountFinanceText10 { get; set; }

        public decimal AmountInstallmentFrom1 { get; set; }
        public decimal AmountInstallmentFrom2 { get; set; }
        public decimal AmountInstallmentFrom3 { get; set; }
        public decimal AmountInstallmentFrom4 { get; set; }
        public decimal AmountInstallmentFrom5 { get; set; }
        public decimal AmountInstallmentFrom6 { get; set; }
        public decimal AmountInstallmentFrom7 { get; set; }
        public decimal AmountInstallmentFrom8 { get; set; }
        public decimal AmountInstallmentFrom9 { get; set; }
        public decimal AmountInstallmentFrom10 { get; set; }


        public decimal AmountInstallmentTo1 { get; set; }
        public decimal AmountInstallmentTo2 { get; set; }
        public decimal AmountInstallmentTo3 { get; set; }
        public decimal AmountInstallmentTo4 { get; set; }
        public decimal AmountInstallmentTo5 { get; set; }
        public decimal AmountInstallmentTo6 { get; set; }
        public decimal AmountInstallmentTo7 { get; set; }
        public decimal AmountInstallmentTo8 { get; set; }
        public decimal AmountInstallmentTo9 { get; set; }
        public decimal AmountInstallmentTo10 { get; set; }

        public string AmountInstallmentText1 { get; set; }
        public string AmountInstallmentText2 { get; set; }
        public string AmountInstallmentText3 { get; set; }
        public string AmountInstallmentText4 { get; set; }
        public string AmountInstallmentText5 { get; set; }
        public string AmountInstallmentText6 { get; set; }
        public string AmountInstallmentText7 { get; set; }
        public string AmountInstallmentText8 { get; set; }
        public string AmountInstallmentText9 { get; set; }
        public string AmountInstallmentText10 { get; set; }

       
        public decimal EffectiveRate { get; set; }
        public decimal NTF { get; set;}
        public decimal InstallmentAmount {get; set;}
        public decimal PercentDP { get; set; }

        public DataTable dtAging { get; set; }

        public string DailyMonthly { get; set; }
        public string UsrCrt { get; set; }
        public string DtmCrt { get; set; }
        public string DtmUpd { get; set; }
        public string UsrUpd { get; set; }
    }

    [Serializable]    
    public class JournalEntities
    {
        public string PostingDate { get; set; }
        public string ValueDate { get; set; }

        public long AgrmntID { get; set; }
        public int OfficeID { get; set; }

        public int OfficeID_X { get; set; }
        public string JournalCodeNumber {get;set;}
        public string PeriodYear {get;set;}
        public string PeriodMonth {get;set;}
        
        public string RefferenceNo {get;set;}
        
        public string JorunalHeaderDescription {get;set;}
        public string CashBankDescription { get; set; }
        public string BatchID {get;set;}
        public string Status_Tr {get;set;}
        public string IsActive {get;set;}
        public string IsValid {get;set;}
        
        public string TransactionCode {get;set;}
        public int JournalSchemeHeader {get;set;}
        public decimal AmountTransaction {get;set;}
        public string JobId {get;set;}
     
        public int BankAccountID {get;set;}
        public int CurrencyID {get;set;}
        public decimal CurrencyRate {get;set;}
        public int CashierID {get;set;}

        public string VoucherNo {get;set;}
        public int OpeningCloseCashier {get;set;}
        public string ReceiveDisburseFlag { get; set; }

        public string WayOfPayment { get; set; }
        public string ReceiveFrom { get; set; }
        public string ReceiptNo { get; set; }
        public string PaymentHistoryDescription { get; set; }
        
        public DataTable dtTempJournalDetail { get; set; }
        public DataTable dtTempJob { get; set; }
        public DataTable dtJournalDetail { get; set; }
        public int HistorySequenceNo { get; set; }

        public string JournalSequencecode { get; set; }

        public string JournalFlags { get; set; }

        public int JournalTransactionID { get; set; }
        public string Notes { get; set; }
        public long JournalHeaderID { get; set; }
        public long CashBankMutationHeaderID { get; set; }
        public int PaymentHistorySequenceNoHeader { get; set; }
        public long ProductID { get; set; }
        public int CoaSchemeID { get; set; }

        public string DescriptionJournalDetail { get; set; }
        public string CoaName { get; set; }
        public string CoaDescription  { get; set; }
        public string CoaSourceTable { get;set;}
        public string CoaCode { get; set; }

        public string CoaCode_X { get; set; }
        public string DepartmentID { get; set;  }
        public decimal AmountJournal { get; set; }
        public int Counter { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public string ColSourceDetail { get; set; }
        public string TableSourceDetail { get; set; }
        public string FilterSourceDetail { get; set; }
        public string TransactionID { get; set;  }
        public string IsCreatePaymentHistoryDetail { get; set; }
        public string IsCreateCashBankTransactionDetail { get; set; }
        public string IsCreateJournalDetail { get; set; }
        public string Post { get; set; }
        public string CoaOffice { get; set; }
        public decimal AmountDetail { get; set;  }
        public int CoyID { get; set; }

        public string UsrCrt { get; set; }
        public string DtmCrt { get; set; }
        public string DtmUpd { get; set; }
        public string UsrUpd { get; set; }
    }

}
 