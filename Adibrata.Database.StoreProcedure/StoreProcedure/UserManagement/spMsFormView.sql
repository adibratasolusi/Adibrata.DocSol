CREATE PROCEDURE [dbo].[spMsFormView]
	@FormID BigInt
AS
set NoCount On 
	SELECT ID, FormName, FormURL, IsActive From MS_Form with (nolock) where ID = @FormID
RETURN 0
