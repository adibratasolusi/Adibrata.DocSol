
-- =============================================
-- Author:		Nurul Fredyani Tanaya
-- Create date: 20141110
-- Description:	insert ke tabel DocTrans
-- =============================================
ALTER PROCEDURE [dbo].[spDocTransInsert]
	-- Add the parameters for the stored procedure here
	@TransId Varchar(50),
	@docType varchar(50), 
	@UsrCrt Varchar(50)

AS
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
Declare @DocTransCode varchar(50)
Exec spMasterSequence 1, 'UPL', @DtmCrt, @DocTransCode Output
-- Insert statements for procedure here
Declare @TransIDInt BigInt
Select @TransIDInt = ID from Proj With (nolock) where ProjCode = @TransId


INSERT INTO DocTrans
(
	TransID,
	DocTransCode,
	DocTypeCode,
	DocTransStatus,
	UsrCrt,
	DtmCrt
) 
OUTPUT inserted.Id 
VALUES
(
	@TransIDInt,
	@DocTransCode,
	@DocType,
	'ACTIVE',
	@UsrCrt,
	GETDATE()
)

RETURN 0
