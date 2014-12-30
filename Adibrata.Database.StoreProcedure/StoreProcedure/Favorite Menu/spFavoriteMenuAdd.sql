CREATE PROCEDURE [dbo].[spFavoriteMenuAdd]
	@FormURL varchar(255), 
	@UserLogin varchar(50)
AS
Set Nocount On 
Declare @FormID bigint, @UserID Bigint

Select @FormID = id from ms_form with (nolock) where FormUrl = @FormUrl
Select @UserId = id from MS_User with (nolock) where UserName = @UserLogin

Insert Into FavoriteMenu (UserId, FormID, UsrCrt, DtmCrt)
values (@UserID, @FormID, @UserID, getdate())
RETURN 0
