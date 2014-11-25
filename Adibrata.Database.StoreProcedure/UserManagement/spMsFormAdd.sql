CREATE PROCEDURE [dbo].[spMsFormAdd]
	@FormName Varchar(50), 
	@FormURL Varchar(255), 
	@ISActive int, 
	@UsrCrt varchar(50)
AS
set Nocount On 
	Insert Into Ms_Form (FormName, FormUrl, IsActive, UsrCrt, DtmCrt)
	values (@FormName, @FormURL, @ISActive, @UsrCrt, Getdate())

RETURN 0
