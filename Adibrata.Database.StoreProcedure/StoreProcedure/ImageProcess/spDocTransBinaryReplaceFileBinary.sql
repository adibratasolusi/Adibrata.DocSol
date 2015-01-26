CREATE PROCEDURE spDocTransBinaryReplaceFileBinary
	-- Add the parameters for the stored procedure here
	@DocTransBinaryId bigint,
	@FileBinary varbinary(max),
	@UsrUpd varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update DocTransBinary set FileBinary = @FileBinary,UsrUpd = @UsrUpd,DtmUpd = GETDATE() where Id = @DocTransBinaryId
END
GO

