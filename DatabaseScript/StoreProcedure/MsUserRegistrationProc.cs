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
    public static void MsUserRegistrationPaging(
                    Int32 currentpage,
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
        else { sb.Append("UserName"); }

        sb.Append(") as number, ");

        #endregion
        sb.Append("ID, UserName, FullName, ExpiredDate From Ms_User with (nolock)");

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
    public static void MsUserRegistrationAdd(String UserName, 
                                            String Password, 
                                            String EmployeeID, 
                                            String FullName, 
                                            String Active, 
                                            String ExpiredDate, 
                                            String UsrCrt
                                            )
    {
        // Put your code here
        StringBuilder sb = new StringBuilder();
        sb.Append("Insert Into Ms_User (UserName, Password, EmployeeID, FullName, Active, ExpiredDate, UsrCrt, DtmCrt) ");
        sb.Append (" values (@UserName, @Password, @EmployeeID, @FullName, @Active, @ExpiredDate, @UsrCrt, @DtmCrt)");

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
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    cmd.Parameters.AddWithValue("@FullName", FullName);
                    cmd.Parameters.AddWithValue("@Active", Active);
                    cmd.Parameters.AddWithValue("@ExpiredDate", ExpiredDate);
                    cmd.Parameters.AddWithValue("@UsrCrt", UsrCrt);
                    cmd.Parameters.AddWithValue("@DtmCrt", DateTime.Now.ToString());
                    cmd.ExecuteNonQuery();
                    _trans.Commit();
                }
                catch (Exception exp)
                {
                    _trans.Rollback();
                    throw new Exception (exp.Message);
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open) { _conn.Close(); }
                    cmd.Dispose();
                    _conn.Dispose();
                }

            }
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void MsUserRegistrationEdit(
                                            Int32 ID, 
                                            String Password, 
                                            String EmployeeID,
                                            String FullName,
                                            String Active,
                                            String ExpiredDate,
                                            String UsrCrt)
    {
        // Put your code here
        StringBuilder sb = new StringBuilder();
        sb.Append("Update Ms_User  Set Password = @Password, EmployeeID = @EmployeeID, FullName = @FullName, Active = @Active, ExpiredDate = @ExpiredDate, "); 
        sb.Append (                 "UsrUpd = @UsrUpd, DtmUpd = @DtmUpd");
        sb.Append (" Where ID = @ID") ;
        

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
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    cmd.Parameters.AddWithValue("@FullName", FullName);
                    cmd.Parameters.AddWithValue("@Active", Active);
                    cmd.Parameters.AddWithValue("@ExpiredDate", ExpiredDate);
                    cmd.Parameters.AddWithValue("@UsrUpd", UsrCrt);
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
    public static void MsUserRegistrationDelete(Int32 ID)
    {
        // Put your code here
        StringBuilder sb = new StringBuilder();
        sb.Append("Delete From Ms_User"); 
        sb.Append(" Where ID = @ID");


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
