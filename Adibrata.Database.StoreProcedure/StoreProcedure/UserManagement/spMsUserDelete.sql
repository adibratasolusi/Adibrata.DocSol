CREATE PROCEDURE [dbo].[spMsUserDelete]
	@UserID BigInt
AS
Set NoCount On 
		Delete Ms_User where ID = @UserID
RETURN 0
