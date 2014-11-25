-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fnSecurityCheckForm]
(
	-- Add the parameters for the function here
	@UserName varchar(50), 
	@FormCode varchar(50)
)
RETURNS BIT
AS
BEGIN
	Declare @Valid bit
	if Exists (Select 1 from MS_UserMenu with (nolock) Inner Join MS_Form  a with (nolock) on MS_UserMenu.MenuID = a.ID Inner Join MS_User with (nolock) on Ms_User.ID = MS_UserMenu.ID
			where MS_User.UserName=@UserName and Ms_Form.ID =MS_UserMenu.ID
			)
		Set @Valid = 1
	Else
		Set @Valid = 0
	RETURN @Valid
END
GO


