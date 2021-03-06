﻿DELETE PROCEDURE [dbo].[spArchieveApprovalPaging]
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
    Set @SortBy = ' DocTransCode Asc '

    Set @SqlStatement = 'Select * from 
        (Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, Id,DocTransCode,DocTypeCode 
        from DocTrans with (nolock) where  (ArchiveStatus = ''PREPARE'' or ArchiveStatus = ''HOLD'') ' + @WhereCond  + ') Qry
        where number between ' + @StartRecord  + ' and  ' + @EndRecord  
        exec (@SqlStatement)
Set @TotalRecord =  @@ROWCOUNT
RETURN 0