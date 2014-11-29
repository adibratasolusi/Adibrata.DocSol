using Adibrata.BusinessProcess.Approval.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using Adibrata.BusinessProcess.Approval.Core;

namespace Adibrata.BusinessProcess.Approval.Extend
{
    public class ApprovalScheme
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");
        SqlTransaction _trans;

        public virtual void ApprSchemeAddEdit(ApprovalEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(Connectionstring);
            SqlParameter[] sqlParams;
            if (_ent.IsEdit == true) { sqlParams = new SqlParameter[28]; } else { sqlParams = new SqlParameter[27]; }
            try
            {
                // Approval Scheme ID Tidak Perlu karena menggunakan Identity

                #region "List Parameter SQL"
                sqlParams[0] = new SqlParameter("@AprvSchemeCode", SqlDbType.VarChar, 50);
                sqlParams[0].Value = _ent.ApprovalSchemeCode;

                sqlParams[1] = new SqlParameter("@AprvTypeID", SqlDbType.Int);
                sqlParams[1].Value = _ent.ApprovalTypeID;

                sqlParams[2] = new SqlParameter("@AprvSchemeDesc", SqlDbType.Int);
                sqlParams[2].Value = _ent.ApprovalSchemeDesc;

                sqlParams[3] = new SqlParameter("@IsStaging", SqlDbType.Int);
                sqlParams[3].Value = _ent.IsStaging;

                sqlParams[4] = new SqlParameter("@IsLimited", SqlDbType.Int);
                sqlParams[4].Value = _ent.IsLimited;

                sqlParams[5] = new SqlParameter("@DocumentLabel", SqlDbType.VarChar, 50);
                sqlParams[5].Value = _ent.DocumentLabel;

                sqlParams[6] = new SqlParameter("@DocumentLink", SqlDbType.VarChar, 100);
                sqlParams[6].Value = _ent.DocumentLink;

                sqlParams[7] = new SqlParameter("@Label1", SqlDbType.VarChar, 50);
                sqlParams[7].Value = _ent.Label1;

                sqlParams[8] = new SqlParameter("@Lable2", SqlDbType.VarChar, 50);
                sqlParams[8].Value = _ent.Label2;

                sqlParams[9] = new SqlParameter("@LinkLabel1", SqlDbType.VarChar, 100);
                sqlParams[9].Value = _ent.LinkLable1;

                sqlParams[10] = new SqlParameter("@LinkLabel2", SqlDbType.VarChar, 50);
                sqlParams[10].Value = _ent.LinkLable2;

                sqlParams[11] = new SqlParameter("@MethodLabel1", SqlDbType.VarChar, 50);
                sqlParams[11].Value = _ent.MethodLable1;

                sqlParams[12] = new SqlParameter("@MethodLabel2", SqlDbType.VarChar, 50);
                sqlParams[12].Value = _ent.MethodLable2;

                sqlParams[13] = new SqlParameter("@LabelNote", SqlDbType.VarChar, 50);
                sqlParams[13].Value = _ent.LabelNote;

                sqlParams[14] = new SqlParameter("@MethodNote", SqlDbType.VarChar, 50);
                sqlParams[14].Value = _ent.MethodNote;

                sqlParams[15] = new SqlParameter("@LimitLabel", SqlDbType.VarChar, 30);
                sqlParams[15].Value = _ent.LimitLabel;

                sqlParams[16] = new SqlParameter("@OtherLink1", SqlDbType.VarChar, 50);
                sqlParams[16].Value = _ent.OtherLink1;

                sqlParams[17] = new SqlParameter("@OtherLink2", SqlDbType.VarChar, 50);
                sqlParams[17].Value = _ent.OtherLink2;

                sqlParams[18] = new SqlParameter("@OtherLink3", SqlDbType.VarChar, 50);
                sqlParams[18].Value = _ent.OtherLink3;

                sqlParams[19] = new SqlParameter("@OtherLink4", SqlDbType.VarChar, 50);
                sqlParams[19].Value = _ent.OtherLink4;

                sqlParams[20] = new SqlParameter("@OtherLink5", SqlDbType.VarChar, 50);
                sqlParams[20].Value = _ent.OtherLink5;

                sqlParams[21] = new SqlParameter("@OtherLinkUrl1", SqlDbType.VarChar, 100);
                sqlParams[21].Value = _ent.OtherLinkUrl1;

                sqlParams[22] = new SqlParameter("@OtherLinkUrl2", SqlDbType.VarChar, 100);
                sqlParams[22].Value = _ent.OtherLinkUrl2;

                sqlParams[23] = new SqlParameter("@OtherLinkUrl3", SqlDbType.VarChar, 100);
                sqlParams[23].Value = _ent.OtherLinkUrl3;

                sqlParams[24] = new SqlParameter("@OtherLinkUrl4", SqlDbType.VarChar, 100);
                sqlParams[24].Value = _ent.OtherLinkUrl4;

                sqlParams[25] = new SqlParameter("@OtherLinkUrl5", SqlDbType.VarChar, 100);
                sqlParams[25].Value = _ent.OtherLinkUrl5;

                sqlParams[26] = new SqlParameter("@OtherLinkUrl5", SqlDbType.VarChar, 100);
                sqlParams[26].Value = _ent.OtherLinkUrl5;

                sqlParams[27] = new SqlParameter("@IsSytem", SqlDbType.Int);
                sqlParams[27].Value = _ent.IsSystem;
                #endregion 

                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                if (_ent.IsEdit == true)
                {
                    sqlParams[28] = new SqlParameter("@ApprSchemeID", SqlDbType.BigInt);
                    sqlParams[28].Value = _ent.ApprovalShemeID;

                    SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spApprSchemeEdit", sqlParams);
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spApprSchemeAdd", sqlParams);
                }
                _trans.Commit();
            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.Approval.Extend",
                    ClassName = "ApprovalScheme",
                    FunctionName = "ApprSchemAddEdit",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalSchemeList",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Approval
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion 
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) { _conn.Close(); };
                _conn.Dispose();
            }
        }

        public virtual void ApprSchemeDelete(ApprovalEntities _ent)
        {

            SqlParameter[] sqlParams = new SqlParameter[0];
            SqlConnection _conn = new SqlConnection(Connectionstring);

            try
            {                
                sqlParams[0] = new SqlParameter("@AprvSchemeID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.ApprovalShemeID;

                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spApprSchemeDelete", sqlParams);

                _trans.Commit();
            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.Approval.Extend",
                    ClassName = "ApprovalScheme",
                    FunctionName = "ApprSchemeDelete",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalSchemeList",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Approval
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion 

            }
            finally
            {
                if (_conn.State == ConnectionState.Open) { _conn.Close(); };
                _conn.Dispose();
            }
        }

        public virtual ApprovalEntities ApprovalSchemeView (ApprovalEntities _ent)
        {
            SqlParameter[] sqlParams = new SqlParameter[0];
            try
            {
                sqlParams[0] = new SqlParameter("@AprvSchemeID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.ApprovalShemeID;
                _ent.ApprovalSchemeViewData.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spApprSchemeView", sqlParams));
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.Approval.Extend",
                    ClassName = "ApprovalScheme",
                    FunctionName = "ApprovalSchemeView",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalSchemeList",
                    ExceptionObject = _exp,
                    EventID = 80, // 80 Untuk Approval
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
                #endregion 
            }
            return _ent; 
        }
    }
}

