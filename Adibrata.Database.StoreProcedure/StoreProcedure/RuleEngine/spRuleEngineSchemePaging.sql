CREATE PROCEDURE [dbo].[spRuleEngineSchemePaging]
	@StartRecord varchar(10), 
	@EndRecord varchar(10), 
	@WhereCond varchar(8000), 
	@SortBy Varchar(8000)
AS
Set NoCount On 
Declare @SqlStatement Varchar(max)
If @SortBy = '' 
	Set @SortBy = ' RuleSchmName Asc '

	Set @SqlStatement = 'Select * from 
		(Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, ID as ID, RuleSchmCode, RuleSchmName, RuleFile, RuleFileName
		From RuleList with (nolock)
		' + @WhereCond  + ') Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  
		exec (@SqlStatement)
RETURN 0
