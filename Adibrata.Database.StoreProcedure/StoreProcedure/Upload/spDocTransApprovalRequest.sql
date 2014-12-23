CREATE PROCEDURE [dbo].[spDocTransApprovalRequest]
@DocTypeCode varchar(50), 
@DocTransID BigInt, 
@NextLevelAppr varchar(1),
@DocTransReqDate DateTime, 
@UserDocTransReq varchar(50)

AS
Set NoCount On

RETURN 0
