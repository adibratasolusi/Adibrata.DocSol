
CREATE PROCEDURE [dbo].[spImageUnlockedStatus]
	

	@ID Bigint
AS
Set NoCount On 

    UPDATE DocTrans set DocTransStatus ='ACTIVE' where Id=@ID

