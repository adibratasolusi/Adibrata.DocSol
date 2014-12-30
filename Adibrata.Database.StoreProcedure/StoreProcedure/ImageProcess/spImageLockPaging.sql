ALTER  PROCEDURE [dbo].[spImageLockPaging]
	@StartRecord varchar(10), 
	@EndRecord varchar(10), 
	@WhereCond varchar(8000), 
	@SortBy Varchar(8000)
AS
Set NoCount On 
Declare 
@RecordNumber Numeric, 
@SqlStatement Varchar(max)
Set NoCount On 
Declare 
@TotalRecord int
If @SortBy = '' 
	Set @SortBy = ' TransID Asc '

	Set @SqlStatement = 'Select * from  
		(Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number,Id, TransID, DocTypeCode
		From DocTrans with (nolock) where DocTransStatus = ''ACTIVE''
		' + @WhereCond + ') Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  
		exec (@SqlStatement)
Set @TotalRecord =  @@ROWCOUNT
RETURN 0