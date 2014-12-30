ALTER  PROCEDURE [dbo].[spMsUserMenuGetByUserId]
    -- Add the parameters for the stored procedure here
    @UserId bigint
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    select MS_UserMenu.FormID,MS_Form.FormName from MS_UserMenu with (nolock) join MS_Form with (nolock) on MS_UserMenu.FormID = MS_Form.ID where MS_UserMenu.UserID = @UserId
END
