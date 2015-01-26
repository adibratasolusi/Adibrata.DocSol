CREATE PROCEDURE PROCEDURE spDocTransBinaryUpdateBinary
	-- Add the parameters for the stored procedure here
	@DocTransBinaryId bigint,
	@Note varchar(20),
	@OldBinary varbinary(max),
	@NewBinary varbinary(max),
	@UsrUpd varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update DocTransBinary set 
		Note = @Note, 
		FileBinary = @NewBinary, 
		OriginalBinary = @OldBinary,
		UsrUpd = @UsrUpd,
		DtmUpd = GETDATE() 
	where Id = @DocTransBinaryId
END
GO