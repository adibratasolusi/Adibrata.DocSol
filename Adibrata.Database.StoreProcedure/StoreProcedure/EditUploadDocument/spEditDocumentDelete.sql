USE [TBIG]
GO
/****** Object:  StoredProcedure [dbo].[spEditDocumentDelete]    Script Date: 2/3/2015 3:30:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[spEditDocumentDelete]
    -- Add the parameters for the stored procedure here
    @DocTransId bigint
 
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here

   delete DocTransContent where 
   DocTransID =@DocTransId
  

END