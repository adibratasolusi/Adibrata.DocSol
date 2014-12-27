CREATE PROCEDURE [dbo].[spDocTransBinaryView] 
	-- Add the parameters for the stored procedure here
	@DocTransID bigint,
	@UserName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	exec spDocTransActivityInsert @username = @UserName, @DocTransId = @DocTransId,@description = 'Binary View'
	select 
	FileName,DateCreated,SizeFileBytes,Pixel,ComputerName,DPI,
	CAST(CASE WHEN DPI <> '-' THEN FileBinary ELSE null END as varbinary(MAX)) AS FileBin
	 from DocTransBinary where DocTransID = @DocTransID

	 

    -- Insert statements for procedure here
END

