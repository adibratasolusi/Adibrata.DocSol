CREATE PROCEDURE [dbo].[spDocTransApprSave]
	@DocTransApprID BigInt, 
	@LastUserAppr varchar(50), 
	@NextLevelAppr varchar(2), 
	@CurrentLevelAppr varchar(2), 
	@DocApprStat varchar(10), 
	@RequestTo varchar(50), 
	@ApprovalNotes Varchar(8000),
	@UsrUpd varchar(50)
AS
set NoCount On 
Update DocTransAppr set RequestTo = @RequestTo, ApprovalNotes = @ApprovalNotes,
				NextLevelAppr = @NextLevelAppr, CurrentLevelAppr= @CurrentLevelAppr, 
				LastUserAppr = @LastUserAppr, DocApprStat = @DocApprStat, DtmLastApproval = GETDATE(), 
				UsrUpd = @UsrUpd, DtmUpd = GetDate()
				where ID = @DocTransApprID

Declare @DocTransID Bigint, @DocTransReqDate smalldatetime, @DocTransReqUser varchar(50), @NextLevelUser varchar(50), @ApprTimeSecond int
Select @DocTransID = DocTransID, @DocTypeCode = DocTypeCode, @DocTransReqDate = DocTransReqDate, @DocTransReqUser = DocTransReqUser, 
@NextLevelUser = NextLevelAppr
from DocTransAppr	with (nolock) where ID = @DocTransApprID

Insert Into DocTransApprHist (DocTransApprID, DocTransID, DocTypeCode, DocTransReqDate, DocTransReqUser, CurrentLevelAppr, NextLevelAppr, DocApprStat, LastUserAppr, 
NextLeveluser, RequestTo, ApprovalNotes, ApprTimeSecond, UsrCrt, DtmCrt)
Values
(@DocTransApprID, @DocTransID, @DocTypeCode, @DocTransReqDate, @DocTransReqUser, @CurrentLevelAppr, @NextLevelAppr, @DocApprStat, @LastUserAppr,
@NextLevelUser, @RequestTo, @ApprovalNotes, @ApprTimeSecond, @UsrUpd, GetDate())
Select * From DocTransApprHist
RETURN 0
