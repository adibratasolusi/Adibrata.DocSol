Alter PROCEDURE [dbo].[spDocTransSearchPaging]
		@StartRecord varchar(10), 
	@EndRecord varchar(10), 
	@WhereCond varchar(8000), 
	@SortBy Varchar(8000)
AS
Set NoCount On 
Declare 
@TotalRecord int, @SqlStatement Varchar(max)
If @SortBy = '' 
	Set @SortBy = ' B.Rank Asc '

	 --''' +  @WhereCond + '''




	Set @SqlStatement = '  Select * from 
 (Select    ROW_NUMBER() OVER (Order By ProjName) as number, *
From 
				(Select  distinct C.Id, 
				 C.DocTransCode, C.DocTypeCode, C.DocTransStatus, D.ProjCode, D.ProjName, D.ProjType, E.CustName, 
				 F.FileName, F.DateCreated, F.SizeFileBytes, F.Pixel, F.ComputerName, F.DPI
				from
				FreeTextTable (DocTransContent, ContentSearchTag,  ''' +  @WhereCond + ''') As B
				Inner Join  DocTransContent A with (nolock) on A.ID= B.[Key]
				inner Join DocTrans C with (nolock) on A.DocTransID = C.ID
				inner Join Proj D with (nolock) on C.TransID = D.ID
				Inner Join Cust E with (nolock) on E.Id = d.CustID
				Left Join DocTransBinary F with (nolock) on F.DocTransID = C.ID) qry1 ) qry2
				where number between ' + @StartRecord  + ' and  ' + @EndRecord  
				print @SqlStatement
			exec (@SqlStatement)
RETURN 0
