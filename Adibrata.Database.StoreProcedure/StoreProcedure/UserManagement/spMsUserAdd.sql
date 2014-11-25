CREATE PROCEDURE [dbo].[spMsUserAdd]
		@UserName varchar(50), 
		@FullName Varchar(50), 
		@Password varchar(500),
		@IsActive int, 
		@IsConnect Int, 
		@UsrCrt varchar(50), 
		@DtmCrt Datetime
AS
Set NOCount On 
Insert Into Ms_User
		(UserName, FullName, Password, Active, IsConnect, DtmCrt, UsrCrt)
Values 
	(@UserName, @FullName, @Password, @IsActive, @IsConnect, @DtmCrt, @UsrCrt)

RETURN 0
