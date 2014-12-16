Alter PROCEDURE [dbo].[spMenuSearch]
	@Crit varchar(50)
AS
Set NoCount On 
IF LOWER(@Crit) = 'all' Or @Crit= ''
BEGIN
	SELECT FormName,FormURL from MS_Form With (nolock)
END
ELSE
BEGIN
	SELECT FormName,FormURL from MS_Form with (nolock) 
		where Contains ((FormName, FormURL), @Crit)
		-- FormName like '%'+@Crit+'%' or FormURL like '%'+@Crit+'%'
END
RETURN 0

