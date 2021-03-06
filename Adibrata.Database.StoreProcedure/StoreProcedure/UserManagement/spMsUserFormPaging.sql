﻿CREATE PROCEDURE [dbo].[spMsUserFormPaging]--sama kohen (spMsUserFormPaging)
    @StartRecord varchar(7), 
    @EndRecord varchar(7), 
    @WhereCond varchar(8000), 
    @SortBy Varchar(8000)
AS
Set NoCount On 
    Set NoCount On 
Declare @RecordNumber Numeric, 
        @SqlStatement Varchar(max)
Set NoCount On 
Declare @TotalRecord int
If @SortBy = '' 
    Set @SortBy = ' FormName Asc '

    Set @SqlStatement = 'Select * from 
        (Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, Id,FormName from MS_Form where ISActive = 1 ' + @WhereCond  + ') Qry
        where number between ' + @StartRecord  + ' and  ' + @EndRecord  
        exec (@SqlStatement)
Set @TotalRecord =  @@ROWCOUNT
RETURN 0