CREATE PROCEDURE [dbo].[spDocTransBinaryInsert]

 
	@DocTransID BigInt,
	@FileName varchar(8000),
	@DateCreated datetime,
	@SizeFileBytes numeric(20,0),
	@Pixel varchar(100) = 0,
	@ComputerName varchar(100) = 0,
	@DPI varchar(100) = 0,
	@FileBinary varbinary(max),
	@UsrCrt varchar(50)


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
		@UsrCrt,
		GETDATE(),
		@UsrCrt,
		GETDATE()
		)

	--select @file +'asdasd'
    -- Insert statements for procedure here
	--update PATH set FILE_NAME = @file+@ext, BINARY = @bin where PATH_ID = cast(@file as int)
END

