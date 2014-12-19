-- =============================================
-- Author:		Nurul Fredyani Tanaya
-- Create date: 20141218
-- Description:	Stored Procedures fo Upload Inquiry
-- =============================================
CREATE PROCEDURE spUploadInquiry
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
	Set @SortBy = ' TransID Asc '

	Set @SqlStatement = 'Select * from 
		(Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, TransID,DocTypeCode
		From DocTrans With (nolock)  ' + @WhereCond  + ') Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  
		exec (@SqlStatement)
Set @TotalRecord =  @@ROWCOUNT
RETURN 0
