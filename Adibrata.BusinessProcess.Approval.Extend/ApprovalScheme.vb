#Region "Revision History"
'$Header: /Clipan/AdIns.Application.Eloan.DataAccess.Approval/ApprovalScheme.vb 16    10/06/05 11:37a Teddy $
'$Workfile: ApprovalScheme.vb $
'-----------------------------------------------------------
'$Log: /Clipan/AdIns.Application.Eloan.DataAccess.Approval/ApprovalScheme.vb $
'
'16    10/06/05 11:37a Teddy
'
'15    7/14/05 6:38p Henry
'
'14    7/14/05 4:50p Johnson
'
'13    5/07/05 5:55p Teddy
'
'12    10/30/04 1:10p Henry
'
'11    9/18/04 8:17p Henry
'
'10    9/14/04 1:52p Henry
'
'9     9/09/04 6:19p Henry
'  
'  8     1/14/04 3:17p Henry
'  
'  7     1/14/04 3:11p Henry
'  
'  6     12/04/03 3:55p Henry
'  
'  5     11/07/03 10:51a Anita
'  
'  4     11/06/03 7:12p Henry
'  
'  3     11/06/03 10:14a Henry
'  
'  2     11/05/03 9:16p Henry
'  
'  1     11/04/03 7:51p Henry
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
Public Class ApprovalScheme : Inherits AdIns.Application.Eloan.DataAccess.DataAccessBase
    Private Const PARAM_APPROVALSCHEMEID As String = "@ApprovalSchemeID"
    Private Const PARAM_APPROVALTYPEID As String = "@ApprovalTypeID"
    Private Const PARAM_APPROVALSCHEMENAME As String = "@ApprovalSchemeName"
    Private Const PARAM_ISSTAGING As String = "@IsStaging"
    Private Const PARAM_ISLIMITNEEDED As String = "@IsLimitNeeded"
    Private Const PARAM_TRANSACTIONNOLABEL As String = "@TransactionNoLabel"
    Private Const PARAM_TRANSACTIONNOLINK As String = "@TransactionNoLink"

    Private Const PARAM_OPTIONALLABEL1 As String = "@OptionalLabel1"
    Private Const PARAM_OPTIONALLABEL2 As String = "@OptionalLabel2"
    Private Const PARAM_OPTIONALLABEL3 As String = "@OptionalLabel3"

    Private Const PARAM_OPTIONALLINK1 As String = "@OptionalLink1"
    Private Const PARAM_OPTIONALLINK2 As String = "@OptionalLink2"
    Private Const PARAM_OPTIONALSQL1 As String = "@OptionalSql1"
    Private Const PARAM_OPTIONALSQL2 As String = "@OptionalSql2"
    Private Const PARAM_ISSYSTEM As String = "@IsSystem"

    Private Const PARAM_NOTELABEL As String = "@NoteLabel"
    Private Const PARAM_NOTESQLCMD As String = "@NoteSQLCmd"
    Private Const PARAM_LIMITSQLCMD As String = "@LimitSQLCmd"

    Private Const PARAM_LINKLABEL1 As String = "@LinkLabel1"
    Private Const PARAM_LINKLABEL2 As String = "@LinkLabel2"
    Private Const PARAM_LINKLABEL3 As String = "@LinkLabel3"
    Private Const PARAM_LINKLABEL4 As String = "@LinkLabel4"
    Private Const PARAM_LINKLABEL5 As String = "@LinkLabel5"

    Private Const PARAM_LINKADDRESS1 As String = "@LinkAddress1"
    Private Const PARAM_LINKADDRESS2 As String = "@LinkAddress2"
    Private Const PARAM_LINKADDRESS3 As String = "@LinkAddress3"
    Private Const PARAM_LINKADDRESS4 As String = "@LinkAddress4"
    Private Const PARAM_LINKADDRESS5 As String = "@LinkAddress5"
    Private Const PARAM_LIMITLABEL As String = "@LimitLabel"

    Private Const PARAM_APPROVALSEQNO As String = "@ApprovalSeqNum"
    Private Const PARAM_APPROVALPARENTSEQNUM As String = "@ApprovalParentSeqNum"
    Private Const PARAM_APPROVALDESCRIPTION As String = "@ApprovalDescription"
    Private Const PARAM_MAXLIMIT As String = "@MaxLimit"
    Private Const PARAM_LEVEL As String = "@Level"
    Private Const PARAM_CANFINALREJECT As String = "@CanFinalReject"
    Private Const PARAM_CANFINALAPPROVE As String = "@CanFinalApprove"
    Private Const PARAM_CANESCALATION As String = "@CanEscalation"
    Private Const PARAM_REJECTACTION As String = "@RejectAction"
    Private Const PARAM_TYPELIMIT As String = "@TypeLimit"

    Private Const PARAM_LIMITVALUE As String = "@LimitValue"
    Private Const PARAM_DURATION As String = "@Duration"
    Private Const PARAM_AUTOUSER As String = "@AutoUser"
    Private Const PARAM_NEXTPERSON As String = "@NextPerson"
        
    Private Const SPAPPROVALSCHEMADD As String = "spApprovalSchemeAdd"
    Private Const SPAPPROVALSCHEMEDIT As String = "spApprovalSchemeEdit"
    Private Const SPAPPROVALSCHEMDELETE As String = "spApprovalSchemeDelete"
    Private Const SPAPPROVALSCHEMVIEW As String = "spApprovalSchemeView"

    Private Const SPAPPROVALMEMBERVIEW As String = "spApprovalMemberView"
    Private Const SPAPPROVALMEMBERADD As String = "spApprovalMemberAdd"
    Private Const SPAPPROVALMEMBEREDIT As String = "spApprovalMemberEdit"
    Private Const SPAPPROVALMEMBERDELETE As String = "spApprovalMemberDelete"
    Private Const SPAPPROVALMEMBERLOGINLIST As String = "spApprovalMemberLoginList"

    Private Const SPGETAPPROVALTYPE As String = "spgetapprovaltype"
    Private Const SPAPPROVALTREEPATHDELETE As String = "spApprovalTreePathDelete"
    Private Const SPAPPROVALTREEPATHADD As String = "spApprovalTreePathAdd"
    Private Const SPAPPROVALTREEPATHEDIT As String = "spApprovalTreePathEdit"
    Private Const SPAPPROVALTREEGETLIMIT As String = "spApprovalTreeGetLimit"
    Private Const SPAPPROVALTREEPATHVIEW As String = "spApprovalTreePathView"
    Private Const SPAPPROVALMEMBERSETMEMBER As String = "spApprovalMemberSetMember"
    Private Const SPAPPROVALPATHTREE As String = "spApprovalPathTreeNEW"
    Private Const PARAM_CANCHANGEFINALLEVEL As String = "@canchangefinallevel"
    Private Const PARAM_APPROVALDURATION As String = "@approvalduration"
    Private Const PARAM_ADDITIONALEMAIL As String = "@additionalemail"
    Private Const spApprovalCommunicationLinkedProcess As String = "spApprovalCommunicationLinkedProcess"