//    Public Function GetApprovalType(ByVal strConnection As String) As DataTable
//        Try
//            Return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, SPGETAPPROVALTYPE).Tables(0)

//        Catch exp As Exception
//            WriteException("ApprovalScheme", "GetApprovalType", exp.Message )
//            Throw New Exception(exp.Message)
//        End Try
//    End Function
//#End Region
//#Region "Approval Tree"
//    Public Function ApprovalPathTree(ByVal customclass As Entities.ApprovalScheme) As DataTable
//        Dim params() As SqlParameter = New SqlParameter(0) {}
//        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
//        params(0).Value = customclass.ApprovalSchemeID
//        Try
//            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALPATHTREE, params).Tables(0)
//        Catch exp As Exception
//            WriteException("ApprovalScheme", "ApprovalPathTree", exp.Message )
//            Throw New Exception(exp.Message)
//        End Try
//    End Function
//#Region "Approval Member"
//    Public Sub ApprovalMemberDelete(ByVal Customclass As Entities.ApprovalScheme)
//        Dim objCon As New SqlConnection(Customclass.strConnection)
//        Dim objtrans As SqlTransaction
//        Dim params() As SqlParameter = New SqlParameter(2) {}

//        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
//        params(0).Value = Customclass.ApprovalSchemeID

//        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
//        params(1).Value = Customclass.ApprovalSeqNo

