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
Imports AdIns.Framework.Email
#End Region
Public Class ApprovalMailNotification : Inherits AdIns.Application.Eloan.DataAccess.DataAccessBase
    Private Const PARAM_APPROVALMAILNOTIFICATION As String = "spApprovalMailNotification"
    Private Const PARAM_APPROVALNO As String = "@approvalno"
    Private oMailServices As New eMailServices

    Private _agreementno As String
    Private _customername As String
    Private _amountapproval As Double
    Private _approvalno As String
    Private _from As String
    Private _mailserver As String
    Private _userrequest As String
    Private _userapproval As String
    Private _approvaltype As String
    Private _emaildestination As String
    Private _urleloan As String

    Public Sub SendMailNotification(ByVal customclass As Entities.Approval)
        Dim objcon As New SqlConnection(customclass.strConnection)
        Dim objtrans As SqlTransaction
        Dim params() As SqlParameter = New SqlParameter(0) {}

        Dim dtMailNotification As DataTable
        Try
            With customclass
                params(0) = New SqlParameter(PARAM_APPROVALNO, SqlDbType.VarChar, 50)
                params(0).Value = customclass.ApprovalNo
            End With

            If objcon.State = ConnectionState.Closed Then objcon.Open()
            objtrans = objcon.BeginTransaction
            dtMailNotification = SqlHelper.ExecuteDataset(objtrans, CommandType.StoredProcedure, PARAM_APPROVALMAILNOTIFICATION, params).Tables(0)
            If dtMailNotification.Rows.Count > 0 Then
                If CStr(dtMailNotification.Rows(0).Item("EmailAddress")) <> "" Then
                    With dtMailNotification.Rows(0)
                        _emaildestination = CStr(.Item("EmailAddress"))
                        _from = CStr(.Item("UserRequest"))
                        _mailserver = CStr(.Item("ServerName"))
                        _approvaltype = CStr(.Item("ApprovalTypeDesc"))
                        _amountapproval = CDbl(.Item("ApprovalValue"))
                        _agreementno = CStr(.Item("AgreementNo"))
                        _customername = CStr(.Item("CustomerName"))
                        _urleloan = CStr(.Item("URLEloan"))
                        _approvalno = customclass.ApprovalNo
                        _userrequest = CStr(.Item("UserRequest"))
                    End With
                    With oMailServices
                        .Destination = _emaildestination
                        .From = _from
                        .Messages = EmailMessages()
                        .ServerName = _mailserver
                        .Subject = "Approval Notification -> Request From " + _userrequest + " to Approve '" + _approvaltype + "'"
                        .SendMail()
                    End With
                End If
            End If
            objtrans.Commit()
        Catch exp As Exception
            objtrans.Rollback()
            WriteException("Approval", "ApprovalMailNotifiaction", exp.Message)
            Throw New Exception(exp.Message)
        End Try
    End Sub

    Private Function EmailMessages() As String
        Dim strEmail As New StringBuilder
        strEmail.Append("<body lang=EN-US link=blue vlink=purple style='tab-interval:36.0pt'>")
        strEmail.Append("<div class=Section1>")
        strEmail.Append("<p class=MsoNormal>" + Now.ToString() + " <o:p></o:p></p>")
        strEmail.Append("<p class=MsoNormal>Dear <span class=GramE>Sir/Madam</span>, <o:p></o:p></p>")
        strEmail.Append("<p class=MsoNormal>This email is just a notification for approval request.<o:p></o:p></p>")
        strEmail.Append("<p class=MsoNormal>Approval Type : " + _approvaltype + " <o:p></o:p></p>")
        strEmail.Append("<p class=MsoNormal>Agreement No : " + _agreementno + " <o:p></o:p></p>")
        strEmail.Append("<p class=MsoNormal>Customer Name : " + _customername + "</p>")
        strEmail.Append("<p class=MsoNormal>Amount To Be Approved : " + FormatNumber(_amountapproval, 2) + " </span><o:p></o:p></p>")
        strEmail.Append("<p class=MsoNormal>Approval No : " + _approvalno + " </span><o:p></o:p></p>")
        strEmail.Append("<p class=MsoNormal><o:p>&nbsp;</o:p></p>")
        strEmail.Append("<p class=MsoNormal>Please process it on <a ")
        strEmail.Append("href=""" + _urleloan + """>" + _urleloan + "</a>")
        strEmail.Append("<o:p></o:p></p>")
        strEmail.Append("<p class=MsoNormal><o:p>&nbsp;</o:p></p>")
        strEmail.Append("<p class=MsoNormal><o:p>&nbsp;</o:p></p>")
        strEmail.Append("<p class=MsoNormal>Thanks<o:p></o:p></p>")
        strEmail.Append("<p class=MsoNormal><o:p>&nbsp;</o:p></p>")
        strEmail.Append("<p class=MsoNormal><o:p>&nbsp;</o:p></p>")
        strEmail.Append("<p class=MsoNormal>Approval Notification<o:p></o:p></p>")
        strEmail.Append("</div>")
        Return strEmail.ToString
    End Function
End Class
