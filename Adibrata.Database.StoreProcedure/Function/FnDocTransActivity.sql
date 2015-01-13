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
Alter FUNCTION dbo.FnDocTransActivity
(
	-- Add the parameters for the function here
	@DocTransID BigInt
)
RETURNS varchar(8000)
AS
BEGIN
	-- Declare the return variable here
	Declare @StartLoop Int
	Declare @EndLoop Int
	Declare @StrActivity varchar(8000)
	Declare @tblDocTrans Table
	(SeqNo Int Identity, 
	UserName varchar(50), 
	ComputerName varchar(50), 
	DtmAccess datetime
	)
	Declare @UserName varchar(50), 
			@DtmAccess datetime, 
			@ComputerName varchar(50)
	Insert Into @tblDocTrans (UserName, DtmAccess, ComputerName)
	Select top 1 UserName, Dtm, ComputerName from DoctransActivity with (nolock) 
		Inner Join DocTransBinary with (nolocK) on DoctransActivity.DocTransID = DocTransBinary.DocTransID
	where DoctransActivity.DocTransID = @DocTransID order by dtm

	Set @EndLoop = @@Rowcount
	Set @StartLoop = 1
	Set @StrActivity = 'Last Access'
	While @StartLoop <= @EndLoop
	Begin
		Select @UserName = UserName, @DtmAccess = DtmAccess, @ComputerName = ComputerName from @tblDocTrans where SeqNo = @Startloop
		Select @strActivity = @strActivity + ' by ' + @UserName + ' from ' + @ComputerName + 'at ' + Convert (varchar(30), @DtmAccess) 
		Set @StartLoop = @StartLoop + 1
	END	
	Return @strActivity
END
GO

