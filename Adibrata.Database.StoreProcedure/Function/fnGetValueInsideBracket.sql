USE [TBIG]
GO
/****** Object:  UserDefinedFunction [dbo].[fnGetValueInsideBracket]    Script Date: 2/3/2015 1:53:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[fnGetValueInsideBracket] 
(
    -- Add the parameters for the function here
    @bla VARCHAR(50)
)
RETURNS varchar(8000)
AS
BEGIN
    -- Declare the return variable here
    
    Declare @Str varchar(8000)
SELECT @Str = SUBSTRING(
         @bla, 
         CHARINDEX('(', @bla) + 1, 
         CHARINDEX(')', @bla) - CHARINDEX('(', @bla) - 1
       )

return @Str

END
