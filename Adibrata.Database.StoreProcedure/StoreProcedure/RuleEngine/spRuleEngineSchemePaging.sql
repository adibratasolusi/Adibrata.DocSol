ALTER PROCEDURE [dbo].[spRuleEngineSchemePaging]
	@StartRecord varchar(10), 
	@EndRecord varchar(10), 
	@WhereCond varchar(8000), 
	@SortBy Varchar(8000)
AS
Set NoCount On 
Declare @SqlStatement Varchar(max)
If @SortBy = '' 
	Set @SortBy = ' RuleSchmDesc Asc '

	Set @SqlStatement = 'Select * from 
		(Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, ID as ID, RuleSchmCode, RuleSchmDesc, RuleFile, RuleFileName
		From RuleScheme with (nolock)
		' + @WhereCond  + ') Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  
		exec (@SqlStatement)
RETURN 0
