CREATE PROCEDURE [dbo].[spDocTransContentView]
	-- Add the parameters for the stored procedure here
	@DocTransId varchar(50),
	@UserName varchar(50)
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	exec spDocTransActivityInsert @username = @UserName, @DocTransId = @DocTransId,@description = 'Content View'


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
		from DocTransContent
		WHERE 
		DocTransID = @DocTransId

END

