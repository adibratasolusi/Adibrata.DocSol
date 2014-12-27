create PROCEDURE [dbo].[spMsUserMenuDeleteBeforeInsert] 
    -- Add the parameters for the stored procedure here
    @UserId bigint
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    delete from MS_UserMenu where UserID = @UserId

END
