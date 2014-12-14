CREATE PROCEDURE [dbo].[spUserRegisterListReport]
AS
set NoCount On
SELECT * FROM MSUser with (nolock) 

