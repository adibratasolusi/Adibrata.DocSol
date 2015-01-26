
CREATE PROCEDURE spDocTransBinaryNoteView
	-- Add the parameters for the stored procedure here
	@DocTransBinaryId Bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select ISNULL(Note,'-') from DocTransBinary with (nolock) where id = @DocTransBinaryId
END
GO