-- =============================================
-- Author:		Nurul Fredyani Tanaya
-- Create date: 20141218
-- Description:	Stored Procedures fo Upload Inquiry
-- =============================================
ALTER PROCEDURE [dbo].[spUploadInquiry]
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
	Set @SortBy = ' DocTrans.TransID Asc '

	Set @SqlStatement = 'Select * from 
		(Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, DocTransCode, TransID, DocTrans.DocTypeCode, Proj.ProjCode, Proj.ProjName, Doctrans.DocTransStatus ,
		Cust.CustCode, Cust.CustName, B.DateCreated, B.SizeFileBytes, B.Pixel, B.ComputerName, B.FileName
		
		From DocTrans With (nolock) Inner Join 	Proj with (nolock) on Doctrans.Transid = Proj.ID 
		inner join cust with (nolock) on Proj.CustID = cust.id
		Inner Join DocTransContent A with (nolock) on A.DocTransID = DocTrans.ID
		Inner Join DocTransBinary B with (nolock) on B.DocTransID = DocTrans.ID
		  ' 
		+ @WhereCond  + ') Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  
		
		exec (@SqlStatement)
Set @TotalRecord =  @@ROWCOUNT
RETURN 0