#Region "Approval Scheme"
    Public Sub ApprovalSchemeAdd(ByVal customclass As Entities.ApprovalScheme)
        Dim objcon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction

        Dim params() As SqlParameter = New SqlParameter(26) {}
        Try
            params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
            params(0).Value = customclass.ApprovalSchemeID

            params(1) = New SqlParameter(PARAM_APPROVALTYPEID, SqlDbType.VarChar, 4)
            params(1).Value = customclass.ApprovalTypeID

            params(2) = New SqlParameter(PARAM_APPROVALSCHEMENAME, SqlDbType.VarChar, 50)
            params(2).Value = customclass.ApprovalSchemeName

            params(3) = New SqlParameter(PARAM_ISSTAGING, SqlDbType.Bit)
            params(3).Value = customclass.IsStaging

            params(4) = New SqlParameter(PARAM_ISLIMITNEEDED, SqlDbType.Bit)
            params(4).Value = customclass.IsLimitNeeded

            params(5) = New SqlParameter(PARAM_TRANSACTIONNOLABEL, SqlDbType.VarChar, 30)
            params(5).Value = customclass.TransactionNoLabel

            params(6) = New SqlParameter(PARAM_TRANSACTIONNOLINK, SqlDbType.VarChar, 110)
            params(6).Value = customclass.TransactionNoLink

            params(7) = New SqlParameter(PARAM_OPTIONALLABEL1, SqlDbType.VarChar, 30)
            params(7).Value = customclass.OptionalLabel1

            params(8) = New SqlParameter(PARAM_OPTIONALLABEL2, SqlDbType.VarChar, 30)
            params(8).Value = customclass.OptionalLabel2

            params(9) = New SqlParameter(PARAM_OPTIONALLINK1, SqlDbType.VarChar, 500)
            params(9).Value = customclass.OptionalLink1
            params(10) = New SqlParameter(PARAM_OPTIONALLINK2, SqlDbType.VarChar, 500)
            params(10).Value = customclass.OptionalLink2

            params(11) = New SqlParameter(PARAM_OPTIONALSQL1, SqlDbType.VarChar, 2000)
            params(11).Value = customclass.OptionalSQL1
            params(12) = New SqlParameter(PARAM_OPTIONALSQL2, SqlDbType.VarChar, 2000)
            params(12).Value = customclass.OptionalSQL2

            params(13) = New SqlParameter(PARAM_NOTELABEL, SqlDbType.VarChar, 30)
            params(13).Value = customclass.NoteLabel

            params(14) = New SqlParameter(PARAM_NOTESQLCMD, SqlDbType.VarChar, 2000)
            params(14).Value = customclass.NoteSQLCmd

            params(15) = New SqlParameter(PARAM_LIMITLABEL, SqlDbType.VarChar, 30)
            params(15).Value = customclass.LimitLabel

            params(16) = New SqlParameter(PARAM_LINKLABEL1, SqlDbType.VarChar, 30)
            params(16).Value = customclass.LinkLabel1
            params(17) = New SqlParameter(PARAM_LINKLABEL2, SqlDbType.VarChar, 30)
            params(17).Value = customclass.LinkLabel2
            params(18) = New SqlParameter(PARAM_LINKLABEL3, SqlDbType.VarChar, 30)
            params(18).Value = customclass.LinkLabel3
            params(19) = New SqlParameter(PARAM_LINKLABEL4, SqlDbType.VarChar, 30)
            params(19).Value = customclass.LinkLabel4
            params(20) = New SqlParameter(PARAM_LINKLABEL5, SqlDbType.VarChar, 30)
            params(20).Value = customclass.LinkLabel5

            params(21) = New SqlParameter(PARAM_LINKADDRESS1, SqlDbType.VarChar, 1000)
            params(21).Value = customclass.LinkAddress1
            params(22) = New SqlParameter(PARAM_LINKADDRESS2, SqlDbType.VarChar, 1000)
            params(22).Value = customclass.LinkAddress2
            params(23) = New SqlParameter(PARAM_LINKADDRESS3, SqlDbType.VarChar, 1000)
            params(23).Value = customclass.LinkAddress3
            params(24) = New SqlParameter(PARAM_LINKADDRESS4, SqlDbType.VarChar, 1000)
            params(24).Value = customclass.LinkAddress4
            params(25) = New SqlParameter(PARAM_LINKADDRESS5, SqlDbType.VarChar, 1000)
            params(25).Value = customclass.LinkAddress5

            params(26) = New SqlParameter(PARAM_ISSYSTEM, SqlDbType.Bit)
            params(26).Value = customclass.IsSystem

            If objcon.State = ConnectionState.Closed Then objcon.Open()
            objtrans = objcon.BeginTransaction
            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALSCHEMADD, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprovalScheme", "ApprovalSchemeAdd", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Sub

    Public Sub ApprovalSchemeEdit(ByVal customclass As Entities.ApprovalScheme)
        Dim objcon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction

        Dim params() As SqlParameter = New SqlParameter(26) {}
        Try
            params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
            params(0).Value = customclass.ApprovalSchemeID

            params(1) = New SqlParameter(PARAM_APPROVALTYPEID, SqlDbType.VarChar, 4)
            params(1).Value = customclass.ApprovalTypeID

            params(2) = New SqlParameter(PARAM_APPROVALSCHEMENAME, SqlDbType.VarChar, 50)
            params(2).Value = customclass.ApprovalSchemeName

            params(3) = New SqlParameter(PARAM_ISSTAGING, SqlDbType.Bit)
            params(3).Value = customclass.IsStaging

            params(4) = New SqlParameter(PARAM_ISLIMITNEEDED, SqlDbType.Bit)
            params(4).Value = customclass.IsLimitNeeded

            params(5) = New SqlParameter(PARAM_TRANSACTIONNOLABEL, SqlDbType.VarChar, 30)
            params(5).Value = customclass.TransactionNoLabel

            params(6) = New SqlParameter(PARAM_TRANSACTIONNOLINK, SqlDbType.VarChar, 110)
            params(6).Value = customclass.TransactionNoLink

            params(7) = New SqlParameter(PARAM_OPTIONALLABEL1, SqlDbType.VarChar, 30)
            params(7).Value = customclass.OptionalLabel1

            params(8) = New SqlParameter(PARAM_OPTIONALLABEL2, SqlDbType.VarChar, 30)
            params(8).Value = customclass.OptionalLabel2

            params(9) = New SqlParameter(PARAM_OPTIONALLINK1, SqlDbType.VarChar, 500)
            params(9).Value = customclass.OptionalLink1
            params(10) = New SqlParameter(PARAM_OPTIONALLINK2, SqlDbType.VarChar, 500)
            params(10).Value = customclass.OptionalLink2

            params(11) = New SqlParameter(PARAM_OPTIONALSQL1, SqlDbType.VarChar, 2000)
            params(11).Value = customclass.OptionalSQL1
            params(12) = New SqlParameter(PARAM_OPTIONALSQL2, SqlDbType.VarChar, 2000)
            params(12).Value = customclass.OptionalSQL2

            params(13) = New SqlParameter(PARAM_NOTELABEL, SqlDbType.VarChar, 30)
            params(13).Value = customclass.NoteLabel

            params(14) = New SqlParameter(PARAM_NOTESQLCMD, SqlDbType.VarChar, 2000)
            params(14).Value = customclass.NoteSQLCmd

            params(15) = New SqlParameter(PARAM_LIMITLABEL, SqlDbType.VarChar, 30)
            params(15).Value = customclass.LimitLabel

            params(16) = New SqlParameter(PARAM_LINKLABEL1, SqlDbType.VarChar, 30)
            params(16).Value = customclass.LinkLabel1
            params(17) = New SqlParameter(PARAM_LINKLABEL2, SqlDbType.VarChar, 30)
            params(17).Value = customclass.LinkLabel2
            params(18) = New SqlParameter(PARAM_LINKLABEL3, SqlDbType.VarChar, 30)
            params(18).Value = customclass.LinkLabel3
            params(19) = New SqlParameter(PARAM_LINKLABEL4, SqlDbType.VarChar, 30)
            params(19).Value = customclass.LinkLabel4
            params(20) = New SqlParameter(PARAM_LINKLABEL5, SqlDbType.VarChar, 30)
            params(20).Value = customclass.LinkLabel5

            params(21) = New SqlParameter(PARAM_LINKADDRESS1, SqlDbType.VarChar, 1000)
            params(21).Value = customclass.LinkAddress1
            params(22) = New SqlParameter(PARAM_LINKADDRESS2, SqlDbType.VarChar, 1000)
            params(22).Value = customclass.LinkAddress2
            params(23) = New SqlParameter(PARAM_LINKADDRESS3, SqlDbType.VarChar, 1000)
            params(23).Value = customclass.LinkAddress3
            params(24) = New SqlParameter(PARAM_LINKADDRESS4, SqlDbType.VarChar, 1000)
            params(24).Value = customclass.LinkAddress4
            params(25) = New SqlParameter(PARAM_LINKADDRESS5, SqlDbType.VarChar, 1000)
            params(25).Value = customclass.LinkAddress5

            params(26) = New SqlParameter(PARAM_ISSYSTEM, SqlDbType.Bit)
            params(26).Value = customclass.IsSystem

            If objcon.State = ConnectionState.Closed Then objcon.Open()
            objtrans = objcon.BeginTransaction
            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALSCHEMEDIT, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprovalScheme", "ApprovalSchemeEdit", exp.Message )
            Throw New Exception(exp.Message)

        Finally
            If objcon.State = ConnectionState.Open Then objcon.Close()
            objcon.Dispose()
        End Try

    End Sub

    Public Sub ApprovalSchemeDelete(ByVal customclass As Entities.ApprovalScheme)
        Dim objcon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(1) {}
        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID

        params(1) = New SqlParameter(PARAM_APPROVALTYPEID, SqlDbType.VarChar, 4)
        params(1).Value = customclass.ApprovalTypeID
        Try
            If objcon.State = ConnectionState.Closed Then objcon.Open()
            objtrans = objcon.BeginTransaction
            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALSCHEMDELETE, params)
            objtrans.Commit()
        Catch exp As Exception
            WriteException("ApprovalScheme", "ApprovalSchemeDelete", exp.Message )
            objtrans.Rollback()
            Throw New Exception(exp.Message)
        Finally
            If objcon.State = ConnectionState.Open Then objcon.Close()
            objcon.Dispose()
        End Try

    End Sub

    Public Function ApprovalSchemeView(ByVal customclass As Entities.ApprovalScheme) As Entities.ApprovalScheme
        Dim objread As SqlDataReader
        Dim params() As SqlParameter = New SqlParameter(1) {}
        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID

        params(1) = New SqlParameter(PARAM_APPROVALTYPEID, SqlDbType.VarChar, 4)
        params(1).Value = customclass.ApprovalTypeID
        Try
            objread = SqlHelper.ExecuteReader(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALSCHEMVIEW, params)
            If objread.Read Then
                With customclass
                    .ApprovalSchemeID = objread("ApprovalSchemeID").ToString
                    .ApprovalTypeID = objread("ApprovalTypeID").ToString
                    .ApprovalSchemeName = objread("ApprovalSchemeName").ToString
                    .IsStaging = CBool(objread("IsStaging"))
                    .IsLimitNeeded = CBool(objread("IsLimitNeeded"))
                    .TransactionNoLabel = objread("TransactionNoLabel").ToString
                    .TransactionNoLink = objread("TransactionNoLink").ToString
                    .OptionalLabel1 = objread("OptionalLabel1").ToString
                    .OptionalLabel2 = objread("OptionalLabel2").ToString
                    .OptionalLink1 = objread("OptionalLink1").ToString
                    .OptionalLink2 = objread("OptionalLink2").ToString
                    .OptionalSQL1 = objread("OptionalSQL1").ToString
                    .OptionalSQL2 = objread("OptionalSQL2").ToString
                    .NoteLabel = objread("NoteLabel").ToString
                    .NoteSQLCmd = objread("NoteSQLCmd").ToString
                    .LimitLabel = objread("LimitLabel").ToString
                    .LinkLabel1 = objread("LinkLabel1").ToString
                    .LinkLabel2 = objread("LinkLabel2").ToString
                    .LinkLabel3 = objread("LinkLabel3").ToString
                    .LinkLabel4 = objread("LinkLabel4").ToString
                    .LinkLabel5 = objread("LinkLabel5").ToString
                    .LinkAddress1 = objread("LinkAddress1").ToString
                    .LinkAddress2 = objread("LinkAddress2").ToString
                    .LinkAddress3 = objread("LinkAddress3").ToString
                    .LinkAddress4 = objread("LinkAddress4").ToString
                    .LinkAddress5 = objread("LinkAddress5").ToString
                    .IsSystem = CBool(objread("IsSystem"))
                End With
            End If
            objread.Close()
            Return customclass
        Catch exp As Exception
            WriteException("ApprovalScheme", "ApprovalSchemeView", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Function

    Public Function GetApprovalType(ByVal strConnection As String) As DataTable
        Try
            Return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, SPGETAPPROVALTYPE).Tables(0)

        Catch exp As Exception
            WriteException("ApprovalScheme", "GetApprovalType", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Function
#End Region
#Region "Approval Tree"
    Public Function ApprovalPathTree(ByVal customclass As Entities.ApprovalScheme) As DataTable
        Dim params() As SqlParameter = New SqlParameter(0) {}
        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID
        Try
            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALPATHTREE, params).Tables(0)
        Catch exp As Exception
            WriteException("ApprovalScheme", "ApprovalPathTree", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Function
#Region "Approval Member"
    Public Sub ApprovalMemberDelete(ByVal Customclass As Entities.ApprovalScheme)
        Dim objCon As New SqlConnection(Customclass.strConnection)
        Dim objtrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(2) {}

        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = Customclass.ApprovalSchemeID

        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
        params(1).Value = Customclass.ApprovalSeqNo

        params(2) = New SqlParameter(PARAM_LOGINID, SqlDbType.VarChar, 12)
        params(2).Value = Customclass.LoginId
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objtrans = objCon.BeginTransaction
            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALMEMBERDELETE, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprovalScheme", "ApprovalMemberDelete", exp.Message )
            Throw New Exception(exp.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub
    Public Function ApprovalMemberLoginList(ByVal customclass As Entities.ApprovalScheme) As DataTable
        Dim params() As SqlParameter = New SqlParameter(1) {}
        params(0) = New SqlParameter(PARAM_LOGINID, SqlDbType.VarChar, 8000)
        params(0).Value = customclass.LoginId
        params(1) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(1).Value = customclass.ApprovalSchemeID
        Try
            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALMEMBERLOGINLIST, params).Tables(0)
        Catch exp As Exception
            WriteException("ApprovalScheme", "ApprovalMemberLoginList", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Function
    Public Function ApprovalMemberSetMember(ByVal customclass As Entities.ApprovalScheme) As DataTable
        Dim params() As SqlParameter = New SqlParameter(0) {}
        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID
        Try
            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALMEMBERSETMEMBER, params).Tables(0)
        Catch exp As Exception
            WriteException("ApprovalScheme", "ApprovalMemberSetMember", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Function
    Public Function ApprovalMemberView(ByVal customclass As Entities.ApprovalScheme) As DataTable
        Dim params() As SqlParameter = New SqlParameter(1) {}
        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID

        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
        params(1).Value = customclass.ApprovalSeqNo
        Try
            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALMEMBERVIEW, params).Tables(0)
        Catch exp As Exception
            WriteException("ApprovalScheme", "ApprovalMemberView", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Function

    Public Sub ApprovalMemberAdd(ByVal Customclass As Entities.ApprovalScheme)
        Dim objCon As New SqlConnection(Customclass.strConnection)
        Dim objtrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(7) {}

        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = Customclass.ApprovalSchemeID

        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
        params(1).Value = Customclass.ApprovalSeqNo

        params(2) = New SqlParameter(PARAM_LIMITVALUE, SqlDbType.Decimal)
        params(2).Value = Customclass.Limit

        params(3) = New SqlParameter(PARAM_DURATION, SqlDbType.Int)
        params(3).Value = Customclass.Duration

        params(4) = New SqlParameter(PARAM_AUTOUSER, SqlDbType.VarChar, 12)
        params(4).Value = Customclass.AutoUser

        params(5) = New SqlParameter(PARAM_LOGINID, SqlDbType.VarChar, 12)
        params(5).Value = Customclass.LoginId

        params(6) = New SqlParameter(PARAM_NEXTPERSON, SqlDbType.VarChar, 8000)
        params(6).Value = Customclass.NextPerson

        params(7) = New SqlParameter(PARAM_ADDITIONALEMAIL, SqlDbType.VarChar, 50)
        params(7).Value = Customclass.AdditionalEmail
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objtrans = objCon.BeginTransaction
            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALMEMBERADD, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprovalScheme", "ApprovalMemberAdd", exp.Message )
            Throw New Exception(exp.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub
    Public Sub ApprovalMemberEdit(ByVal customclass As Entities.ApprovalScheme)
        Dim objCon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(7) {}

        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID

        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
        params(1).Value = customclass.ApprovalSeqNo

        params(2) = New SqlParameter(PARAM_LIMITVALUE, SqlDbType.Decimal)
        params(2).Value = customclass.Limit

        params(3) = New SqlParameter(PARAM_DURATION, SqlDbType.Int)
        params(3).Value = customclass.Duration

        params(4) = New SqlParameter(PARAM_AUTOUSER, SqlDbType.VarChar, 12)
        params(4).Value = customclass.AutoUser

        params(5) = New SqlParameter(PARAM_LOGINID, SqlDbType.VarChar, 12)
        params(5).Value = customclass.LoginId

        params(6) = New SqlParameter(PARAM_NEXTPERSON, SqlDbType.VarChar, 8000)
        params(6).Value = customclass.NextPerson

        params(7) = New SqlParameter(PARAM_ADDITIONALEMAIL, SqlDbType.VarChar, 50)
        params(7).Value = customclass.AdditionalEmail
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objtrans = objCon.BeginTransaction
            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALMEMBEREDIT, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprovalScheme", "ApprovalMemberEdit", exp.Message )
            Throw New Exception(exp.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub
#End Region

#Region "Approval Path"
    Public Sub ApprovalPathDelete(ByVal customclass As Entities.ApprovalScheme)
        Dim objCon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction


        Dim params() As SqlParameter = New SqlParameter(1) {}
        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID

        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
        params(1).Value = customclass.ApprovalSeqNo
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objtrans = objCon.BeginTransaction

            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALTREEPATHDELETE, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprovalScheme", "ApprovalPathDelete", exp.Message )
            Throw New Exception(exp.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub

    Public Sub ApprovalPathAdd(ByVal customclass As Entities.ApprovalScheme)
        Dim objCon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(10) {}

        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID

        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
        params(1).Value = customclass.ApprovalSeqNo

        params(2) = New SqlParameter(PARAM_APPROVALDESCRIPTION, SqlDbType.VarChar, 50)
        params(2).Value = customclass.ApprovalDescription

        params(3) = New SqlParameter(PARAM_MAXLIMIT, SqlDbType.Decimal)
        params(3).Value = customclass.MaxLimit

        params(4) = New SqlParameter(PARAM_LEVEL, SqlDbType.Decimal)
        params(4).Value = customclass.Level

        params(5) = New SqlParameter(PARAM_CANFINALREJECT, SqlDbType.Int)
        params(5).Value = customclass.CanFinalReject

        params(6) = New SqlParameter(PARAM_CANFINALAPPROVE, SqlDbType.Bit)
        params(6).Value = customclass.CanFinalApprove

        params(7) = New SqlParameter(PARAM_CANESCALATION, SqlDbType.Bit)
        params(7).Value = customclass.CanEscalation

        params(8) = New SqlParameter(PARAM_CANCHANGEFINALLEVEL, SqlDbType.Bit)
        params(8).Value = customclass.CanChangeFinalLevel

        params(9) = New SqlParameter(PARAM_APPROVALDURATIOn, SqlDbType.Int)
        params(9).Value = customclass.ApprovalDuration

        params(10) = New SqlParameter(PARAM_REJECTACTION, SqlDbType.Char, 1)
        params(10).Value = customclass.RejectAction
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objtrans = objCon.BeginTransaction

            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALTREEPATHADD, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprovalScheme", "ApprovalPathAdd", exp.Message )
            Throw New Exception(exp.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub

    Public Sub ApprovalPathEdit(ByVal customclass As Entities.ApprovalScheme)
        Dim objCon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(11) {}

        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID

        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
        params(1).Value = customclass.ApprovalSeqNo

        params(2) = New SqlParameter(PARAM_APPROVALPARENTSEQNUM, SqlDbType.Int)
        params(2).Value = customclass.ApprovalParentSeqNum

        params(3) = New SqlParameter(PARAM_APPROVALDESCRIPTION, SqlDbType.VarChar, 50)
        params(3).Value = customclass.ApprovalDescription

        params(4) = New SqlParameter(PARAM_MAXLIMIT, SqlDbType.Decimal)
        params(4).Value = customclass.MaxLimit

        params(5) = New SqlParameter(PARAM_LEVEL, SqlDbType.Decimal)
        params(5).Value = customclass.Level

        params(6) = New SqlParameter(PARAM_CANFINALREJECT, SqlDbType.Int)
        params(6).Value = customclass.CanFinalReject

        params(7) = New SqlParameter(PARAM_CANFINALAPPROVE, SqlDbType.Bit)
        params(7).Value = customclass.CanFinalApprove

        params(8) = New SqlParameter(PARAM_CANESCALATION, SqlDbType.Bit)
        params(8).Value = customclass.CanEscalation

        params(9) = New SqlParameter(PARAM_CANCHANGEFINALLEVEL, SqlDbType.Bit)
        params(9).Value = customclass.CanChangeFinalLevel

        params(10) = New SqlParameter(PARAM_APPROVALDURATION, SqlDbType.Int)
        params(10).Value = customclass.ApprovalDuration

        params(11) = New SqlParameter(PARAM_REJECTACTION, SqlDbType.Char, 1)
        params(11).Value = customclass.RejectAction
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objtrans = objCon.BeginTransaction

            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALTREEPATHEDIT, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprovalScheme", "ApprovalPathEdit", exp.Message )
            Throw New Exception(exp.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub

    Public Function ApprovalPathGetLimit(ByVal customclass As Entities.ApprovalScheme) As Double

        Dim params() As SqlParameter = New SqlParameter(2) {}

        params(0) = New SqlParameter(PARAM_TYPELIMIT, SqlDbType.VarChar, 3)
        params(0).Value = customclass.TypeLimit

        params(1) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(1).Value = customclass.ApprovalSchemeID

        params(2) = New SqlParameter(PARAM_LEVEL, SqlDbType.Int)
        params(2).Value = customclass.Level

        Try
            Return CDbl(SqlHelper.ExecuteScalar(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALTREEGETLIMIT, params))
        Catch exp As Exception
            WriteException("ApprovalScheme", "ApprovalPathGetLimit", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Function

    Public Function ApprovalPathView(ByVal customclass As Entities.ApprovalScheme) As Entities.ApprovalScheme
        Dim objread As SqlDataReader
        Dim params() As SqlParameter = New SqlParameter(2) {}

        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 12)
        params(0).Value = customclass.ApprovalSchemeID

        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
        params(1).Value = customclass.ApprovalSeqNo

        params(2) = New SqlParameter(PARAM_APPROVALPARENTSEQNUM, SqlDbType.Int)
        params(2).Value = customclass.ApprovalParentSeqNum

        Try
            objread = SqlHelper.ExecuteReader(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALTREEPATHVIEW, params)
            If objread.Read Then
                With customclass
                    .Limit = CDbl(objread("Limit"))
                    .MaxLimit = CDbl(objread("MaxLimit"))
                    .MinLimit = CDbl(objread("MinLimit"))
                    .CanFinalReject = CBool(objread("CanFinalReject"))
                    .CanFinalApprove = CBool(objread("CanFinalApprove"))
                    .CanEscalation = CBool(objread("CanEscalation"))
                    .RejectAction = CStr(objread("RejectAction"))
                    .ApprovalDuration = CInt(objread("ApprovalDuration"))
                    .CanChangeFinalLevel = CBool(objread("CanChangeFinalLevel"))
                End With
            End If
            objread.Close()
            Return customclass
        Catch exp As Exception
            WriteException("ApprovalScheme", "ApprovalPathView", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Function
#End Region
#End Region
#Region "Approval Communication Linked"
    Public Sub AprCommLinkedProcess(ByVal Customclass As Entities.ApprovalScheme)
        Dim objCon As New SqlConnection(Customclass.strConnection)
        Dim objtrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(9) {}

        params(0) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
        params(0).Value = Customclass.BranchId

        params(1) = New SqlParameter("@ApplicationID", SqlDbType.VarChar, 20)
        params(1).Value = Customclass.ApplicationID

        params(2) = New SqlParameter("@SenderName", SqlDbType.VarChar, 50)
        params(2).Value = Customclass.LoginId

        params(3) = New SqlParameter("@RecipientName", SqlDbType.VarChar, 1000)
        params(3).Value = Customclass.Recipient

        params(4) = New SqlParameter("@Subject", SqlDbType.Text)
        params(4).Value = Customclass.subjectMessage

        params(5) = New SqlParameter("@Message", SqlDbType.Text)
        params(5).Value = Customclass.mailmessage

        params(6) = New SqlParameter("@FlagMessage", SqlDbType.Char, 1)
        params(6).Value = Customclass.FlagMessage

        params(7) = New SqlParameter("@StrEmployeeIDRecepient", SqlDbType.VarChar, 8000)
        params(7).Value = Customclass.StrEmployeeIDRecipient

        params(8) = New SqlParameter("@SeqNo", SqlDbType.Int)
        params(8).Value = Customclass.SeqNo

        params(9) = New SqlParameter("@BusinessDate", SqlDbType.DateTime)
        params(9).Value = Customclass.BusinessDate
        
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objtrans = objCon.BeginTransaction
            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, spApprovalCommunicationLinkedProcess, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("ApprovalScheme", "AprCommLinkedProcess", exp.Message)
            Throw New Exception(exp.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub
#End Region

End Class
