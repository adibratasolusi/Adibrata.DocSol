ALTER PROCEDURE spMsFormPaging
	@StartRecord varchar(7), 
	@EndRecord varchar(7), 
	@WhereCond varchar(8000), 
	@SortBy Varchar(8000)
AS
Set NoCount On 
	
Declare 
		@SqlStatement Varchar(max)
Set NoCount On 

If @SortBy = '' 
	Set @SortBy = ' FormName Asc '

	Set @SqlStatement = 'Select * from 
		(Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, FormCode, FormName, FormUrl, 
		(Case When IsActive = 1 then ''Y'' else ''N'' end) Active
		From Ms_Form with (nolock) ' + @WhereCond  + ') Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  
		exec (@SqlStatement)
--Set @TotalRecord =  @@ROWCOUNT
RETURN 0


