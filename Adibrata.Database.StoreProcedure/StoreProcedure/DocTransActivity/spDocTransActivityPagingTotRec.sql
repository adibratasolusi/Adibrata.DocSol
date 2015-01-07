CREATE PROCEDURE [dbo].[spDocTransActivityPagingTotRec]

		@WhereCond varchar(8000), 
		@SortBy Varchar(8000)
AS
Set NoCount On 
Declare 
@TotalRecord int, @SqlStatement Varchar(8000)


	Set @SqlStatement = 'Select * from  
				(Select  Count(1) as TotalRecord
								From DocTransActivity A With (nolock) inner Join DocTrans B  With (nolock) on A.DocTransId = B.ID
								inner Join Proj C  With (nolock) on B.TransID = C.ID 
								Inner Join Cust D  With (nolock) on C.CustID = D.ID
								' + @WhereCond + ') Qry '
		
			exec (@SqlStatement)
			

RETURN 0
