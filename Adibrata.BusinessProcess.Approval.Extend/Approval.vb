#Region "Revision History"
'$Header: /Clipan/AdIns.Application.Eloan.DataAccess.Approval/Approval.vb 50    12/13/06 6:44p Kristina $
'$Workfile: Approval.vb $
'-----------------------------------------------------------
'$Log: /Clipan/AdIns.Application.Eloan.DataAccess.Approval/Approval.vb $
'
'50    12/13/06 6:44p Kristina
'
'49    12/12/06 7:25p Irfan
'
'48    11/25/06 4:22p Henry
'
'47    11/24/06 6:53p Anton
'
'46    11/16/06 6:41p Anton
'
'45    10/26/06 1:34p Anton
'
'44    10/19/06 6:25p Anton
'
'43    5/24/06 10:13a Yovita
'
'42    24-05-06 6:25p Emilia
'
'41    5/19/06 5:03a Yovita
'
'40    5/17/06 5:37p Anton
'
'39    4/28/06 5:27p Isabel
'
'38    4/28/06 12:04p Isabel
'
'37    4/24/06 12:04p Isabel
'
'36    4/24/06 9:38a Isabel
'
'35    4/24/06 9:22a Isabel
'
'34    12/16/05 2:24p Yovita
'
'33    8/04/05 12:05p Johnson
'
'32    6/10/05 12:08p Henry
'
'31    1/12/05 2:37p Rony
'
'30    10/30/04 1:10p Henry
'
'29    9/20/04 7:39p Henry
'
'28    9/17/04 10:37a Henry
'
'27    9/16/04 8:00p Henry
'
'26    9/14/04 5:53p Henry
'
'25    9/08/04 1:15p Henry
'
'24    7/29/04 4:22p Eric
'
'23    7/07/04 9:12a Eric
'
'22    4/19/04 2:36p Henry
'
'21    4/19/04 2:32p Henry
'  
'  20    4/06/04 10:10a Henry
'  
'  19    3/16/04 6:49p Henry
'  
'  18    2/19/04 7:36p Lina
'  
'  17    1/29/04 3:13p Henry
'  
'  16    1/14/04 3:11p Henry
'  
'  15    12/05/03 3:38p Henry
'  
'  14    12/04/03 3:50p Henry
'  
'  13    11/18/03 4:14p Henry
'  
'  12    11/13/03 4:34p Anita
'  
'  11    11/07/03 7:43p Henry
'  
'  10    9/18/03 8:16p Henry
'  
'  9     9/01/03 5:29p Anita
'  
'  8     9/01/03 4:25p Anita
'  
'  7     9/01/03 3:43p Anita
'  
'  6     9/01/03 3:11p Anita
'  
'  5     8/29/03 3:31p Anita
'  
'  4     8/23/03 5:20p Henry
'  
'  3     8/21/03 6:56p Henry
'  
'  2     8/20/03 3:49p Henry
'  
'  1     8/20/03 2:13p Henry
'-----------------------------------------------------------
#End Region

#Region "Imports"
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient
Imports AdIns.Application.Eloan.Business
Imports AdIns.Application.Eloan
Imports AdIns.FrameWork.DataAccess
'Imports AdIns.Application.Eloan.Exceptions
'Imports AdIns.Framework.Email
#End Region

