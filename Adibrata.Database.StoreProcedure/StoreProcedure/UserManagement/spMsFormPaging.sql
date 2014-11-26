CREATE PROCEDURE [dbo].[spMsFormPaging]
	@StartRecord int, 
	@EndRecord int, 
	@WhereCond varchar(8000), 
	@SortBy Varchar(8000)
AS
Set NoCount On 
	SELECT @param1, @param2
RETURN 0

