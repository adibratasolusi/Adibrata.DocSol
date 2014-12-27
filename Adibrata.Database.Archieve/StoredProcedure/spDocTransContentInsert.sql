
CREATE PROCEDURE [dbo].[spDocTransContentInsert] 
	-- Add the parameters for the stored procedure here
	@DocTypeCode varchar(50),
	@DocTransId bigint,
	@ContentName varchar(50) = '',
	@ContentValue varchar(8000) = '',
	@ContentValueDate datetime = GETDATE,
	@ContentValueNumeric numeric(17,2) = 0,
	@UsrCrt varchar(50),
	@ContentSearchTag varchar(8000) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into DocTransContent
	(
		DocTypeCode,
		DocTransID,
		ContentName,
		ContentValue,
		ContenValueDate,
		ContentValueNumeric,
		ContentSearchTag,
		UsrUpd,
		DtmUpd,
		UsrCrt,
		DtmCrt
	)
	values
	(
		@DocTypeCode,
		@DocTransId,
		@ContentName,
		@ContentValue,
		@ContentValueDate,
		@ContentValueNumeric,
		@ContentSearchTag,
		@UsrCrt,
		GETDATE(),
		@UsrCrt,
		GETDATE()
	)
END

