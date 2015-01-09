Create  PROCEDURE [dbo].[spProjectPagingTotRec]
	@WhereCond varchar(8000), 
	@SortBy Varchar(8000)
AS
Set NoCount On 
Declare @SqlStatement varchar(8000)
		Set @SqlStatement = 'Select * from 
		(Select Count(1) as TotalRecord 
		From Proj With (nolock) Left Join 
		Cust with (nolock) on Proj.CustID = Cust.ID 
		left Join CustCoy with (nolock) on Cust.ID = CustCoy.CustID ' + @WhereCond  + ') Qry '
		
		exec (@SqlStatement)
RETURN 0
