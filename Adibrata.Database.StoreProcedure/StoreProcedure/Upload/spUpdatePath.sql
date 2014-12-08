
-- =============================================
-- Author:		Nurul Fredyani Tanaya
-- Create date: 29/10/2014
-- Description:	SP Untuk update binary di table path, input berupa gambar
-- =============================================
ALTER PROCEDURE [dbo].[spUpdatePath]
 
 
	@DocTransID BigInt,
	@FileName varchar(8000),
	@DateCreated datetime,
	@SizeFileBytes numeric(20,0),
	@Pixel varchar(100) = 0,
	@ComputerName varchar(100) = 0,
	@DPI varchar(100) = 0,
	@FileBinary varbinary(max)


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO DocTransBinary
		(
		DocTransID,
		FileName,
		DateCreated,
		SizeFileBytes,
		Pixel,
		ComputerName,
		DPI,
		FileBinary,
		UsrUpd,
		DtmUpd,
		UsrCrt,
		DtmCrt
		)
	VALUES
		(
		@DocTransID,
		@FileName,
		@DateCreated,
		@SizeFileBytes,
		@Pixel,
		@ComputerName,
		@DPI,
		@FileBinary,
		'sa',
		GETDATE(),
		'sa',
		GETDATE()
		)

	--select @file +'asdasd'
    -- Insert statements for procedure here
	--update PATH set FILE_NAME = @file+@ext, BINARY = @bin where PATH_ID = cast(@file as int)
END