Public Class Approval : Inherits AdIns.Application.Eloan.DataAccess.DataAccessBase
    Private Const spApprovalUserList As String = "spApprovalUserList"
    Private Const spCheckApprovalID As String = "spApprovalCheckApprovalID"
    Private Const APPROVALREQUEST As String = "spApprovalRequest"
    Private Const spApprovalUserAlternateList As String = "spApprovalUserAlternateList"

    Private Const SPAPPROVALHMVIEW As String = "spApprovalHMView"
    Private Const SPAPPROVALHMADD As String = "spApprovalHMAdd"
    Private Const SPAPPROVALHMEDIT As String = "spApprovalHMEdit"
    Private Const SPAPPROVALHMSAVE As String = "spAgreementApprovalHMSave"
    Private Const SPAPPROVALHMDELETE As String = "spAgreementApprovalHMDelete"

    Private Const PARAM_APPROVALHMSEQ As String = "@HMSeq"
    Private Const PARAM_APPROVALHMNAME As String = "@HMName"
    Private Const PARAM_APPROVALHMISACTIVE As String = "@IsActive"
    Private Const PARAM_APPLICATIONID As String = "@ApplicationID"

    Private Const PARAM_SCHEMEID As String = "@ApprovalSchemeID"
    Private Const PARAM_AMOUNT As String = "@Amount"
    Private Const PARAM_APPROVALID As String = "@ApprovalID"
    Private Const PARAM_USERAPPROVAL As String = "@UserApproval"
    Private Const PARAM_ISEXISTS As String = "@ISExists"
    Private Const PARAM_USERALTERNATE As String = "@UserAlternate"
    Private Const PARAM_TRANSACTIONTYPE As String = "@TransactionType"
    Private Const PARAM_REQUESTDATE As String = "@requestdate"
    Private Const PARAM_USERREQUEST As String = "@userrequest"
    Private Const PARAM_APPROVALVALUE As String = "@approvalvalue"
    Private Const PARAM_TRANSACTIONNO As String = "@transactionno"
    Private Const PARAM_APPROVALNOTE As String = "@approvalnote"
    Private Const PARAM_APPROVALNO As String = "@approvalno"
    Private Const PARAM_APPROVALMAILNOTIFICATION As String = "spApprovalMailNotification"

    Private oMailServices As New ApprovalMailNotification


    Public Function GetApprovalSchemeID(ByVal customclass As Entities.ApprovalScheme) As Entities.ApprovalScheme

        Dim params() As SqlParameter = New SqlParameter(2) {}
        Try
            params(0) = New SqlParameter("@ApprovalTypeID", SqlDbType.VarChar, 4)
            params(0).Value = customclass.ApprovalTypeID

            params(1) = New SqlParameter("@ProductID", SqlDbType.VarChar, 10)
            params(1).Value = customclass.ProductID

            params(2) = New SqlParameter("@ApprovalSchemeID", SqlDbType.VarChar, 10)
            params(2).Direction = ParameterDirection.Output

            SqlHelper.ExecuteNonQuery(customclass.strConnection, CommandType.StoredProcedure, "spGetApprovalSchemeID", params)
            customclass.SchemeID = CStr(params(2).Value)
            Return customclass
        Catch exp As Exception
            WriteException("Approval", "GetUserApproval", exp.Message)
        End Try

    End Function

    Public Function GetUserApproval(ByVal customclass As Entities.Approval) As System.Data.DataTable

        Dim params() As SqlParameter = New SqlParameter(3) {}
        Try
            params(0) = New SqlParameter(PARAM_SCHEMEID, SqlDbType.VarChar, 5)
            params(0).Value = customclass.SchemeID

            params(1) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
            params(1).Value = customclass.BranchId

            params(2) = New SqlParameter(PARAM_AMOUNT, SqlDbType.Decimal)
            params(2).Value = customclass.AmountApproval

            params(3) = New SqlParameter(PARAM_BUSINESSDATE, SqlDbType.DateTime)
            params(3).Value = customclass.BusinessDate

            If customclass.SpName = "" Or customclass.SpName = "-" Then
                customclass.SpName = "spApprovalUserList"
            End If
            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, customclass.SpName, params).Tables(0)
        Catch exp As Exception
            WriteException("Approval", "GetUserApproval", exp.Message)
        End Try
    End Function

    Public Function GetUserAlternate(ByVal customclass As Entities.Approval) As System.Data.DataTable

        Dim params() As SqlParameter = New SqlParameter(3) {}
        Try
            params(0) = New SqlParameter(PARAM_SCHEMEID, SqlDbType.VarChar, 5)
            params(0).Value = customclass.SchemeID

            params(1) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
            params(1).Value = customclass.BranchId

            params(2) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
            params(2).Value = customclass.UserApproval

            params(3) = New SqlParameter(PARAM_BUSINESSDATE, SqlDbType.DateTime)
            params(3).Value = customclass.BusinessDate

            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, spApprovalUserAlternateList, params).Tables(0)
        Catch exp As Exception
            WriteException("Approval", "GetUserApproval", exp.Message)
        End Try
    End Function

    Public Function CheckApprovalID(ByVal strConnection As SqlClient.SqlTransaction, ByVal ApprovalID As String, ByVal UserApproval As String) As Boolean

        Dim params() As SqlParameter = New SqlParameter(2) {}
        Try
            params(0) = New SqlParameter(PARAM_APPROVALID, SqlDbType.VarChar, 4)
            params(0).Value = ApprovalID

            params(1) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
            params(1).Value = UserApproval

            params(2) = New SqlParameter(PARAM_ISEXISTS, SqlDbType.Bit)
            params(2).Direction = ParameterDirection.Output

            SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, spCheckApprovalID, params)
            Return CBool(params(2).Value)
        Catch exp As Exception
            WriteException("Approval", "CheckApprovalID", exp.Message)
        End Try
    End Function

