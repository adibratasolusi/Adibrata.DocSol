Create PROCEDURE [dbo].[spDeleteDocumentStatus]
	@ID Bigint
AS
Set NoCount On 

    UPDATE DocTrans set DocTransStatus ='CANCEL' where Id=@ID

