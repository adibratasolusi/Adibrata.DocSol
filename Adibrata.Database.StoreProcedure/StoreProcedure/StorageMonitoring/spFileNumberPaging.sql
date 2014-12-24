CREATE PROCEDURE [dbo].[spFileNumberPaging]
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
	Set @SortBy = ' (dbo.GetColumnValue(DocTransBinary.FileName,''.'',2)) Asc '

	Set @SqlStatement = 'Select * from  
		(Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number,(dbo.GetColumnValue(DocTransBinary.FileName,''.'',2))  Ext,  COUNT(*) as JumlahFile,SUM(SizeFileBytes) as TotalSize  from DocTransBinary
		 with (nolock)
		' + @WhereCond + 'group by (dbo.GetColumnValue(FileName,''.'',2))) Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  
		exec (@SqlStatement)
Set @TotalRecord =  @@ROWCOUNT
RETURN 0

