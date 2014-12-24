Create  PROCEDURE [dbo].[spDocTransApprPaging]
	@StartRecord varchar(10), 
	@EndRecord varchar(10), 
	@WhereCond varchar(8000), 
	@SortBy Varchar(8000)
AS
Set NoCount On 

Declare @SqlStatement Varchar(8000)
If @SortBy = '' 
	Set @SortBy = ' DocTransReqDate Asc '

	Set @SqlStatement = 'Select * from 
		(Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, A.DocTransApprCode, B.DocTransCode, C.ProjName, C.ProjType, D.CustName
		from DocTransAppr A WITH (NOLOCK)
				Inner Join DocTrans B WITH (NOLOCK) on A.DocTransID = B.ID
				inner Join Proj C WITH (NOLOCK) on B.TransID = C.Id
				inner Join Cust D WITH (NOLOCK) on C.CustID = D.ID
		' + @WhereCond  + ') Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  
		exec (@SqlStatement)
		
RETURN 0



