
-- =============================================
-- Author:		Nurul Fredyani Tanaya
-- Create date: 20141110
-- Description:	insert ke tabel DocTrans
-- =============================================
CREATE PROCEDURE [dbo].[spDocTransInsert]
	-- Add the parameters for the stored procedure here
	@TransId bigint,
	@docType varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRAN A
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
		'sa',
		GETDATE(),
		'sa',
		GETDATE()
	)
		 IF (@@ERROR <> 0) BEGIN
        ROLLBACK TRAN A
        RETURN 1
    END

COMMIT TRAN A

RETURN 0

END
