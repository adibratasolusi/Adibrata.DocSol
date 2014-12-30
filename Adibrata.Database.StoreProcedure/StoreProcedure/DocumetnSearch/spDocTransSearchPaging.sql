/****** Object:  StoredProcedure [dbo].[spDocTransSearchPaging]    Script Date: 12/25/2014 2:13:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[spDocTransSearchPaging]
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

	Set @SqlStatement = 'Select * from  
				(Select  Distinct ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number,C.Id, 
				 C.DocTransCode, C.DocTypeCode, C.DocTransStatus, D.ProjCode, D.ProjName, D.ProjType, E.CustName
				from
				FreeTextTable (DocTransContent, ContentSearchTag, ''' +  @WhereCond + ''') As B
				Inner Join  DocTransContent A with (nolock) on A.ID= B.[Key]
				inner Join DocTrans C with (nolock) on A.DocTransID = C.ID
				inner Join Proj D with (nolock) on C.TransID = D.ID
				Inner Join Cust E with (nolock) on E.Id = d.CustID) Qry
		where number between ' + @StartRecord  + ' and  ' + @EndRecord  
			exec (@SqlStatement)
RETURN 0
