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
using System.Text;


public partial class StoredProcedures
{
   

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void OfficePaging 
                (   Int32 currentpage, 
                    Int32 pagesize, 
                    String wherecond, 
                    String sortby
                )
    {
        // Put your code here
        StringBuilder sb = new StringBuilder();
        int firstrec, lastrec;
        firstrec = (currentpage -1) * pagesize +1;
        lastrec = (currentpage * pagesize + 1) - 1;


        sb.Append("Select * From (");
        sb.Append ("Select ");
        sb.Append ("ROW_NUMBER() OVER (Order By ");
        #region "Sort"

        if (sortby != "")
        { sb.Append(sortby); }
        else { sb.Append("OfficeID"); }
        
        sb.Append (") as number, ");

        #endregion 
        sb.Append ("OfficeID, OfficeFullName From Office with (nolock)");

        #region "Where condition"
        if (wherecond != "")
        {
            sb.Append(wherecond);

        }
#endregion 

#region "Paging record"
        sb.Append (")qry1 where number between ");
        sb.Append (firstrec.ToString());
        sb.Append (" and ");
        sb.Append (lastrec.ToString());
#endregion 

        SqlConnection connection = new SqlConnection("context connection=true");
        string statement = sb.ToString();
        
        using (connection)
        {
            connection.Open();
            using (SqlCommand cmd = connection.CreateCommand()) 
            { 
                cmd.CommandText =statement;
                SqlContext.Pipe.ExecuteAndSend(cmd);
            }
        }
    }

    public static void OfficeAdd()
    {
        // Put your code here
        StringBuilder sb = new StringBuilder();
        
        sb.Append("Select * From MS_Sequence with (nolock)");

        SqlConnection connection = new SqlConnection("context connection=true");
        string statement = sb.ToString();
        
        using (connection)
        {
            connection.Open();
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = statement;
                SqlContext.Pipe.ExecuteAndSend(cmd);
            }
        }
    }

    public static void OfficeDelete()
    {
        // Put your code here
        SqlContext.Pipe.Send("Office Delete!\n");

    }

    public static void OfficeEdit()
    {
        // Put your code here
        SqlContext.Pipe.Send("Office Edit!\n");

    }



}
