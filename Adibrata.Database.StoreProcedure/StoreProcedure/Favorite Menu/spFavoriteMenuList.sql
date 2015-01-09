CREATE PROCEDURE [dbo].[spFavoriteMenuList]
@UserLogin varchar(50)
AS
Set NoCount On 
Select B.FormName, B.FormURL From FavoriteMenu A with (nolock)
inner Join Ms_Form B on A.FormID = B.ID
Inner Join Ms_User C on A.UserID = C.ID
where C.UserName = @UserLogin
