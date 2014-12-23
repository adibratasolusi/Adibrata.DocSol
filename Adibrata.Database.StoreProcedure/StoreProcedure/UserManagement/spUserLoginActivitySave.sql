CREATE PROCEDURE [dbo].[spUserLoginActivitySave]
	@FormUrl Varchar(50), 
	@UserLogin varchar(20), 
	@DateTimeAccess Datetime

AS
Set NoCount On 

Insert Into UserActivity (UserLogin, FormUrl, DateTimeAccess, UsrCrt, DtmCrt)
Values (@UserLogin, @FormUrl, @DateTimeAccess, @UserLogin, getdate())

RETURN 0