#Region "Type Transaction"
    Private Function Convert_ETransactionType(ByVal pBytEnum As Integer) As String
        Select Case pBytEnum
            Case 1
                Return "RCA_"
            Case 2
                Return "PPAY"
            Case 3
                Return "RFND"
            Case 4
                Return "BNS_"
            Case 5
                Return "NA__"
            Case 6
                Return "WO__"
            Case 7
                Return "RESC"
            Case 8
                Return "APPR"
            Case 9
                Return "NSTR"
            Case 10
                Return "ASRP"
            Case 11
                Return "DF__"
            Case 12
                Return "CC__"
            Case 13
                Return "CINV"
            Case 14
                Return "ACDD"
            Case 15
                Return "WA__"
            Case 16
                Return "AGTF"
            Case 17
                Return "CLEX"
            Case 18
                Return "RADV"
            Case 19
                Return "SELL"
            Case 20
                Return "CADV"
            Case 21
                Return "PAPY"
            Case 22
                Return "ASRL"
            Case 23
                Return "REST"
            Case 25
                Return "EST"
            Case 26
                Return "ISLP"
            Case 27
                Return "LOES"
            Case 28
                Return "LOSE"
            Case 29
                Return "BUEX"
            Case 30
                Return "PCRV"
            Case 31
                Return "PYRV"
            Case 32
                Return "ECAD"
            Case 33
                Return "ECAR"
        End Select

    End Function
