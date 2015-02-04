USE [TBIG]
GO
/****** Object:  StoredProcedure [dbo].[spEditDocumentvalue]    Script Date: 2/3/2015 3:35:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[spEditDocumentvalue]
    @DocTransId Bigint,
    @DocTypeCode Varchar(50)

AS
Set NoCount On 


Begin
select 
	b.Field1,
    b.Field2,
	dbo.fnGetResult(b.Result) Result,
	UPPER(dbo.fnGetValueInsideBracket(b.Result)) DataType,
    a.DocTransID,
    a.ContentValue,
	a.Id

from  
    RuDocContentItem b  join DocTransContent a 
on 
    a.ContentName = b.Field2 
where 
    b.Field1 = @DocTypeCode 
        and
    a.DocTransID = @DocTransId
END
RETURN 0
