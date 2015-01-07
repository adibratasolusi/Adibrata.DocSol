CREATE PROCEDURE [dbo].[spCustPagingTotRec]
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
		(Select Count(1) as TotalRecord
		From Cust with (nolock)
		left Join CustCoy with (nolock) on Cust.ID = CustCoy.CustID ' + @WhereCond  + ') Qry '
		
		exec (@SqlStatement)
Set @TotalRecord =  @@ROWCOUNT
		 