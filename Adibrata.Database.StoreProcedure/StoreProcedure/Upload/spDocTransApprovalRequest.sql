ALTER PROCEDURE [dbo].[spDocTransApprovalRequest]
	@DocTypeCode varchar(50), 
	@DocTransID BigInt, 
	@DocTransReqDate DateTime, 
	@DocTransReqUser varchar(50),
	@UsrCrt Varchar(50),
	@DtmCrt smalldatetime
AS
Set NoCount On

Declare @DocTransApprCode Varchar(50)
Exec spMasterSequence 1, 'APR', @DtmCrt, @DocTransApprCode Output

Insert Into DocTransAppr (DocTransID, DocTypeCode, DocTransReqUser, DocTransReqDate, DocTransApprCode, NextlevelAppr, CurrentLevelAppr, UsrCrt, DtmCrt)
Values
(@DocTransID, @DocTypeCode, @DocTransReqUser, @DocTransReqDate, @DocTransApprCode, '1', '0', @UsrCrt, Getdate())

Update DocTrans set DocTransStatus = 'NEW' where id = @DocTransID
