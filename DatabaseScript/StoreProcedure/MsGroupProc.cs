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
    public static void MsGroupPaging(Int32 currentpage,
                    Int32 pagesize,
                    String wherecond,
                    String sortby)
    {
        // Put your code here

        StringBuilder sb = new StringBuilder();
        int firstrec, lastrec;
        firstrec = (currentpage - 1) * pagesize + 1;
        lastrec = (currentpage * pagesize + 1) - 1;


        sb.Append("Select * From (");
        sb.Append("Select ");
        sb.Append("ROW_NUMBER() OVER (Order By ");
        #region "Sort"

        if (sortby != "")
        { sb.Append(sortby); }
        else { sb.Append("GroupName"); }

        sb.Append(") as number, ");

        #endregion
        sb.Append("ID, GroupName From Ms_Form with (nolock)");

        #region "Where condition"
        if (wherecond != "")
        {
            sb.Append(wherecond);

        }
        #endregion

        #region "Paging record"
        sb.Append(")qry1 where number between ");
        sb.Append(firstrec.ToString());
        sb.Append(" and ");
        sb.Append(lastrec.ToString());
        #endregion

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

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void MsGroupAdd(String GroupName, String Active, String UsrCrt)
    {
        // Put your code here
        StringBuilder sb = new StringBuilder();
        sb.Append("Insert Into MS_Group (GroupName, Active, UsrCrt, DtmCrt) values (@GroupName, @Active, @UsrCrt, @DtmCrt)");
        string _statement = sb.ToString();
        using (SqlConnection _conn = new SqlConnection("context connection=true"))
        {
            if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
            SqlTransaction _trans;
            _trans = _conn.BeginTransaction();
            using (SqlCommand cmd = _conn.CreateCommand())
            {
                try
                {
                    cmd.Transaction = _trans;
                    cmd.CommandText = _statement;

                    cmd.Parameters.AddWithValue("@GroupName", GroupName);
                    cmd.Parameters.AddWithValue("@Active", Active);
                    cmd.Parameters.AddWithValue("@UsrCrt", UsrCrt);
                    cmd.Parameters.AddWithValue("@DtmCrt", DateTime.Now.ToString());
                    cmd.ExecuteNonQuery();
                    _trans.Commit();
                }
                catch (Exception exp)
                {
                    _trans.Rollback();
                    throw new Exception(exp.Message);
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open) { _conn.Close(); }
                }

            }
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void MsGroupEdit(int ID, String GroupName, String Active, String UsrUpd)
    {
        // Put your code here
        StringBuilder sb = new StringBuilder();
        sb.Append("Update MS_Group Set GroupName = @GroupName, Active=@Active, UsrUpd = @UsrUpd, DtmUpd = @DtmUpd where ID=@ID");
        string _statement = sb.ToString();

        SqlConnection _conn = new SqlConnection("context connection=true");
        SqlTransaction _trans;

        using (_conn)
        {
            if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
            _trans = _conn.BeginTransaction();
            using (SqlCommand cmd = _conn.CreateCommand())
            {
                try
                {

                    cmd.CommandText = _statement;
                    cmd.Transaction = _trans;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@GroupName", GroupName);
                    cmd.Parameters.AddWithValue("@Active", Active);
                    cmd.Parameters.AddWithValue("@UsrUpd", UsrUpd);
                    cmd.Parameters.AddWithValue("@DtmUpd", DateTime.Now.ToString());
                    cmd.ExecuteNonQuery();
                    _trans.Commit();
                }
                catch (Exception exp)
                {
                    _trans.Rollback();
                    throw new Exception(exp.Message);
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open) { _conn.Close(); }
                }

            }
        }
    }


    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void MsGroupDelete(int ID, String GroupName, String Active, String UsrUpd)
    {
        // Put your code here
        // Put your code here
        StringBuilder sb = new StringBuilder();
        sb.Append("Delete From MS_Group where ID=@ID");
        string _statement = sb.ToString();

        SqlConnection _conn = new SqlConnection("context connection=true");
        SqlTransaction _trans;
        sb.Clear();
        sb.Append("Select 1 from Ms_");
        _statement = sb.ToString();

        using (_conn)
        {
            if (_conn.State == ConnectionState.Closed) { _conn.Open(); }
            _trans = _conn.BeginTransaction();
            using (SqlCommand cmd = _conn.CreateCommand())
            {
                try
                {
                    cmd.CommandText = _statement;
                    cmd.Transaction = _trans;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                    _trans.Commit();
                }
                catch (Exception exp)
                {
                    _trans.Rollback();
                    throw new Exception(exp.Message);
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open) { _conn.Close(); }
                }
            }
        }
    }

}