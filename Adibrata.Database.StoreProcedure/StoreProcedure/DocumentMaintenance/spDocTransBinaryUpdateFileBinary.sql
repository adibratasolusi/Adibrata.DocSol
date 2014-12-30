Alter PROCEDURE [dbo].[spDocTransBinaryUpdateFileBinary]
    -- Add the parameters for the stored procedure here
    @DocTransBinaryId bigint,
    @Usr varchar(50),
    @filebin varbinary(max)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    
    update DocTransBinary set FileBinary = @filebin,UsrUpd = @usr,DtmUpd = GETDATE() where Id = @DocTransBinaryId
END