#End Region

    Public Function RequestForApproval(ByVal customclass As Entities.Approval) As String
        Dim params() As SqlParameter = New SqlParameter(12) {}
        Try
            With customclass
                params(0) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
                params(0).Value = .BranchId

                params(1) = New SqlParameter(PARAM_TRANSACTIONTYPE, SqlDbType.VarChar, 50)
                params(1).Value = Convert_ETransactionType(CType(.AprovalType, Integer))

                params(2) = New SqlParameter(PARAM_APPROVALID, SqlDbType.VarChar, 4)
                params(2).Value = Get_ApprovalID(customclass.ApprovalTransaction, customclass.UserApproval)

                params(3) = New SqlParameter(PARAM_SCHEMEID, SqlDbType.VarChar, 5)
                params(3).Value = .SchemeID

                params(4) = New SqlParameter(PARAM_REQUESTDATE, SqlDbType.DateTime)
                params(4).Value = .RequestDate

                params(5) = New SqlParameter(PARAM_USERREQUEST, SqlDbType.VarChar, 12)
                params(5).Value = .UserRequest

                params(6) = New SqlParameter(PARAM_USERALTERNATE, SqlDbType.VarChar, 12)
                params(6).Value = .UserAlternate

                params(7) = New SqlParameter(PARAM_APPROVALVALUE, SqlDbType.Decimal)
                params(7).Value = .ApprovalValue

                params(8) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
                params(8).Value = .UserApproval

                params(9) = New SqlParameter(PARAM_TRANSACTIONNO, SqlDbType.VarChar, 20)
                params(9).Value = .TransactionNo

                params(10) = New SqlParameter(PARAM_APPROVALNOTE, SqlDbType.VarChar, 8000)
                params(10).Value = .ApprovalNote

                params(11) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
                params(11).Direction = ParameterDirection.Output

                params(12) = New SqlParameter(PARAM_STRERROR, SqlDbType.VarChar, 50)
                params(12).Direction = ParameterDirection.Output
            End With

            SqlHelper.ExecuteNonQuery(customclass.ApprovalTransaction, CommandType.StoredProcedure, APPROVALREQUEST, params)

            If CStr(params(12).Value) <> "" Then
                Throw New Exception("Approval No Already exists")
            End If
            Return CStr(params(11).Value)
        Catch exp As Exception
            WriteException("Approval", "RequestForApproval", exp.Message)
            Throw New Exception(exp.Message)
            customclass.ApprovalTransaction.Rollback()
        Finally

            customclass.ApprovalNo = CStr(params(11).Value)

            '---------------------------------- Anton 20-10-2006 ----------------------------------
            Dim IsSendMailNotification As String
            Dim objread As SqlDataReader
            IsSendMailNotification = ""
            objread = SqlHelper.ExecuteReader(customclass.ApprovalTransaction, CommandType.StoredProcedure, "spIsSendMailNotification")

            If objread.Read Then
                IsSendMailNotification = CStr(objread("GSValue"))
            End If

            objread.Close()

            If IsSendMailNotification = "1" Then
                oMailServices.SendMailNotification(customclass)
            End If
            '--------------------------------------------------------------------------------------

            'oMailServices.SendMailNotification(customclass)
        End Try
    End Function

    Public Function RequestForApprovalJump(ByVal customclass As Entities.Approval) As String
        Dim params() As SqlParameter = New SqlParameter(12) {}
        Try
            With customclass
                params(0) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
                params(0).Value = .BranchId

                params(1) = New SqlParameter(PARAM_TRANSACTIONTYPE, SqlDbType.VarChar, 12)
                params(1).Value = Convert_ETransactionType(CType(.AprovalType, Integer))

                params(2) = New SqlParameter(PARAM_APPROVALID, SqlDbType.VarChar, 4)
                params(2).Value = Get_ApprovalID(customclass.ApprovalTransaction, customclass.UserApproval)

                params(3) = New SqlParameter(PARAM_SCHEMEID, SqlDbType.VarChar, 5)
                params(3).Value = .SchemeID

                params(4) = New SqlParameter(PARAM_REQUESTDATE, SqlDbType.DateTime)
                params(4).Value = .RequestDate

                params(5) = New SqlParameter(PARAM_USERREQUEST, SqlDbType.VarChar, 12)
                params(5).Value = .UserRequest

                params(6) = New SqlParameter(PARAM_USERALTERNATE, SqlDbType.VarChar, 12)
                params(6).Value = .UserAlternate

                params(7) = New SqlParameter(PARAM_APPROVALVALUE, SqlDbType.Decimal)
                params(7).Value = .ApprovalValue

                params(8) = New SqlParameter(PARAM_USERAPPROVAL, SqlDbType.VarChar, 12)
                params(8).Value = .UserApproval

                params(9) = New SqlParameter(PARAM_TRANSACTIONNO, SqlDbType.VarChar, 20)
                params(9).Value = .TransactionNo

                params(10) = New SqlParameter(PARAM_APPROVALNOTE, SqlDbType.VarChar, 8000)
                params(10).Value = .ApprovalNote

                params(11) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
                params(11).Direction = ParameterDirection.Output

                params(12) = New SqlParameter(PARAM_STRERROR, SqlDbType.VarChar, 50)
                params(12).Direction = ParameterDirection.Output
            End With

            SqlHelper.ExecuteNonQuery(customclass.ApprovalTransaction, CommandType.StoredProcedure, "spApprovalRequestJump", params)

            If CStr(params(12).Value) <> "" Then
                Throw New Exception("Approval No is not found")
            End If
            Return CStr(params(11).Value)
        Catch exp As Exception
            WriteException("Approval", "RequestForApproval", exp.Message)
            Throw New Exception(exp.Message)
            customclass.ApprovalTransaction.Rollback()
        Finally
            customclass.ApprovalNo = CStr(params(11).Value)

            '---------------------------------- Anton 20-10-2006 ----------------------------------
            Dim IsSendMailNotification As String
            Dim objread As SqlDataReader

            objread = SqlHelper.ExecuteReader(customclass.ApprovalTransaction, CommandType.StoredProcedure, "spIsSendMailNotification")

            If objread.Read Then
                IsSendMailNotification = CStr(objread("GSValue"))
            End If

            objread.Close()

            If IsSendMailNotification = "1" Then
                oMailServices.SendMailNotification(customclass)
            End If
            '--------------------------------------------------------------------------------------


            'oMailServices.SendMailNotification(customclass)
        End Try
    End Function
