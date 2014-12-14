CREATE PROCEDURE [dbo].[spMsUserPaging]
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
	Set @SortBy = ' FullName Asc '

	Set @SqlStatement = 'Select * from 
		(Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, Ms_User.ID, UserName, FullName, 
		(Case When Active = 1 then ''Y'' else ''N'' end) Active,
		(Case When IsConnect = 1 then ''Y'' else ''N'' end) IsConnet
		
		From Ms_User with (nolock) ' + @WhereCond  + ') Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  
		exec (@SqlStatement)
--Set @TotalRecord =  @@ROWCOUNT
RETURN 0

