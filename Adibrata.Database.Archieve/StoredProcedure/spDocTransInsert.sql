CREATE PROCEDURE [dbo].[spDocTransInsert]
	-- Add the parameters for the stored procedure here
	@TransId Varchar(50),
	@docType varchar(50),
	@UsrCrt varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	INSERT INTO DocTrans
	(
		TransID,
		DocTypeCode,
		UsrCrt,
		DtmCrt,
		UsrUpd,
		DtmUpd

	) 
	OUTPUT inserted.Id 
	VALUES
	(
		@TransId,
		@docType,
		@UsrCrt,
		GETDATE(),
		@UsrCrt,
		GETDATE()
	)

END

