USE [SMARTCMS]
GO
/****** Object:  StoredProcedure [dbo].[spDocTransSearchPaging]    Script Date: 1/12/2015 1:09:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spDocTransSearchPaging]
		@StartRecord varchar(10), 
	@EndRecord varchar(10), 
	@WhereCond varchar(8000), 
	@SortBy Varchar(8000), 
	@WhereCond2 varchar(8000)
AS
Set NoCount On 
Declare 
@TotalRecord int, @SqlStatement Varchar(max)
Declare @WhereCondSearch varchar(8000), @WhereCondSearch2 varchar(8000), 
@strInnerJoin Varchar(8000)
If @SortBy = '' 
	Set @SortBy = ' B.Rank Asc '

	 --''' +  @WhereCond + '''
	 If @WhereCond <> ''
	 Begin
			Set @WhereCondSearch = ' FreeTextTable (DocTransContent, ContentSearchTag,  ''' +  @WhereCond + ''') As B Inner Join '
			Set @strInnerJoin = ' on A.ID= B.[Key] '

	 END
	 else
	 BegiN
			 Set @WhereCondSearch = ''
			 set @strInnerJoin = ''
	 END

	 If @WhereCond2 <> ''
	 Begin
			Set @WhereCondSearch2 = 'Where  ' + @WhereCond2
	 END
	 ELSE
	 BEGIN
			Set @WhereCondSearch2 = ' '
	 END



	Set @SqlStatement = '  Select * from 
 (Select    ROW_NUMBER() OVER (Order By ProjName) as number, *
From 
				(Select  distinct C.Id, 
				 C.DocTransCode, C.DocTypeCode, C.DocTransStatus, D.ProjCode, D.ProjName, D.ProjType, E.CustName, 
				 F.FileName, F.DateCreated, F.SizeFileBytes, F.Pixel, F.ComputerName, F.DPI
				from ' + @WhereCondSearch + '
				 DocTransContent A with (nolock) ' + @strInnerJoin + '
				inner Join DocTrans C with (nolock) on A.DocTransID = C.ID
				inner Join Proj D with (nolock) on C.TransID = D.ID
				Inner Join Cust E with (nolock) on E.Id = d.CustID
				Left Join DocTransBinary F with (nolock) on F.DocTransID = C.ID' +
				@WhereCondSearch2 + '
				) qry1 ) qry2
				where number between ' + @StartRecord  + ' and  ' + @EndRecord  
				
			exec (@SqlStatement)
RETURN 0
