
USE [TBIG]
GO
/****** Object:  StoredProcedure [dbo].[spEditDocumentvalue]    Script Date: 1/26/2015 6:28:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spEditDocumentvalue]
    @DocTransId Bigint,
    @DocTypeCode Varchar(50)

AS
Set NoCount On 


Begin
select 
    b.Field2,
    a.DocTransID,
    a.ContentValue,
    b.Result
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



