CREATE PROCEDURE [dbo].[spMsUserSave]
		@UserName varchar(50), 
		@FullName Varchar(50), 
		@Password varchar(500),
		@IsActive int, 
		@IsConnect Int, 
		@UsrCrt varchar(50), 
		@DtmCrt SmallDateTime, 
		@ID bigint= null, 
		@IsEdit bit=null, 
		@UsrUpd varchar(50) = null,
		@DtmUpd SmallDateTime = null 
AS
Set NOCount On 
If @IsEdit = 0
Begin
		Insert Into Ms_User
				(UserName, FullName, Password, Active, IsConnect, DtmCrt, UsrCrt)
		Values 
			(@UserName, @FullName, @Password, @IsActive, @IsConnect, @DtmCrt, @UsrCrt)
End
Else
Begin
		Update Ms_User set userName = @UserName, FullName = @FullName, Password = @Password, Active= @IsActive, isConnect= @IsConnect, UsrUpd = @UsrUpd, DtmUpd = @DtmUpd
		where ID = @ID
END
RETURN 0
