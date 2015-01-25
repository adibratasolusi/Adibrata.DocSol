USE [TBIG]
GO
/****** Object:  StoredProcedure [dbo].[spEditUploadDocument]    Script Date: 1/23/2015 4:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Erwin>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spEditUploadDocument]
	-- Add the parameters for the stored procedure here
	@Id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
select c.CustCode,
    c.CustName,
    b.ProjCode,
    b.ProjName,
	b.ProjType,
    a.DocTypeCode, 
    a.DocTransCode,
    a.Id

from 
    DocTrans a with(nolock)
    join proj b with(nolock)
on 
    a.TransID = b.Id 
    join Cust c with(nolock)
on 
    b.CustID = c.ID
Where
    a.Id = @Id
END
