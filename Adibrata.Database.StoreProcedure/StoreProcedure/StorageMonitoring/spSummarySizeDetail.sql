CREATE PROCEDURE [dbo].[spSummarySizeDetail]
    -- Add the parameters for the stored procedure here
    @Ext varchar(8000)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    select dt.T