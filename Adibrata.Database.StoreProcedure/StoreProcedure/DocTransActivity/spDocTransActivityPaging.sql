ALter PROCEDURE [dbo].[spDocTransActivityPaging]
		@StartRecord varchar(10), 
		@EndRecord varchar(10), 
		@WhereCond varchar(8000), 
		@SortBy Varchar(8000)
AS
Set NoCount On 
Declare 
@TotalRecord int, @SqlStatement Varchar(8000)

If @SortBy = '' 
	set @SortBy =  'C.ProjName Asc '

	Set @SqlStatement = 'Select * from  
				(Select   ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, 
								A.UserName, B.DocTransCode, B.DocTypeCode, C.ProjName, D.CustName 
								From DocTransActivity A With (nolock) inner Join DocTrans B  With (nolock) on A.DocTransId = B.ID
								inner Join Proj C  With (nolock) on B.TransID = C.ID 
								Inner Join Cust D  With (nolock) on C.CustID = D.ID
								' + @WhereCond + ') Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  

			exec (@SqlStatement)
			
RETURN 0
