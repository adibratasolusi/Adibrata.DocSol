CREATE PROCEDURE [dbo].[spMsFormView]
	@FormName varchar(50)
AS
set NoCount On 
	SELECT ID, FormName, FormURL, IsActive From MS_Form with (nolock) where FormName = @FormName
RETURN 0
