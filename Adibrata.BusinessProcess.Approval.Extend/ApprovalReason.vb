#Region "Revision History"
'$Header: /Finansia/AdIns.Application.Eloan.DataAccess.Approval/ApprovalReason.vb 16    25/06/08 09:43a Amelya $
'$Workfile: ApprovalReason.vb $
'-----------------------------------------------------------
'$Log: /Finansia/AdIns.Application.Eloan.DataAccess.Approval/ApprovalReason.vb $

'-----------------------------------------------------------
#End Region
#Region "Imports"
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Text
Imports AdIns.Framework.DataAccess
Imports System.Data.SqlClient
Imports AdIns.Application.Eloan.Business
Imports AdIns.Application.Eloan
Imports AdIns.Application.Eloan.Exceptions
#End Region
Public Class ApprovalReason : Inherits AdIns.Application.Eloan.DataAccess.DataAccessBase
    Public Function GetApprovalType(ByVal customclass As Entities.ApprovalReason) As Entities.ApprovalReason
        Dim oReturnValue As New Entities.ApprovalReason
        Dim params(0) As SqlParameter
        params(0) = New SqlParameter("@Table", SqlDbType.VarChar, 50)
        params(0).Value = customclass.tableName
        Try
            oReturnValue.ApprovalHMListData = SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, "spGetApprovalTypeFillCbo", params).Tables(0)
            Return oReturnValue
        Catch exp As Exception
            WriteException("Approval", "GetApprovalType", exp.Message)
            Throw New Exception("Error On DataAccess.AccAcq.Customer.CustomerPersonalAdd")
        End Try
    End Function

    Public Function ApprovalRSADD(ByVal customclass As Entities.ApprovalReason) As Entities.ApprovalReason
        Dim objcon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction

        Dim params() As SqlParameter = New SqlParameter(5) {}
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        objtrans = objcon.BeginTransaction
        Try
            params(0) = New SqlParameter("@ApprovalTypeID", SqlDbType.VarChar, 4)
            params(0).Value = customclass.ApprovalID

            params(1) = New SqlParameter("@ReasonID", SqlDbType.VarChar, 10)
            params(1).Value = customclass.ReasonID

            params(2) = New SqlParameter("@Description", SqlDbType.VarChar, 100)
            params(2).Value = customclass.Description

            params(3) = New SqlParameter("@Type", SqlDbType.Char, 3)
            params(3).Value = customclass.AprovalReasonType

            params(4) = New SqlParameter("@isActive", SqlDbType.VarChar, 1)
            params(4).Value = customclass.ApprovalRSIsActive

            params(5) = New SqlParameter("@Err", SqlDbType.VarChar, 100)
            params(5).Direction = ParameterDirection.Output

            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, "spApprovalReasonAdd", params)
            customclass.Err = CStr(params(5).Value)
            objtrans.Commit()
            Return customclass
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("Approval", "ApprovalReasonAdd", exp.Message)
            Throw New Exception(exp.Message)
        End Try
    End Function

    Public Sub ApprovalRSEdit(ByVal customclass As Entities.ApprovalReason)
        Dim objcon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction

        Dim params() As SqlParameter = New SqlParameter(4) {}
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        objtrans = objcon.BeginTransaction
        Try
            params(0) = New SqlParameter("@ApprovalTypeID", SqlDbType.VarChar, 4)
            params(0).Value = customclass.ApprovalID

            params(1) = New SqlParameter("@ReasonID", SqlDbType.VarChar, 10)
            params(1).Value = customclass.ReasonID

            params(2) = New SqlParameter("@Description", SqlDbType.VarChar, 100)
            params(2).Value = customclass.Description

            params(3) = New SqlParameter("@Type", SqlDbType.Char, 3)
            params(3).Value = customclass.AprovalReasonType

            params(4) = New SqlParameter("@isActive", SqlDbType.VarChar, 1)
            params(4).Value = customclass.ApprovalRSIsActive

            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, "spApprovalReasonEdit", params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("Approval", "ApprovalReasonEdit", exp.Message)
            Throw New Exception(exp.Message)

        Finally
            If objcon.State = ConnectionState.Open Then objcon.Close()
            objcon.Dispose()
        End Try

    End Sub
End Class
