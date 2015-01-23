USE [TBIG]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



Create PROCEDURE [dbo].[spEditDocumentvalue]
	@DocTransId Bigint,
	@DocTypeCode Varchar(50)

AS
Set NoCount On 


Begin
select 
        a.DocTransID, 
        a.ContentName,  
        a.DocTypeCode, 
        a.ContentValue,
        b.Result 
from 
        DocTransContent a with (nolock)
        join  
        RuDocContentItem b with (nolock) 
        on 
        a.ContentName = b.Field2 
where 
        a.DocTypeCode = @DocTypeCode 
        and 
        a.DocTransID = @DocTransId
END
RETURN 0




