#Region "Revision History"
'$Header: /Clipan/AdIns.Application.Eloan.DataAccess.Approval/ApprovalScreen.vb 24    5/24/06 2:31a Yovita $
'$Workfile: ApprovalScreen.vb $
'-----------------------------------------------------------
'$Log: /Clipan/AdIns.Application.Eloan.DataAccess.Approval/ApprovalScreen.vb $
'26    15/07/08 4:00 Pita
'
'25    25/06/08 9:38a Amelya
'
'24    5/24/06 2:31a Yovita
'
'24    5/23/06 3:32p Henry
'
'23    6/08/05 4:17p Henry
'
'22    4/13/05 11:22a Johnson
'
'21    4/09/05 4:42p Henry
'
'20    10/30/04 1:10p Henry
'
'19    9/25/04 5:49p Henry
'
'18    9/20/04 7:39p Henry
'
'17    9/19/04 4:10p Henry
'
'16    9/18/04 8:17p Henry
'
'15    9/18/04 5:43p Henry
'
'14    9/17/04 8:58p Henry
'
'13    9/16/04 8:00p Henry
'
'12    9/16/04 6:03p Henry
'
'11    9/16/04 2:31p Henry
'
'10    9/15/04 6:27p Henry
'
'9     9/15/04 9:29a Henry
'
'8     9/14/04 5:19p Henry
'
'7     9/14/04 3:37p Henry
'
'6     9/14/04 1:52p Henry
'  
'  5     1/14/04 4:40p Henry
'  
'  4     11/10/03 8:29p Henry
'  
'  3     11/10/03 3:24p Henry
'  
'  2     11/07/03 8:01p Henry
'  
'  1     11/07/03 7:43p Henry
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
Public Class ApprovalScreen : Inherits AdIns.Application.Eloan.DataAccess.DataAccessBase
    Private Const PARAM_APPROVALTYPEID As String = "@ApprovalTypeID"
    Private Const PARAM_REQUESTFROM As String = "@RequestFrom"
    Private Const PARAM_APPROVEDBY As String = "@ApprBy"
    Private Const PARAM_APPROVALSTATUS As String = "@ApprovalStatus"
    Private Const PARAM_APPROVALRESULT As String = "@ApprovalResult"
    Private Const PARAM_APPROVALNO As String = "@ApprovalNo"
    Private Const PARAM_TRANSACTIONNO As String = "@transactionno"
    Private Const PARAM_APPROVALSCHEMEID As String = "@ApprovalSchemeID"
    Private Const PARAM_ISREREQUEST As String = "@IsReRequest"
    Private Const PARAM_APPROVALLOGINID As String = "@ApprovalLoginID"
    Private Const PARAM_APPROVALAMOUNT As String = "@AmountApproval"
    Private Const PARAM_ISSTAGING As String = "@IsStaging"
    Private Const PARAM_ISFINALLEVEL As String = "@IsFinalLevel"
    Private Const PARAM_ISBACKWARD As String = "@isBackWard"
    Private Const PARAM_ISFINALSTAGE As String = "@IsFinalStage"
    Private Const PARAM_APPROVALSEQUENCENO As String = "@approvalseqNo"
    Private Const PARAM_FINALLEVELAPPROVAL As String = "@finallevelapproval"
    Private Const PARAM_USERREQUEST As String = "@userrequest"
    Private Const PARAM_ISEVERREJECTED As String = "@IsEverRejected"
    Private Const PARAM_ALTERNATEUSER As String = "@AlternateUser"
    Private Const PARAM_NEXTPERSON As String = "@NextPerson"
    Private Const PARAM_MAINAPPROVALLOGINID As String = "@MainApprovalLoginID"
    Private Const PARAM_SECURITYCODE As String = "@SecurityCode"
    Private Const PARAM_USERSECURITYCODE As String = "@UserSecurityCode"
    Private Const PARAM_APPROVALDATE As String = "@ApprovalDate"
    Private Const PARAM_APPROVALNOTES As String = "@ApprovalNotes"
    Private Const PARAM_ISFORWARD As String = "@IsForward"
    Private Const PARAM_USERAPPROVAL As String = "@UserApproval"
    Private Const PARAM_REASONID As String = "@ReasonID"
    Private Const SPAPPROVALREQUESTTONEXTPERSON As String = "spApprovalRequestToNextPerson"
    Private Const SPAPPROVALHISTORYLIST As String = "spApprovalHistoryList"
    Private Const SPAPPROVALTYPEDESCRIPTION As String = "spApprovalType"
    Private Const SPAPPROVALLISTUSER As String = "spApprovalListUser"
    Private Const SPAPPROVALLISTPAGING As String = "spApprovalListPaging"
    Private Const SPAPPROVALSCREENUSERREQUEST As String = "spApprovalScreenUserRequest"
    Private Const SPAPPROVALSCREENVIEWSCHEME As String = "spApprovalScreenViewScheme"
    Private Const SPAPPROVALSCREENUSERSCHEME As String = "spApprovalScreenUserScheme"
    Private Const SPAPPROVALSCREENISVALIDAPPROVAL As String = "spApprovalScreenIsValidApproval"
    Private Const SPAPPROVALSCREENSETLIMIT As String = "spApprovalScreenSetLimit"
    Private Const SPTAKEBACKAPPROVAL As String = "spApprovalScreenTakeBack"
    Private Const SPAPPROVALHOLD As String = "spApprovalScreenHold"
    Private Const SPAPPROVALUNHOLD As String = "spApprovalScreenUnHold"
    Private Const SPAPPROVALHISTORY As String = "spApprovalHistory"
    Private Const SPAPPROVALGETMAINUSERREQUEST As String = "spApprovalGetMainUserRequest"
    Private Const SPAPPROVALSCREENISEVERREJECTED As String = "spApprovalScreenIsEverRejected"
    Private Const SPAPPROVALCHANGEFINALLEVEL As String = "spApprovalChangeFinalLevel"
    Private Const SPAPPROVALSCREENSETNEXTPERSON As String = "spApprovalScreenSetNextPerson"
    Private Const SPAPPROVALSCREENCHECKISFINAL As String = "spApprovalScreenIsCheckFinal"
    Private Const SPAPPROVALSCREEN2NEXTPERSON As String = "spApprovalScreen2StepNextPerson"
    Private Const SPAPPROVALDECLINE As String = "spApprovalDecline"
    Private Const SPAPPROVALSCREENSPAPPROVALFINAL As String = "spApprovalSaveFinal"
    Private Const SPAPPROVALGETMAINUSERAPPROVAL As String = "spApprovalGetmainUserApproval"

    Private oMailServices As New ApprovalMailNotification

    Public Function ApprovalList(ByVal customclass As Entities.ApprovalScreen) As Entities.ApprovalScreen
        Dim params() As SqlParameter = New SqlParameter(11) {}
        Try
            params(0) = New SqlParameter(PARAM_CURRENTPAGE, SqlDbType.Int)
            params(0).Value = customclass.CurrentPage

            params(1) = New SqlParameter(PARAM_PAGESIZE, SqlDbType.Int)
            params(1).Value = customclass.PageSize

            params(2) = New SqlParameter(PARAM_WHERECOND, SqlDbType.VarChar, 8000)
            params(2).Value = customclass.WhereCond

            params(3) = New SqlParameter(PARAM_SORTBY, SqlDbType.VarChar, 8000)
            params(3).Value = customclass.SortBy

            params(4) = New SqlParameter(PARAM_APPROVALTYPEID, SqlDbType.VarChar, 4)
            params(4).Value = customclass.ApprovalTypeID

            params(5) = New SqlParameter(PARAM_REQUESTFROM, SqlDbType.VarChar, 12)
            params(5).Value = customclass.RequestFrom

            params(6) = New SqlParameter(PARAM_APPROVEDBY, SqlDbType.VarChar, 12)
            params(6).Value = customclass.ApproveBy

            params(7) = New SqlParameter(PARAM_APPROVALSTATUS, SqlDbType.VarChar, 1)
            params(7).Value = customclass.ApprovalStatus

            params(8) = New SqlParameter(PARAM_APPROVALRESULT, SqlDbType.VarChar, 1)
            params(8).Value = customclass.ApprovalResult

            params(9) = New SqlParameter(PARAM_BUSINESSDATE, SqlDbType.VarChar, 10)
            params(9).Value = customclass.BusinessDate.ToString("yyyyMMdd")

            params(10) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
            params(10).Value = customclass.BranchId

            params(11) = New SqlParameter(PARAM_TOTALRECORD, SqlDbType.Int)
            params(11).Direction = ParameterDirection.Output

            customclass.ApprovalList = SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALLISTPAGING, params).Tables(0)
            customclass.TotalRecord = CInt(params(11).Value)
            Return customclass
        Catch exp As Exception
            WriteException("ApprovalScreen", "ApprovalList", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Function
    Public Function ApprovalListHistory(ByVal customclass As Entities.ApprovalScreen) As System.Data.DataTable
        Dim params() As SqlParameter = New SqlParameter(1) {}
        Try
            params(0) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
            params(0).Value = customclass.ApprovalNo

            params(1) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
            params(1).Value = customclass.ApprovalSchemeID

            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALHISTORYLIST, params).Tables(0)
        Catch exp As Exception
            WriteException("ApprovalScreen", "ApprovalListHistory", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Function
    Public Function ApprovalHistory(ByVal customclass As Entities.ApprovalScreen) As System.Data.DataTable
        Dim params() As SqlParameter = New SqlParameter(1) {}
        Try
            params(0) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
            params(0).Value = customclass.ApprovalNo

            params(1) = New SqlParameter(PARAM_BUSINESSDATE, SqlDbType.DateTime)
            params(1).Value = customclass.BusinessDate

            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALHISTORY, params).Tables(0)
        Catch exp As Exception
            WriteException("ApprovalScreen", "ApprovalListHistory", exp.Message )
            Throw New Exception(exp.Message)
        End Try
    End Function
    Public Function ApprovalListTypeDescription(ByVal customclass As Entities.ApprovalScreen) As String
        Dim params() As SqlParameter = New SqlParameter(0) {}
        params(0) = New SqlParameter(PARAM_APPROVALTYPEID, SqlDbType.VarChar, 4)
        params(0).Value = customclass.ApprovalTypeID

        Try
            Return SqlHelper.ExecuteScalar(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALTYPEDESCRIPTION, params).ToString
        Catch exp As Exception
            WriteException("ApprovalScreen", "ApprovalListTypeDescription", exp.Message)
        End Try
    End Function

    Public Function ApprovalListUser(ByVal customclass As Entities.ApprovalScreen) As System.Data.DataTable
        Dim params() As SqlParameter = New SqlParameter(0) {}
        params(0) = New SqlParameter(PARAM_APPROVALTYPEID, SqlDbType.VarChar, 4)
        params(0).Value = customclass.ApprovalTypeID
        Try
            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALLISTUSER, params).Tables(0)
        Catch exp As Exception
            WriteException("ApprovalScreen", "ApprovalListUser", exp.Message )
        End Try
    End Function

    Public Function ApprovalScreenUserRequest(ByVal customclass As Entities.ApprovalScreen) As String
        Dim params() As SqlParameter = New SqlParameter(2) {}

        params(0) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
        params(0).Value = customclass.ApprovalNo

        params(1) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(1).Value = customclass.ApprovalSchemeID

        params(2) = New SqlParameter(PARAM_TRANSACTIONNO, SqlDbType.VarChar, 20)
        params(2).Value = customclass.TransactionNo
        Try
            Return SqlHelper.ExecuteScalar(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALSCREENUSERREQUEST, params).ToString.Trim
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ApprovalScreenViewScheme(ByVal customclass As Entities.ApprovalScreen) As DataTable
        Dim params() As SqlParameter = New SqlParameter(2) {}

        params(0) = New SqlParameter(PARAM_APPROVALTYPEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalTypeID
        params(1) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(1).Value = customclass.ApprovalSchemeID
        params(2) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
        params(2).Value = customclass.ApprovalNo
        Try
            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALSCREENVIEWSCHEME, params).Tables(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ApprovalScreenUserScheme(ByVal customclass As Entities.ApprovalScreen) As Entities.ApprovalScreen
        Dim params() As SqlParameter = New SqlParameter(1) {}
        Dim objread As SqlDataReader
        params(0) = New SqlParameter(PARAM_LOGINID, SqlDbType.VarChar, 12)
        params(0).Value = customclass.LoginId

        params(1) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(1).Value = customclass.ApprovalSchemeID
        Try
            objread = SqlHelper.ExecuteReader(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALSCREENUSERSCHEME, params)
            If objread.Read Then
                With customclass
                    .CanFinalReject = CBool(objread("CanFinalReject"))
                    .CanFinalApprove = CBool(objread("CanFinalApprove"))
                    .CanEscalation = CBool(objread("CanEscalation"))
                    .CanChangeFinalLevel = CBool(objread("CanChangeFinalLevel"))
                    .RejectAction = CStr(objread("RejectActioN"))
                    .ApprovalSeqNo = CInt(objread("ApprovalSeqnum"))
                End With
            End If
            objread.Close()
            Return customclass
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ApprovalScreenIsValidApproval(ByVal customclass As Entities.ApprovalScreen) As Entities.ApprovalScreen
        Dim params() As SqlParameter = New SqlParameter(2) {}
        Dim objread As SqlDataReader
        params(0) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
        params(0).Value = customclass.ApprovalNo

        params(1) = New SqlParameter(PARAM_TRANSACTIONNO, SqlDbType.VarChar, 20)
        params(1).Value = customclass.TransactionNo

        params(2) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 20)
        params(2).Value = customclass.ApprovalSchemeID
        Try
            objread = SqlHelper.ExecuteReader(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALSCREENISVALIDAPPROVAL, params)
            If objread.Read Then
                With customclass
                    .IsEverRejected = CBool(objread("IsEverRejected"))
                    .UserApproval = CStr(objread("UserApproval"))
                End With
            End If
            objread.Close()
            Return customclass
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ApprovalScreenSetLimit(ByVal customclass As Entities.ApprovalScreen) As Entities.ApprovalScreen
        Dim params() As SqlParameter = New SqlParameter(3) {}
        Dim objread As SqlDataReader

        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID

        params(1) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
        params(1).Value = customclass.ApprovalNo

        params(2) = New SqlParameter(PARAM_LOGINID, SqlDbType.VarChar, 12)
        params(2).Value = customclass.LoginId

        params(3) = New SqlParameter(PARAM_ISREREQUEST, SqlDbType.Bit)
        params(3).Value = customclass.IsReRequest
        Try
            objread = SqlHelper.ExecuteReader(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALSCREENSETLIMIT, params)
            If objread.Read Then
                With customclass
                    .IsFinal = CBool(objread("IsFinal"))
                    .AmountApproval = CDbl(objread("AmountApproval"))
                End With
            End If
            objread.Close()
            Return customclass
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub ApprovalHold(ByVal customclass As Entities.ApprovalScreen)
        Dim objCon As New SqlConnection(customclass.strConnection)
        Dim objTrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(0) {}
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objTrans = objCon.BeginTransaction

            params(0) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 8000)
            params(0).Value = customclass.ApprovalNo

            SqlHelper.ExecuteNonQuery(objTrans, CommandType.StoredProcedure, SPAPPROVALHOLD, params)
            objTrans.Commit()
        Catch ex As Exception
            objTrans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub

    Public Sub ApprovalTakeBack(ByVal customclass As Entities.ApprovalScreen)
        Dim objCon As New SqlConnection(customclass.strConnection)
        Dim objTrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(1) {}
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objTrans = objCon.BeginTransaction

            params(0) = New SqlParameter(PARAM_BUSINESSDATE, SqlDbType.DateTime)
            params(0).Value = customclass.BusinessDate

            params(1) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
            params(1).Value = customclass.ApprovalNo
            SqlHelper.ExecuteNonQuery(objTrans, CommandType.StoredProcedure, SPTAKEBACKAPPROVAL, params)
            objTrans.Commit()
        Catch ex As Exception
            objTrans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub

    Public Sub ApprovalUnHold(ByVal customclass As Entities.ApprovalScreen)
        Dim objCon As New SqlConnection(customclass.strConnection)
        Dim objTrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(0) {}
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objTrans = objCon.BeginTransaction

            params(0) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 8000)
            params(0).Value = customclass.ApprovalNo

            SqlHelper.ExecuteNonQuery(objTrans, CommandType.StoredProcedure, SPAPPROVALUNHOLD, params)
            objTrans.Commit()
        Catch ex As Exception
            objTrans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub

    Public Function ApprovalScreenMainUserRequest(ByVal customclass As Entities.ApprovalScreen) As String

        Dim params() As SqlParameter = New SqlParameter(2) {}

        params(0) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
        params(0).Value = customclass.ApprovalNo

        params(1) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(1).Value = customclass.ApprovalSchemeID

        params(2) = New SqlParameter(PARAM_TRANSACTIONNO, SqlDbType.VarChar, 20)
        params(2).Value = customclass.TransactionNo
        Try
            Return SqlHelper.ExecuteScalar(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALGETMAINUSERREQUEST, params).ToString.Trim
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ApprovalScreenMainUserApproval(ByVal customclass As Entities.ApprovalScreen) As String

        Dim params() As SqlParameter = New SqlParameter(2) {}

        params(0) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
        params(0).Value = customclass.ApprovalNo
        params(1) = New SqlParameter(PARAM_TRANSACTIONNO, SqlDbType.VarChar, 20)
        params(1).Value = customclass.TransactionNo
        params(2) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(2).Value = customclass.ApprovalSchemeID

     
        Try
            Return SqlHelper.ExecuteScalar(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALGETMAINUSERAPPROVAL, params).ToString.Trim
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ApprovalScreenIsEverRejected(ByVal customclass As Entities.ApprovalScreen) As Boolean

        Dim params() As SqlParameter = New SqlParameter(0) {}
        params(0) = New SqlParameter(PARAM_TRANSACTIONNO, SqlDbType.VarChar, 20)
        params(0).Value = customclass.TransactionNo
        Try
            Return CBool(SqlHelper.ExecuteScalar(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALSCREENISEVERREJECTED, params))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ApprovalScreenUserChangeFinalLevel(ByVal customclass As Entities.ApprovalScreen) As Entities.ApprovalScreen

        Dim params() As SqlParameter = New SqlParameter(3) {}
        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID
        params(1) = New SqlParameter(PARAM_APPROVALLOGINID, SqlDbType.VarChar, 12)
        params(1).Value = customclass.UserApproval
        params(2) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
        params(2).Value = customclass.ApprovalNo
        params(3) = New SqlParameter(PARAM_FINALLEVELAPPROVAL, SqlDbType.Int)
        params(3).Direction = ParameterDirection.Output

        Try
            With customclass
                .ListUserChangeFinalLevel = SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALCHANGEFINALLEVEL, params).Tables(0)
                .FinalLevelApproval = CInt(params(3).Value)
            End With
            Return customclass
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function ApprovalScreenFillCboApprReason(ByVal customclass As Entities.ApprovalScreen) As Entities.ApprovalScreen
        'Amelya 25062008 begin
        Dim params() As SqlParameter = New SqlParameter(1) {}
        params(0) = New SqlParameter("@TypeID", SqlDbType.VarChar, 4)
        params(0).Value = customclass.ApprovalTypeID
        params(1) = New SqlParameter("@Type", SqlDbType.VarChar, 3)
        params(1).Value = customclass.approvaltype

        Try
            customclass.ListApprovalReason = SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, "spGetApprovalReasonFillCbo", params).Tables(0)
            Return customclass
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        'Amelya 25062008 end
    End Function

    Public Function ApprovalScreenSetNextPerson(ByVal customclass As Entities.ApprovalScreen) As DataTable
        Dim params() As SqlParameter = New SqlParameter(6) {}
        params(0) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
        params(0).Value = customclass.BranchId

        params(1) = New SqlParameter(PARAM_BUSINESSDATE, SqlDbType.DateTime)
        params(1).Value = customclass.BusinessDate

        params(2) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(2).Value = customclass.ApprovalSchemeID
        params(3) = New SqlParameter(PARAM_APPROVALAMOUNT, SqlDbType.Decimal)
        params(3).Value = customclass.AmountApproval
        params(4) = New SqlParameter(PARAM_ISSTAGING, SqlDbType.Bit)
        params(4).Value = customclass.IsStaging
        params(5) = New SqlParameter(PARAM_APPROVALLOGINID, SqlDbType.VarChar, 12)
        params(5).Value = customclass.UserApproval
        'Pita 15 July 08
        params(6) = New SqlParameter(PARAM_ISBACKWARD, SqlDbType.Bit)
        params(6).Value = customclass.isBackWard
        '===============
        Try
            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALSCREENSETNEXTPERSON, params).Tables(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function ApprovalScreenCheckIsFinal(ByVal customclass As Entities.ApprovalScreen) As Entities.ApprovalScreen
        Dim params() As SqlParameter = New SqlParameter(5) {}
        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customclass.ApprovalSchemeID
        params(1) = New SqlParameter(PARAM_APPROVALLOGINID, SqlDbType.VarChar, 12)
        params(1).Value = customclass.UserApproval
        params(2) = New SqlParameter(PARAM_FINALLEVELAPPROVAL, SqlDbType.Int)
        params(2).Value = customclass.FinalLevelApproval
        params(3) = New SqlParameter(PARAM_ISFINALLEVEL, SqlDbType.Bit)
        params(3).Direction = ParameterDirection.Output
        params(4) = New SqlParameter(PARAM_ISFINALSTAGE, SqlDbType.Bit)
        params(4).Direction = ParameterDirection.Output
        params(5) = New SqlParameter(PARAM_APPROVALSEQUENCENO, SqlDbType.Int)
        params(5).Direction = ParameterDirection.Output
        Try
            SqlHelper.ExecuteNonQuery(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALSCREENCHECKISFINAL, params)
            With customclass
                .IsFinalLevel = CBool(params(3).Value)
                .IsFinalStage = CBool(params(4).Value)
                .ApprovalSeqNo = CInt(params(5).Value)
            End With
            Return customclass
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ApprovalScreen2NextPerson(ByVal customClass As Entities.ApprovalScreen) As DataTable
        Dim params() As SqlParameter = New SqlParameter(1) {}
        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
        params(0).Value = customClass.ApprovalSchemeID
        params(1) = New SqlParameter(PARAM_APPROVALSEQUENCENO, SqlDbType.Int)
        params(1).Value = customClass.ApprovalSeqNo
        Try
            Return SqlHelper.ExecuteDataset(customClass.strConnection, CommandType.StoredProcedure, SPAPPROVALSCREEN2NEXTPERSON, params).Tables(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub ApprovalScreenspApprovalRequestToNextPerson(ByVal customclass As Entities.ApprovalScreen)
        Dim objCon As New SqlConnection(customclass.strConnection)
        Dim objTrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(15) {}
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objTrans = objCon.BeginTransaction
            params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
            params(0).Value = customclass.ApprovalSchemeID

            params(1) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 8000)
            params(1).Value = customclass.ApprovalNo

            params(2) = New SqlParameter(PARAM_TRANSACTIONNO, SqlDbType.VarChar, 20)
            params(2).Value = customclass.TransactionNo

            params(3) = New SqlParameter(PARAM_USERREQUEST, SqlDbType.VarChar, 12)
            params(3).Value = customclass.UserRequest

            params(4) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
            params(4).Value = customclass.UserApproval

            params(5) = New SqlParameter(PARAM_ALTERNATEUSER, SqlDbType.VarChar, 12)
            params(5).Value = customclass.AlternateUser

            params(6) = New SqlParameter(PARAM_FINALLEVELAPPROVAL, SqlDbType.Int)
            params(6).Value = customclass.FinalLevelApproval

            params(7) = New SqlParameter(PARAM_APPROVALDATE, SqlDbType.DateTime)
            params(7).Value = customclass.ApprovalDate

            params(8) = New SqlParameter(PARAM_APPROVALRESULT, SqlDbType.VarChar, 1)
            params(8).Value = customclass.ApprovalResult

            params(9) = New SqlParameter(PARAM_APPROVALNOTES, SqlDbType.VarChar, 8000)
            params(9).Value = customclass.ApprovalNote

            params(10) = New SqlParameter(PARAM_SECURITYCODE, SqlDbType.VarChar, 50)
            params(10).Value = customclass.SecurityCode

            params(11) = New SqlParameter(PARAM_USERSECURITYCODE, SqlDbType.VarChar, 12)
            params(11).Value = customclass.UserSecurityCode

            params(12) = New SqlParameter(PARAM_ISEVERREJECTED, SqlDbType.Bit)
            params(12).Value = customclass.IsEverRejected

            params(13) = New SqlParameter(PARAM_ISFORWARD, SqlDbType.Bit)
            params(13).Value = customclass.IsForward

            params(14) = New SqlParameter(PARAM_ISREREQUEST, SqlDbType.Bit)
            params(14).Value = customclass.IsReRequest
            'Amelya 25062008 begin
            params(15) = New SqlParameter(PARAM_REASONID, SqlDbType.VarChar, 10)
            params(15).Value = customclass.AprReasonID
            'Amelya 25062008 end
            SqlHelper.ExecuteNonQuery(objTrans, CommandType.StoredProcedure, SPAPPROVALREQUESTTONEXTPERSON, params)
            objTrans.Commit()
        Catch ex As Exception
            objTrans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            oMailServices.SendMailNotification(customclass)
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub

    Public Sub ApprovalScreenspApprovalDecline(ByVal customclass As Entities.ApprovalScreen)
        Dim objCon As New SqlConnection(customclass.strConnection)
        Dim objTrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(14) {}
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objTrans = objCon.BeginTransaction

            params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
            params(0).Value = customclass.ApprovalSchemeID

            params(1) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 8000)
            params(1).Value = customclass.ApprovalNo

            params(2) = New SqlParameter(PARAM_TRANSACTIONNO, SqlDbType.VarChar, 20)
            params(2).Value = customclass.TransactionNo

            params(3) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
            params(3).Value = customclass.UserApproval

            params(4) = New SqlParameter(PARAM_APPROVALLOGINID, SqlDbType.VarChar, 12)
            params(4).Value = customclass.ApprovalLoginID

            params(5) = New SqlParameter(PARAM_FINALLEVELAPPROVAL, SqlDbType.VarChar, 8000)
            params(5).Value = customclass.FinalLevelApproval

            params(6) = New SqlParameter(PARAM_APPROVALDATE, SqlDbType.DateTime)
            params(6).Value = customclass.ApprovalDate

            params(7) = New SqlParameter(PARAM_APPROVALRESULT, SqlDbType.VarChar, 1)
            params(7).Value = customclass.ApprovalResult

            params(8) = New SqlParameter(PARAM_ISEVERREJECTED, SqlDbType.Bit)
            params(8).Value = customclass.IsEverRejected

            params(9) = New SqlParameter(PARAM_SECURITYCODE, SqlDbType.VarChar, 50)
            params(9).Value = customclass.SecurityCode

            params(10) = New SqlParameter(PARAM_USERSECURITYCODE, SqlDbType.VarChar, 12)
            params(10).Value = customclass.UserSecurityCode

            params(11) = New SqlParameter(PARAM_APPROVALNOTES, SqlDbType.VarChar, 8000)
            params(11).Value = customclass.ApprovalNote

            params(12) = New SqlParameter(PARAM_ISFORWARD, SqlDbType.Bit)
            params(12).Value = customclass.IsForward

            params(13) = New SqlParameter(PARAM_ISREREQUEST, SqlDbType.Bit)
            params(13).Value = customclass.IsReRequest
            'amelya 25062008 begin
            params(14) = New SqlParameter(PARAM_REASONID, SqlDbType.VarChar, 10)
            params(14).Value = customclass.AprReasonID
            'amelya 25062008 end
            SqlHelper.ExecuteNonQuery(objTrans, CommandType.StoredProcedure, SPAPPROVALDECLINE, params)
            objTrans.Commit()
        Catch ex As Exception
            objTrans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub

    Public Sub ApprovalScreenspApprovalFinal(ByVal customclass As Entities.ApprovalScreen)
        Dim objCon As New SqlConnection(customclass.strConnection)
        Dim objTrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(13) {}
        Try
            If objCon.State = ConnectionState.Closed Then objCon.Open()
            objTrans = objCon.BeginTransaction

            params(0) = New SqlParameter(PARAM_APPROVALTYPEID, SqlDbType.VarChar, 4)
            params(0).Value = customclass.ApprovalTypeID

            params(1) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
            params(1).Value = customclass.ApprovalSchemeID

            params(2) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 8000)
            params(2).Value = customclass.ApprovalNo

            params(3) = New SqlParameter(PARAM_APPROVALRESULT, SqlDbType.VarChar, 1)
            params(3).Value = customclass.ApprovalResult

            params(4) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
            params(4).Value = customclass.UserApproval

            params(5) = New SqlParameter(PARAM_MAINAPPROVALLOGINID, SqlDbType.VarChar, 12)
            params(5).Value = customclass.MainApprovalLoginID

            params(6) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
            params(6).Value = customclass.BranchId

            params(7) = New SqlParameter(PARAM_APPROVALDATE, SqlDbType.DateTime)
            params(7).Value = customclass.ApprovalDate

            params(8) = New SqlParameter(PARAM_APPROVALNOTES, SqlDbType.VarChar, 8000)
            params(8).Value = customclass.ApprovalNote

            params(9) = New SqlParameter(PARAM_SECURITYCODE, SqlDbType.VarChar, 50)
            params(9).Value = customclass.SecurityCode

            params(10) = New SqlParameter(PARAM_USERSECURITYCODE, SqlDbType.VarChar, 12)
            params(10).Value = customclass.UserSecurityCode

            params(11) = New SqlParameter(PARAM_ISEVERREJECTED, SqlDbType.Bit)
            params(11).Value = customclass.IsEverRejected

            params(12) = New SqlParameter(PARAM_TRANSACTIONNO, SqlDbType.VarChar, 20)
            params(12).Value = customclass.TransactionNo
            'Amelya 25062008 begin
            params(13) = New SqlParameter(PARAM_REASONID, SqlDbType.VarChar, 10)
            params(13).Value = customclass.AprReasonID
            'Amelya 25062008 end
            SqlHelper.ExecuteNonQuery(objTrans, CommandType.StoredProcedure, SPAPPROVALSCREENSPAPPROVALFINAL, params)
            objTrans.Commit()
        Catch ex As Exception
            objTrans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Dispose()
        End Try
    End Sub

End Class
