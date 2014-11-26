using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using Adibrata.Database.Script.StoreProcedure;


public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SequenceNoProc (int OfficeID, string SequenceId, DateTime PostingDate, out string SequenceNo)
    {
        MasterSequenceClass _proc = new MasterSequenceClass();
        SequenceNo = _proc.GetSequenceNo(OfficeID, SequenceId, PostingDate.ToString());
        // Put your code here
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void JournalNoProc(int OfficeID, int JournalTransactionID, DateTime PostingDate, out string SequenceNo)
    {
        MasterSequenceClass _proc = new MasterSequenceClass();
        SequenceNo = _proc.GetJournalNo(OfficeID, JournalTransactionID, PostingDate.ToString());
        // Put your code here
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void VoucherNoProc(int BankAccountID, DateTime PostingDate, out string SequenceNo)
    {
        MasterSequenceClass _proc = new MasterSequenceClass();
        SequenceNo = _proc.GetVoucherNo(BankAccountID, PostingDate.ToString());
        // Put your code here
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void PaymentHistoryNoProc(long AgrmntID, int paymenthistoryno)
    {
        MasterSequenceClass _proc = new MasterSequenceClass();
        paymenthistoryno = _proc.GetMaxHistorySequenceNo(AgrmntID);
        // Put your code here
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void AgreementNoProc(string OfficeID, string ProductID, out string AgreementNo)
    {
        MasterSequenceClass _proc = new MasterSequenceClass();
        AgreementNo = _proc.GetAgreementNo(OfficeID, ProductID);
        // Put your code here
    }
}
