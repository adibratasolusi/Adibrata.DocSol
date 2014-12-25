create PROCEDURE [dbo].[spImageLockedStatus]
	

	@ID Bigint
AS
Set NoCount On 

    UPDATE DocTrans set DocTransStatus ='LOCKED' where Id=@ID