//        params(2) = New SqlParameter(PARAM_LOGINID, SqlDbType.VarChar, 12)
//        params(2).Value = Customclass.LoginId
//        Try
//            If objCon.State = ConnectionState.Closed Then objCon.Open()
//            objtrans = objCon.BeginTransaction
//            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALMEMBERDELETE, params)
//            objtrans.Commit()
//        Catch exp As Exception
//            objtrans.Rollback()
//            WriteException("ApprovalScheme", "ApprovalMemberDelete", exp.Message )
//            Throw New Exception(exp.Message)
//        Finally
//            If objCon.State = ConnectionState.Open Then objCon.Close()
//            objCon.Dispose()
//        End Try
//    End Sub
//    Public Function ApprovalMemberLoginList(ByVal customclass As Entities.ApprovalScheme) As DataTable
//        Dim params() As SqlParameter = New SqlParameter(1) {}
//        params(0) = New SqlParameter(PARAM_LOGINID, SqlDbType.VarChar, 8000)
//        params(0).Value = customclass.LoginId
//        params(1) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
//        params(1).Value = customclass.ApprovalSchemeID
//        Try
//            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALMEMBERLOGINLIST, params).Tables(0)
//        Catch exp As Exception
//            WriteException("ApprovalScheme", "ApprovalMemberLoginList", exp.Message )
//            Throw New Exception(exp.Message)
//        End Try
//    End Function
//    Public Function ApprovalMemberSetMember(ByVal customclass As Entities.ApprovalScheme) As DataTable
//        Dim params() As SqlParameter = New SqlParameter(0) {}
//        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
//        params(0).Value = customclass.ApprovalSchemeID
//        Try
//            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALMEMBERSETMEMBER, params).Tables(0)
//        Catch exp As Exception
//            WriteException("ApprovalScheme", "ApprovalMemberSetMember", exp.Message )
//            Throw New Exception(exp.Message)
//        End Try
//    End Function
//    Public Function ApprovalMemberView(ByVal customclass As Entities.ApprovalScheme) As DataTable
//        Dim params() As SqlParameter = New SqlParameter(1) {}
//        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
//        params(0).Value = customclass.ApprovalSchemeID

//        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
//        params(1).Value = customclass.ApprovalSeqNo
//        Try
//            Return SqlHelper.ExecuteDataset(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALMEMBERVIEW, params).Tables(0)
//        Catch exp As Exception
//            WriteException("ApprovalScheme", "ApprovalMemberView", exp.Message )
//            Throw New Exception(exp.Message)
//        End Try
//    End Function

//    Public Sub ApprovalMemberAdd(ByVal Customclass As Entities.ApprovalScheme)
//        Dim objCon As New SqlConnection(Customclass.strConnection)
//        Dim objtrans As SqlTransaction
//        Dim params() As SqlParameter = New SqlParameter(7) {}

//        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
//        params(0).Value = Customclass.ApprovalSchemeID

//        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
//        params(1).Value = Customclass.ApprovalSeqNo

//        params(2) = New SqlParameter(PARAM_LIMITVALUE, SqlDbType.Decimal)
//        params(2).Value = Customclass.Limit

//        params(3) = New SqlParameter(PARAM_DURATION, SqlDbType.Int)
//        params(3).Value = Customclass.Duration

//        params(4) = New SqlParameter(PARAM_AUTOUSER, SqlDbType.VarChar, 12)
//        params(4).Value = Customclass.AutoUser

//        params(5) = New SqlParameter(PARAM_LOGINID, SqlDbType.VarChar, 12)
//        params(5).Value = Customclass.LoginId

//        params(6) = New SqlParameter(PARAM_NEXTPERSON, SqlDbType.VarChar, 8000)
//        params(6).Value = Customclass.NextPerson

//        params(7) = New SqlParameter(PARAM_ADDITIONALEMAIL, SqlDbType.VarChar, 50)
//        params(7).Value = Customclass.AdditionalEmail
//        Try
//            If objCon.State = ConnectionState.Closed Then objCon.Open()
//            objtrans = objCon.BeginTransaction
//            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALMEMBERADD, params)
//            objtrans.Commit()
//        Catch exp As Exception
//            objtrans.Rollback()
//            WriteException("ApprovalScheme", "ApprovalMemberAdd", exp.Message )
//            Throw New Exception(exp.Message)
//        Finally
//            If objCon.State = ConnectionState.Open Then objCon.Close()
//            objCon.Dispose()
//        End Try
//    End Sub
//    Public Sub ApprovalMemberEdit(ByVal customclass As Entities.ApprovalScheme)
//        Dim objCon As New SqlConnection(customclass.strConnection)
//        Dim objtrans As SqlTransaction
//        Dim params() As SqlParameter = New SqlParameter(7) {}

