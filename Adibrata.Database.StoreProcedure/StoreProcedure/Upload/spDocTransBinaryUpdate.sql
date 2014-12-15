CREATE PROCEDURE [dbo].[spDocTransBinaryUpdate]
	-- Add the parameters for the stored procedure here
	@DocTransBinaryID bigint,
	@FileName varchar(100),
	@FileBinary varbinary(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE DocTransBinary set FileBinary = @FileBinary, FileName = @FileName where Id = @DocTransBinaryID
END

