-- =============================================
-- Author:		Nurul Fredyani Tanaya
-- Create date: 20141218
-- Description:	Stored Procedure untuk get Doc Trans Content by doc transs id
-- =============================================
CREATE PROCEDURE [dbo].[spDocTransContentView]
	-- Add the parameters for the stored procedure here
	@DocTransId BigInt,
	@UserName varchar(50)
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	exec spDocTransActivityInsert @username = @UserName, @DocTransId = @DocTransId,@description = 'Content View'
    -- Insert statements for procedure here
	select DocTransContent.ContentName,
		CAST(
			CASE 
				WHEN ContentValue <> '' 
					Then ContentValue 
			ELSE 
				CASE 
					WHEN ContentValueNumeric <>0 
						THEN CAST(ContentValueNumeric as varchar)
					ELSE 
						CAST(ContenValueDate as varchar)
					END 
			END
		AS varchar(100)) 
		as value
		from DocTransContent with (nolock) 
		WHERE 
		DocTransID = @DocTransId

END
GO
