ALTER PROCEDURE [dbo].[spMsFormDelete]
	@FormID bigint
AS
set NoCount On 
	Delete Ms_form where ID = @FormID
RETURN 0