#Region "ApprovalID"
    Public Function Get_ApprovalID(ByVal strConnection As SqlClient.SqlTransaction, _
                    ByVal UserApproval As String) As String
        Dim lObjKey As New ArrayList
        Dim lObjRand As New Random
        Dim strApprovalID As String
        Dim lStrCode As String
        Dim lBytCounter As Byte
        Dim lBlnOK As Boolean = False

        Dim DACheckApprovalID As New AdIns.Application.Eloan.DataAccess.Approval.Approval
        Try
            lObjKey.Add("A")
            lObjKey.Add("D")
            lObjKey.Add("G")
            lObjKey.Add("J")
            lObjKey.Add("M")
            lObjKey.Add("P")
            lObjKey.Add("T")
            lObjKey.Add("W")
            While Not lBlnOK
                strApprovalID = ""
                lStrCode = ""
                For lBytCounter = 1 To 4
                    strApprovalID &= CStr(lObjKey(lObjRand.Next(1, 8)))
                Next
                If Not CheckApprovalID(strConnection, strApprovalID, UserApproval) Then
                    If lStrCode = "" Then lBlnOK = True
                End If
            End While
            Return strApprovalID
        Catch exp As Exception
            WriteException("Approval", "Get_ApprovalID", exp.Message)
        End Try
    End Function
#End Region

