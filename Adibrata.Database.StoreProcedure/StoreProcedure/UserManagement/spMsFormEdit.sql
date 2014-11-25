CREATE PROCEDURE [dbo].[spMsFormEdit]
	@FormID bigint, 
	@FormName Varchar(50), 
	@FormURL Varchar(255), 
	@ISActive int, 
	@UsrUpd varchar(50)
AS
set Nocount On 
	Update MS_Form set FormName = @FormName, FormUrl = @FormURL, ISActive = @IsActive, UsrUpd = @UsrUpd
	where ID = @FormID
RETURN 0
