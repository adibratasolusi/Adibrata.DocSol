
/****** Object:  StoredProcedure [dbo].[spMenuSearch]    Script Date: 1/12/2015 1:00:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spMenuSearch]
	@Crit varchar(50)
AS
Set NoCount On 
IF LOWER(@Crit) = 'all' Or @Crit= ''
BEGIN
	SELECT FormName,FormURL from MS_Form With (nolock) order by formName
END
ELSE
BEGIN
	SELECT FormName,FormURL from MS_Form with (nolock) 
	inner join  FreeTextTable (MS_Form, FormName,  'Upload Document') As B
	on B.[Key] = Ms_Form.ID
	order by formName
		-- FormName like '%'+@Crit+'%' or FormURL like '%'+@Crit+'%'
END
RETURN 0