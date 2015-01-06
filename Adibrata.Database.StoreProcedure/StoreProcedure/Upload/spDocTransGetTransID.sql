
CREATE PROCEDURE [dbo].[spDocTransGetTransID]
	@DocTransCode varchar(50),
	@TransID BigInt OutPut
AS
Set NoCount On

	Select @TransID = ID From DocTrans with (nolock) where DocTransCode = @DocTransCode
