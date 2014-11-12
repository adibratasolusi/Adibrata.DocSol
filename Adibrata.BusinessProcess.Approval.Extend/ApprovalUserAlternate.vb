#Region "Revision History"
'$Header: /Clipan/AdIns.Application.Eloan.DataAccess.Approval/ApprovalUserAlternate.vb 4     10/06/05 11:37a Teddy $
'$Workfile: ApprovalUserAlternate.vb $
'-----------------------------------------------------------
'$Log: /Clipan/AdIns.Application.Eloan.DataAccess.Approval/ApprovalUserAlternate.vb $
'
'4     10/06/05 11:37a Teddy
'
'3     9/10/04 1:22p Henry
'
'2     9/09/04 6:19p Henry
'
'1     9/08/04 1:15p Henry
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
Public Class ApprovalUserAlternate : Inherits AdIns.Application.Eloan.DataAccess.DataAccessBase
    Private Const spApprovalUserAlternateList As String = "spApprovalUserAlternatePaging"
    Private Const spApprovalUserAlternateAdd As String = "spApprovalUserAlternateAdd"
    Private Const spApprovalUserAlternateUpdate As String = "spApprovalUserAlternateEdit"
    Private Const spApprovalUserAlternateDelete As String = "spApprovalUserAlternateDelete"
    Private Const spApprovalUserAlternateView As String = "spApprovalUserAlternateView"

    Private Const PARAM_STATUS As String = "@isactive"
    Private Const PARAM_APPROVALSCHEME As String = "@ApprovalSchemeID"
    Private Const PARAM_APPROVALTYPE As String = "@ApprovalTypeID"
    Private Const PARAM_USERAPPROVAL As String = "@UserApproval"
    Private Const PARAM_USERALTERNATE As String = "@UserAlternate"
    Private Const PARAM_ESCALATIONUSER As String = "@EscalationUser"
    Private Const PARAM_ALTERNATESEQUENCE As String = "@AltSeqNum"
    Private Const spApprovalUserAlternateEscalationMember As String = "spApprovalUserAlternateEscalationMember"
    Private Const spApprovalUserAlternateMember As String = "spApprovalUserAlternateMember"

    Public Function ApprovalUserAlternateList(ByVal customclass As Entities.ApprovalUserAlternate) As Entities.ApprovalUserAlternate
        Dim params() As SqlParameter = New SqlParameter(2) {}
        Try
            params(0) = New SqlParameter(PARAM_APPROVALTYPE, SqlDbType.VarChar, 4)
            params(0).Value = customclass.ApprovalTypeID

            params(1) = New SqlParameter(PARAM_APPROVALSCHEME, SqlDbType.VarChar, 10)
            params(1).Value = customclass.ApprovalSchemeID

            params(2) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
            params(2).Value = customclass.UserApproval

            customclass.UserAlternateList = SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, spApprovalUserAlternateList, params).Tables(0)
            Return customclass
        Catch exp As Exception
            WriteException("ApprobalUserAlternate", "ApprovalUserAlternateList", exp.Message)
        End Try
    End Function

    Public Sub ApprovalUserAlternateAdd(ByVal customclass As Entities.ApprovalUserAlternate)
        Dim objConnection As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction
        Try
            Dim params() As SqlParameter = New SqlParameter(5) {}
            If objConnection.State = ConnectionState.Closed Then objConnection.Open()
            objtrans = objConnection.BeginTransaction

            params(0) = New SqlParameter(PARAM_APPROVALTYPE, SqlDbType.VarChar, 4)
            params(0).Value = customclass.ApprovalTypeID

            params(1) = New SqlParameter(PARAM_APPROVALSCHEME, SqlDbType.VarChar, 10)
            params(1).Value = customclass.ApprovalSchemeID

            params(2) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
            params(2).Value = customclass.UserApproval

            params(3) = New SqlParameter(PARAM_USERALTERNATE, SqlDbType.VarChar, 12)
            params(3).Value = customclass.UserAlternate

            params(4) = New SqlParameter(PARAM_ESCALATIONUSER, SqlDbType.VarChar, 12)
            params(4).Value = customclass.EscalationUser

            params(5) = New SqlParameter(PARAM_STATUS, SqlDbType.Bit)
            params(5).Value = customclass.IsActiveUserAlternate

            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, spApprovalUserAlternateAdd, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprobalUserAlternate", "ApprovalUserAlternateAdd", exp.Message)
        Finally
            If objConnection.State = ConnectionState.Open Then objConnection.Close()
            objConnection.Dispose()
        End Try
    End Sub

    Public Function ApprovalUserAlternateUpdate(ByVal customclass As Entities.ApprovalUserAlternate) As String
        Dim objConnection As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction
        Try
            Dim params() As SqlParameter = New SqlParameter(5) {}
            If objConnection.State = ConnectionState.Closed Then objConnection.Open()
            objtrans = objConnection.BeginTransaction

            params(0) = New SqlParameter(PARAM_APPROVALTYPE, SqlDbType.VarChar, 4)
            params(0).Value = customclass.ApprovalTypeID

            params(1) = New SqlParameter(PARAM_APPROVALSCHEME, SqlDbType.VarChar, 10)
            params(1).Value = customclass.ApprovalSchemeID

            params(2) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
            params(2).Value = customclass.UserApproval

            params(3) = New SqlParameter(PARAM_ALTERNATESEQUENCE, SqlDbType.Int)
            params(3).Value = customclass.AlternateSequence

            params(4) = New SqlParameter(PARAM_ESCALATIONUSER, SqlDbType.VarChar, 12)
            params(4).Value = customclass.EscalationUser

            params(5) = New SqlParameter(PARAM_STATUS, SqlDbType.Bit)
            params(5).Value = customclass.IsActiveUserAlternate

            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, spApprovalUserAlternateUpdate, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprobalUserAlternate", "ApprovalUserAlternateUpdate", exp.Message)
        Finally
            If objConnection.State = ConnectionState.Open Then objConnection.Close()
            objConnection.Dispose()
        End Try

    End Function

    Public Sub ApprovalUserAlternateDelete(ByVal customclass As Entities.ApprovalUserAlternate)
        Dim objConnection As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction
        Try
            Dim params() As SqlParameter = New SqlParameter(3) {}
            If objConnection.State = ConnectionState.Closed Then objConnection.Open()
            objtrans = objConnection.BeginTransaction

            params(0) = New SqlParameter(PARAM_APPROVALTYPE, SqlDbType.VarChar, 4)
            params(0).Value = customclass.ApprovalTypeID

            params(1) = New SqlParameter(PARAM_APPROVALSCHEME, SqlDbType.VarChar, 10)
            params(1).Value = customclass.ApprovalSchemeID

            params(2) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
            params(2).Value = customclass.UserApproval

            params(3) = New SqlParameter(PARAM_ALTERNATESEQUENCE, SqlDbType.Int)
            params(3).Value = customclass.AlternateSequence

            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, spApprovalUserAlternateDelete, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprobalUserAlternate", "ApprovalUserAlternateDelete", exp.Message)
        Finally
            If objConnection.State = ConnectionState.Open Then objConnection.Close()
            objConnection.Dispose()
        End Try
    End Sub

    Public Function UserAlternateUserApprovalList(ByVal customclass As Entities.ApprovalUserAlternate) As Entities.ApprovalUserAlternate
        Dim params() As SqlParameter = New SqlParameter(2) {}
        Try
            params(0) = New SqlParameter(PARAM_APPROVALTYPE, SqlDbType.VarChar, 4)
            params(0).Value = customclass.ApprovalTypeID

            params(1) = New SqlParameter(PARAM_APPROVALSCHEME, SqlDbType.VarChar, 10)
            params(1).Value = customclass.ApprovalSchemeID

            params(2) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
            params(2).Value = customclass.UserApproval

            customclass.AlternateMember = SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, spApprovalUserAlternateMember, params).Tables(0)
            customclass.AlternateEscalationMember = SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, spApprovalUserAlternateEscalationMember, params).Tables(0)
            Return customclass
        Catch exp As Exception
            WriteException("ApprobalUserAlternate", "ApprovalUserAlternateList", exp.Message)
        End Try
    End Function

    Public Function UserAlternateView(ByVal customclass As Entities.ApprovalUserAlternate) As Entities.ApprovalUserAlternate
        Dim params() As SqlParameter = New SqlParameter(6) {}
        Try
            params(0) = New SqlParameter(PARAM_APPROVALTYPE, SqlDbType.VarChar, 4)
            params(0).Value = customclass.ApprovalTypeID

            params(1) = New SqlParameter(PARAM_APPROVALSCHEME, SqlDbType.VarChar, 10)
            params(1).Value = customclass.ApprovalSchemeID

            params(2) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
            params(2).Value = customclass.UserApproval

            params(3) = New SqlParameter(PARAM_ALTERNATESEQUENCE, SqlDbType.Int)
            params(3).Value = customclass.AlternateSequence

            params(4) = New SqlParameter(PARAM_USERALTERNATE, SqlDbType.VarChar, 12)
            params(4).Direction = ParameterDirection.Output

            params(5) = New SqlParameter(PARAM_ESCALATIONUSER, SqlDbType.VarChar, 12)
            params(5).Direction = ParameterDirection.Output

            params(6) = New SqlParameter("@IsActive", SqlDbType.Bit)
            params(6).Direction = ParameterDirection.Output

            SqlHelper.ExecuteNonQuery(customclass.strConnection, CommandType.StoredProcedure, spApprovalUserAlternateView, params)

            customclass.UserAlternate = CStr(params(4).Value)
            customclass.EscalationUser = CStr(params(5).Value)
            customclass.IsActiveUserAlternate = CType(params(6).Value, Boolean)
            Return customclass
        Catch exp As Exception
            WriteException("ApprobalUserAlternate", "ApprovalUserAlternateList", exp.Message)
        End Try
    End Function
End Class
