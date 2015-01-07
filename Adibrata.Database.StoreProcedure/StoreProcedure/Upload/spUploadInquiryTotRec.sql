CREATE PROCEDURE [dbo].[spUploadInquiryTotRec]
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
		(Select Count(1) as TotalRecord
		From DocTrans With (nolock) Inner Join 	Proj with (nolock) on Doctrans.Transid = Proj.ID 
		inner join cust on Proj.CustID = cust.id
		  ' 
		+ @WhereCond  + ') Qry'

		exec (@SqlStatement)
Set @TotalRecord =  @@ROWCOUNT
RETURN 0
