CREATE PROCEDURE [dbo].[spUserLoginActivitySave]
	@FormUrl Varchar(50), 
	@UserLogin varchar(20), 
	@DateTimeAccess Datetime

AS
Set NoCount On 
Declare @FormName Varchar(50)
Select @FormName = FormName from MS_Form with (nolock) where FormURL = @FormUrl

Insert Into UserActivity (UserLogin, FormUrl, DateTimeAccess, UsrCrt, DtmCrt)
Values (@UserLogin, @FormUrl, @DateTimeAccess, @UserLogin, getdate())

RETURN 0
