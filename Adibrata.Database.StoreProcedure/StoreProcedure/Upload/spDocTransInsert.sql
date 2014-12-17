﻿
-- =============================================
-- Author:		Nurul Fredyani Tanaya
-- Create date: 20141110
-- Description:	insert ke tabel DocTrans
-- =============================================
ALTER PROCEDURE [dbo].[spDocTransInsert]
	-- Add the parameters for the stored procedure here
	@TransId Varchar(50),
	@docType varchar(50)

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

-- Insert statements for procedure here
Declare @TransIDInt BigInt
Select @TransIDInt = ID from Proj With (nolock) where ProjCode = @TransId


INSERT INTO DocTrans
(
	TransID,
	DocTransCode,
	DocTypeCode,
	UsrCrt,
	DtmCrt,
	UsrUpd,
	DtmUpd
) 
OUTPUT inserted.Id 
VALUES
(
	@TransIDInt,
	@TransId,
	@docType,
	'sa',
	GETDATE(),
	'sa',
	GETDATE()
)
		 IF (@@ERROR <> 0) BEGIN
        ROLLBACK TRAN A
        RETURN 1
    END

COMMIT TRAN A

RETURN 0

END
