//------------------------------------------------------------------------------
// <copyright file="CSSqlStoredProcedure.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using Adibrata.Database.Script.StoreProcedure;



public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void PostingPayment ()
    {
        TablePosting oTable = new TablePosting();
        //AgreementStatus oStatus = new AgreementStatus();
        DataTable tblPayment = new DataTable();
        tblPayment = oTable.StructureTablePosting();   

        DataRow _row = tblPayment.NewRow();
        _row["SequenceNo"] = "1";
        _row["CoaName"] = "Installment";
        _row["Amount"] = "Installment";
        _row["RefDesc"] = "Installment";
        _row["DepartmentID"] = "Installment";
        _row["VoucherDesc"] = "Installment";

        tblPayment.Rows.Add (_row);
        //MasterSequenceClass _seqno = new MasterSequenceClass();
        SqlContext.Pipe.Send("sebelum masuk");
        

        //SqlConnection connection = new SqlConnection("context connection=true");
        

        //using (connection)
        //{
            //connection.Open();
            //using (SqlCommand cmd = connection.CreateCommand())
            //{
               //foreach (DataRow row in tblPayment.Rows) 
               //{
               //    foreach (DataColumn col in tblPayment.Columns)
               //    {
               //    //    test = row[col].ToString();
               //        SqlContext.Pipe.Send(test);
               //    }
               //}
                
            //}

        //}
        // Put your code here
    }
}