//        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
//        params(0).Value = customclass.ApprovalSchemeID

//        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
//        params(1).Value = customclass.ApprovalSeqNo

//        params(2) = New SqlParameter(PARAM_LIMITVALUE, SqlDbType.Decimal)
//        params(2).Value = customclass.Limit

//        params(3) = New SqlParameter(PARAM_DURATION, SqlDbType.Int)
//        params(3).Value = customclass.Duration

//        params(4) = New SqlParameter(PARAM_AUTOUSER, SqlDbType.VarChar, 12)
//        params(4).Value = customclass.AutoUser

//        params(5) = New SqlParameter(PARAM_LOGINID, SqlDbType.VarChar, 12)
//        params(5).Value = customclass.LoginId

//        params(6) = New SqlParameter(PARAM_NEXTPERSON, SqlDbType.VarChar, 8000)
//        params(6).Value = customclass.NextPerson

//        params(7) = New SqlParameter(PARAM_ADDITIONALEMAIL, SqlDbType.VarChar, 50)
//        params(7).Value = customclass.AdditionalEmail
//        Try
//            If objCon.State = ConnectionState.Closed Then objCon.Open()
//            objtrans = objCon.BeginTransaction
//            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, SPAPPROVALMEMBEREDIT, params)
//            objtrans.Commit()
//        Catch exp As Exception
//            objtrans.Rollback()
//            WriteException("ApprovalScheme", "ApprovalMemberEdit", exp.Message )
//            Throw New Exception(exp.Message)
//        Finally
//            If objCon.State = ConnectionState.Open Then objCon.Close()
//            objCon.Dispose()
//        End Try
//    End Sub
//#End Region

//#End Region
//#Region "Approval Communication Linked"
//    Public Sub AprCommLinkedProcess(ByVal Customclass As Entities.ApprovalScheme)
//        Dim objCon As New SqlConnection(Customclass.strConnection)
//        Dim objtrans As SqlTransaction
//        Dim params() As SqlParameter = New SqlParameter(9) {}

//        params(0) = New SqlParameter(PARAM_BRANCHID, SqlDbType.VarChar, 3)
//        params(0).Value = Customclass.BranchId

//        params(1) = New SqlParameter("@ApplicationID", SqlDbType.VarChar, 20)
//        params(1).Value = Customclass.ApplicationID

//        params(2) = New SqlParameter("@SenderName", SqlDbType.VarChar, 50)
//        params(2).Value = Customclass.LoginId

//        params(3) = New SqlParameter("@RecipientName", SqlDbType.VarChar, 1000)
//        params(3).Value = Customclass.Recipient

//        params(4) = New SqlParameter("@Subject", SqlDbType.Text)
//        params(4).Value = Customclass.subjectMessage

//        params(5) = New SqlParameter("@Message", SqlDbType.Text)
//        params(5).Value = Customclass.mailmessage

//        params(6) = New SqlParameter("@FlagMessage", SqlDbType.Char, 1)
//        params(6).Value = Customclass.FlagMessage

//        params(7) = New SqlParameter("@StrEmployeeIDRecepient", SqlDbType.VarChar, 8000)
//        params(7).Value = Customclass.StrEmployeeIDRecipient

//        params(8) = New SqlParameter("@SeqNo", SqlDbType.Int)
//        params(8).Value = Customclass.SeqNo

//        params(9) = New SqlParameter("@BusinessDate", SqlDbType.DateTime)
//        params(9).Value = Customclass.BusinessDate
        
//        Try
//            If objCon.State = ConnectionState.Closed Then objCon.Open()
//            objtrans = objCon.BeginTransaction
//            SqlHelper.ExecuteNonQuery(objtrans, CommandType.StoredProcedure, spApprovalCommunicationLinkedProcess, params)
//            objtrans.Commit()
//        Catch exp As Exception
//            objtrans.Rollback()
//            WriteException("ApprovalScheme", "AprCommLinkedProcess", exp.Message)
//            Throw New Exception(exp.Message)
//        Finally
//            If objCon.State = ConnectionState.Open Then objCon.Close()
//            objCon.Dispose()
//        End Try
//    End Sub
