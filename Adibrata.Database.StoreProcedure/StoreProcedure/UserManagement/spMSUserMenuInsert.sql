create PROCEDURE [dbo].[spMSUserMenuInsert]
    -- Add the parameters for the stored procedure here
    @UserId bigint,
    @FormId bigint,
    @UsrCrt varchar(20)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    
    -- Insert statements for procedure here
    INSERT INTO 
    MS_UserMenu
    (
        UserID,
        FormID,
        UsrCrt,
        DtmCrt,
        UsrUpd,
        DtmUpd
    )
    VALUES
    (
        @UserId,
        @FormId,
        @UsrCrt,
        GETDATE(),
        @UsrCrt,
        GETDATE()
    )
END
