ALTER PROCEDURE [dbo].[spDocTransApprovalRequest]
	@DocTypeCode varchar(50), 
	@DocTransID BigInt, 
	@DocTransReqDate DateTime, 
	@DocTransReqUser varchar(50),
	@RequestTo Varchar(50), 
	@ApprovalNotes Varchar(8000),
	@UsrCrt Varchar(50),
	@DtmCrt smalldatetime
AS
Set NoCount On

Declare @DocTransApprCode Varchar(50)
Exec spMasterSequence 1, 'APR', @DtmCrt, @DocTransApprCode Output

Insert Into DocTransAppr (DocTransApprCode, DocTransID, DocTypeCode, DocTransReqUser, DocTransReqDate, 
NextlevelAppr, CurrentLevelAppr, DocApprStat, 
RequestTo, ApprovalNotes, DtmLastApproval, UsrCrt, DtmCrt)
Values
(@DocTransApprCode, @DocTransID, @DocTypeCode, @DocTransReqUser, @DocTransReqDate,  1, 0, 'NEW', @RequestTo, @ApprovalNotes, 
GetDate(),
@UsrCrt, Getdate())

Update DocTrans set DocTransStatus = 'NEW' where id = @DocTransID
Declare @DocTransApprID BigInt
Select @DocTransApprID = ID from DocTransAppr with (nolock) where DocTransApprCode = @DocTransApprCode

Insert Into DocTransApprHist (DocTransApprID, DocTransID, DocTypeCode, DocTransReqUser, DocTransReqDate, 
NextlevelAppr, CurrentLevelAppr, DocApprStat, 
RequestTo, ApprovalNotes, UsrCrt, DtmCrt)
Values
(@DocTransApprID, @DocTransID, @DocTypeCode, @DocTransReqUser, @DocTransReqDate, 1, 0, 'NEW',
 @RequestTo, @ApprovalNotes, @UsrCrt, Getdate())

--Select * from DocContentApprHist