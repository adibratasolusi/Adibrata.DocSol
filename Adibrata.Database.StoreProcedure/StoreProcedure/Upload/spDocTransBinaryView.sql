-- =============================================
-- Author:		Nurul Fredyani Tanaya
-- Create date: 20141218
-- Description:	Stored Procedure untuk view doc trans binary by DocTransId
-- =============================================
CREATE PROCEDURE spDocTransBinaryView 
	-- Add the parameters for the stored procedure here
	@DocTransID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select 
	FileName,DateCreated,SizeFileBytes,Pixel,ComputerName,DPI,
	CAST(CASE WHEN DPI <> '-' THEN FileBinary ELSE null END as varbinary(MAX)) AS FileBin
	 from DocTransBinary where DocTransID = @DocTransID

END
GO

