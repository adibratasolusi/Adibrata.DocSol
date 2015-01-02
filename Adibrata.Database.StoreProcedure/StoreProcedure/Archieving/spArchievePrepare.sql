CREATE PROCEDURE [dbo].[spArchievePrepare] 
    -- Add the parameters for the stored procedure here
    @DocTransCode varchar(50)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    update DocTrans set ArchiveStatus = 'PREPARE' where DocTransCode = @DocTransCode
END