#Region "Approval Highlight Message"
    Public Sub ApprovalHMAdd(ByVal customclass As Entities.Approval)
        Dim objcon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction

        Dim params() As SqlParameter = New SqlParameter(1) {}
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        objtrans = objcon.BeginTransaction
        Try
            params(0) = New SqlParameter(PARAM_APPROVALHMNAME, SqlDbType.VarChar, 50)
            params(0).Value = customclass.ApprovalHMName

            params(1) = New SqlParameter(PARAM_APPROVALHMISACTIVE, SqlDbType.VarChar, 1)
            params(1).Value = customclass.ApprovalHMIsActive

           
            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALHMADD, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("Approval", "ApprovalHMAdd", exp.Message)
            Throw New Exception(exp.Message)
        End Try
    End Sub

    Public Sub ApprovalHMEdit(ByVal customclass As Entities.Approval)
        Dim objcon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction

        Dim params() As SqlParameter = New SqlParameter(2) {}
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        objtrans = objcon.BeginTransaction
        Try
            params(0) = New SqlParameter(PARAM_APPROVALHMSEQ, SqlDbType.VarChar, 4)
            params(0).Value = customclass.ApprovalHMSeq

            params(1) = New SqlParameter(PARAM_APPROVALHMNAME, SqlDbType.VarChar, 50)
            params(1).Value = customclass.ApprovalHMName

            params(2) = New SqlParameter(PARAM_APPROVALHMISACTIVE, SqlDbType.VarChar, 1)
            params(2).Value = customclass.ApprovalHMIsActive

        
            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALHMEDIT, params)
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("Approval", "ApprovalHMEdit", exp.Message)
            Throw New Exception(exp.Message)

        Finally
            If objcon.State = ConnectionState.Open Then objcon.Close()
            objcon.Dispose()
        End Try

    End Sub
    Public Function ApprovalHMView(ByVal customclass As Entities.Approval) As Entities.Approval
        'Dim oReturnValue As Entities.Approval
        Dim params() As SqlParameter = New SqlParameter(4) {}
        Try
            params(0) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
            params(0).Value = customclass.BranchId

            params(1) = New SqlParameter(PARAM_APPLICATIONID, SqlDbType.VarChar, 20)
            params(1).Value = customclass.AppID

            params(2) = New SqlParameter("@LoginID", SqlDbType.VarChar, 20)
            params(2).Value = customclass.LoginId

            params(3) = New SqlParameter("@BranchName", SqlDbType.VarChar, 50)
            params(3).Direction = ParameterDirection.Output

            params(4) = New SqlParameter("@Path", SqlDbType.VarChar, 50)
            params(4).Direction = ParameterDirection.Output

            customclass.ApprovalHMListData = SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALHMVIEW, params).Tables(0)
            customclass.BranchName = CType(params(3).Value, String)
            customclass.Path = CType(params(4).Value, String)
            Return customclass
        Catch exp As Exception
            WriteException("ApprovalHM", "ApprovalHMView", exp.Message)
            Throw New Exception(exp.Message)
        End Try
    End Function
    Public Function ApprovalHMViewAgreement(ByVal customclass As Entities.Approval) As Entities.Approval
        'Dim oReturnValue As Entities.Approval
        Dim params() As SqlParameter = New SqlParameter(2) {}
        Try
            params(0) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
            params(0).Value = customclass.BranchId

            params(1) = New SqlParameter(PARAM_APPLICATIONID, SqlDbType.VarChar, 20)
            params(1).Value = customclass.AppID

            params(2) = New SqlParameter("@BranchName", SqlDbType.VarChar, 50)
            params(2).Direction = ParameterDirection.Output

            customclass.ApprovalHMListData = SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, "spApprovalHMViewAgreement", params).Tables(0)
            customclass.BranchName = CType(params(2).Value, String)

            Return customclass
        Catch exp As Exception
            WriteException("ApprovalHM", "ApprovalHMViewAgreement", exp.Message)
            Throw New Exception(exp.Message)
        End Try
    End Function
#End Region

#Region "Agreement Approval Highlight Message"
    Public Sub ApprovalHMSave(ByVal customclass As Entities.Approval)
        Dim objcon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction

        Dim Table As New DataTable
        Dim Row As DataRow
        Dim i As Integer

        Table = customclass.ApprovalHMListData
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        objtrans = objcon.BeginTransaction
        Dim params() As SqlParameter = New SqlParameter(6) {}
        Try
            params(0) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
            params(1) = New SqlParameter(PARAM_APPLICATIONID, SqlDbType.VarChar, 20)
            params(2) = New SqlParameter(PARAM_APPROVALHMSEQ, SqlDbType.VarChar, 4)
            params(3) = New SqlParameter("@IsCheck", SqlDbType.VarChar, 1)
            params(4) = New SqlParameter("@UserRequest", SqlDbType.VarChar, 50)
            params(5) = New SqlParameter("@Path", SqlDbType.VarChar, 50)
            params(6) = New SqlParameter("@Value", SqlDbType.VarChar, 50)
           
            For i = 0 To Table.Rows.Count() - 1
                Row = Table.Rows(i)
                params(0).Value = Table.Rows(i).Item("BranchID")
                params(1).Value = Table.Rows(i).Item("ApplicationID")
                params(2).Value = Table.Rows(i).Item("HMSeq")
                params(3).Value = Table.Rows(i).Item("IsCheck")
                params(4).Value = Table.Rows(i).Item("UserRequest")
                params(5).Value = Table.Rows(i).Item("Path")
                params(6).Value = Table.Rows(i).Item("Value")
                SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALHMDELETE, params)
                If params(3).Value Is "1" Then
                    SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALHMSAVE, params)
                End If
            Next i
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("Approval", "ApprovalHMUpdate", exp.Message)
            Throw New Exception(exp.Message)
        End Try
    End Sub
#End Region

End Class
