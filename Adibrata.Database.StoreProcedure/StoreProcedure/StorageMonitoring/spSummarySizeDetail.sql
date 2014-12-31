CREATE PROCEDURE [dbo].[spSummarySizeDetail]
    -- Add the parameters for the stored procedure here
    @Ext varchar
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    select dt.TransID,dt.DocTypeCode,dtb.FileName,dtb.SizeFileBytes,dtb.Pixel, dtb.FileBinary from DocTransBinary dtb with (nolock) join DocTrans dt with (nolock) on dtb.DocTransID =dt.Id where dbo.GetColumnValue(dtb.FileName,'.',2) = 'jpg'
END