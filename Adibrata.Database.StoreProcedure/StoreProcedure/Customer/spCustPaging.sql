ALTER PROCEDURE [dbo].[spCustPaging]
	@StartRecord varchar(10), 
	@EndRecord varchar(10), 
	@WhereCond varchar(8000), 
	@SortBy Varchar(8000)
AS
Set NoCount On 
Declare @RecordNumber Numeric, 
		@SqlStatement Varchar(max)
Set NoCount On 
Declare @TotalRecord int
If @SortBy = '' 
	Set @SortBy = ' CustName Asc '

	Set @SqlStatement = 'Select * from 
		(Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, Cust.ID as CustID, CustCode, CustName, CustType
		,(Case When cust.CustType= ''C'' then isnull(CustCoy.FullAddress,'''') End) as Address
		From Cust with (nolock)
		left Join CustCoy with (nolock) on Cust.ID = CustCoy.CustID ' + @WhereCond  + ') Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  
		exec (@SqlStatement)
Set @TotalRecord =  @@ROWCOUNT
		 

RETURN 0
