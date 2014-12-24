CREATE PROCEDURE spDocTransActivityInsert
	-- Add the parameters for the stored procedure here
	@username varchar(50),
	@DocTransId Bigint,
	@Description varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO DocTransActivity
		(
			UserName,
			DocTransId,
			Description,
			Dtm
		)
	VALUES
		(
			@username,
			@DocTransId,
			@Description,
			GETDATE()
		)
END
GO
