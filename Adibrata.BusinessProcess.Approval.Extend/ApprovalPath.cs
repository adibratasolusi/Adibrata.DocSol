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
    public class ApprovalPath : Adibrata.BusinessProcess.Approval.Core.ApprovalPath
    {
        static string Connectionstring = AppConfig.Config("ConnectionString");
        SqlTransaction _trans;

        public virtual void ApprPathAddEdit(ApprovalEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(Connectionstring);
            SqlParameter[] sqlParams;

            if (_ent.IsEdit == true) { sqlParams = new SqlParameter[9]; } else { sqlParams = new SqlParameter[8]; }
            try
            {
                #region "List Parameter SQL"
                sqlParams[0] = new SqlParameter("@AprvSchemeId", SqlDbType.Int);
                sqlParams[0].Value = _ent.ApprovalShemeID;
                
                sqlParams[1] = new SqlParameter("@AprvSeqNo", SqlDbType.Int);
                sqlParams[1].Value = _ent.ApprovalSeqNo;

                sqlParams[2] = new SqlParameter("@AprvPathDesc", SqlDbType.Int);
                sqlParams[2].Value = _ent.ApprovalPathDescription;

                sqlParams[3] = new SqlParameter("@MaxLimit", SqlDbType.Float);
                sqlParams[3].Value = _ent.MaximumLimit;

                sqlParams[4] = new SqlParameter("@Level", SqlDbType.Int);
                sqlParams[4].Value = _ent.ApprovalPathLevel;

                sqlParams[5] = new SqlParameter("@CanFinalReject", SqlDbType.Int);
                sqlParams[5].Value = _ent.CanFinalReject;

                sqlParams[6] = new SqlParameter("@CanFinalApprove", SqlDbType.Int);
                sqlParams[6].Value = _ent.CanFinalApprove;

                sqlParams[7] = new SqlParameter("@CanEscalation", SqlDbType.Int);
                sqlParams[7].Value = _ent.CanEscalation;

                sqlParams[8] = new SqlParameter("@CanChangeFinalLevel", SqlDbType.Int);
                sqlParams[8].Value = _ent.CanChangeFinalLevel;

                #endregion

                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                if (_ent.IsEdit == true)
                {
                    sqlParams[9] = new SqlParameter("@AprvPathID", SqlDbType.BigInt);
                    sqlParams[9].Value = _ent.ApprovalPathID;

                    SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spApprPathEdit", sqlParams);
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spApprPathAdd", sqlParams);
                }
                _trans.Commit();
            }
            catch(Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.Approval.Extend",
                    ClassName = "ApprovalPath",
                    FunctionName = "ApprPathAddEdit",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalPath",
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

        public virtual void ApprPathDelete(ApprovalEntities _ent)
        {
            SqlConnection _conn = new SqlConnection(Connectionstring);
            SqlParameter[] sqlParams = new SqlParameter[0];

            sqlParams[0] = new SqlParameter("@AprvPathID", SqlDbType.Int);
            sqlParams[0].Value = _ent.ApprovalPathID;
            try
            {
                if (_conn.State == ConnectionState.Closed) { _conn.Open(); };
                _trans = _conn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, "spApprPathDelete", sqlParams);
            }
            catch (Exception _exp)
            {
                _trans.Rollback();
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = _ent.UserLogin,
                    NameSpace = "Adibrata.BusinessProcess.Approval.Extend",
                    ClassName = "ApprovalPath",
                    FunctionName = "ApprPathDelete",
                    ExceptionNumber = 1,
                    EventSource = "ApprovalPath",
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


        public virtual ApprovalEntities ApprPathView(ApprovalEntities _ent)
        {
            SqlParameter[] sqlParams = new SqlParameter[0];
            try
            {
                sqlParams[0] = new SqlParameter("@AprvPathID", SqlDbType.BigInt);
                sqlParams[0].Value = _ent.ApprovalPathID;
                _ent.ApprovalPathViewData.Load(SqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, "spApprPathView", sqlParams));
            }
            catch (Exception _exp)
            {
                #region "Write to Event Viewer"
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = _ent.UserLogin,
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

//    Public Function ApprovalPathGetLimit(ByVal customclass As Entities.ApprovalScheme) As Double

//        Dim params() As SqlParameter = New SqlParameter(2) {}

//        params(0) = New SqlParameter(PARAM_TYPELIMIT, SqlDbType.VarChar, 3)
//        params(0).Value = customclass.TypeLimit

//        params(1) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 10)
//        params(1).Value = customclass.ApprovalSchemeID

//        params(2) = New SqlParameter(PARAM_LEVEL, SqlDbType.Int)
//        params(2).Value = customclass.Level

//        Try
//            Return CDbl(SqlHelper.ExecuteScalar(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALTREEGETLIMIT, params))
//        Catch exp As Exception
//            WriteException("ApprovalScheme", "ApprovalPathGetLimit", exp.Message )
//            Throw New Exception(exp.Message)
//        End Try
//    End Function

//    Public Function ApprovalPathView(ByVal customclass As Entities.ApprovalScheme) As Entities.ApprovalScheme
//        Dim objread As SqlDataReader
//        Dim params() As SqlParameter = New SqlParameter(2) {}

//        params(0) = New SqlParameter(PARAM_APPROVALSCHEMEID, SqlDbType.VarChar, 12)
//        params(0).Value = customclass.ApprovalSchemeID

//        params(1) = New SqlParameter(PARAM_APPROVALSEQNO, SqlDbType.Int)
//        params(1).Value = customclass.ApprovalSeqNo

//        params(2) = New SqlParameter(PARAM_APPROVALPARENTSEQNUM, SqlDbType.Int)
//        params(2).Value = customclass.ApprovalParentSeqNum

//        Try
//            objread = SqlHelper.ExecuteReader(customclass.strConnection, CommandType.StoredProcedure, SPAPPROVALTREEPATHVIEW, params)
//            If objread.Read Then
//                With customclass
//                    .Limit = CDbl(objread("Limit"))
//                    .MaxLimit = CDbl(objread("MaxLimit"))
//                    .MinLimit = CDbl(objread("MinLimit"))
//                    .CanFinalReject = CBool(objread("CanFinalReject"))
//                    .CanFinalApprove = CBool(objread("CanFinalApprove"))
//                    .CanEscalation = CBool(objread("CanEscalation"))
//                    .RejectAction = CStr(objread("RejectAction"))
//                    .ApprovalDuration = CInt(objread("ApprovalDuration"))
//                    .CanChangeFinalLevel = CBool(objread("CanChangeFinalLevel"))
//                End With
//            End If
//            objread.Close()
//            Return customclass
//        Catch exp As Exception
//            WriteException("ApprovalScheme", "ApprovalPathView", exp.Message )
//            Throw New Exception(exp.Message)
//        End Try
//    End Function
//#End Region