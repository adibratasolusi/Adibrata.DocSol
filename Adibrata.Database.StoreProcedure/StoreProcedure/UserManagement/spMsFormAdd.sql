ALTER PROCEDURE [dbo].[spMsFormAdd]
	@FormCode Varchar(50), 
	@FormURL Varchar(255), 
	@FormName Varchar(50), 
	@ISActive int, 
	@UsrCrt varchar(50), 
	@DtmCrt smallDatetime
AS
set Nocount On 
	Insert Into Ms_Form (FormCode, FormName, FormUrl, IsActive, UsrCrt, DtmCrt)
	values (@FormCode, @FormName, @FormURL, @ISActive, @UsrCrt, Getdate())

RETURN 0
