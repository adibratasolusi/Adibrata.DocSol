
-- Create by Henry, 25 Nov 2014

CREATE PROCEDURE [dbo].[spMsUserView]
	@UserID bigint
AS
set NoCount On 
	SELECT UserName, FullName, Active, IsConnect, Password, SecurityQuestion, SecurityAnswer, ExpiredDate, SeqWrongPwd From MS_User with (nolock) where ID = @UserID
RETURN 0
