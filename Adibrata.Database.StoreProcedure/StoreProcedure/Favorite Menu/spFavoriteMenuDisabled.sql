CREATE PROCEDURE [dbo].[spFavoriteMenuDisabled]
	@FormURL varchar(255), 
	@UserLogin Varchar(50)

AS
set nocount on 
Declare @FormID bigint, @UserID Bigint
Select @FormID = id from ms_form with (nolock) where FormUrl = @FormUrl
Select @UserId = id from MS_User with (nolock) where UserName = @UserLogin

If exists (select 1 from FavoriteMenu with (nolock) where FormID = @FormID and UserID = @UserID)
	Select 1
Else
	Select 0


RETURN 0
