﻿CREATE PROCEDURE [dbo].[spFileStoragePaging]
    @StartRecord varchar(10), 
    @EndRecord varchar(10), 
    @WhereCond varchar(8000), 
    @SortBy Varchar(8000)
AS
Set NoCount On 
Declare @RecordNumber Numeric, 
        @SqlStatement Varchar(max)
Set NoCount On 
Declare @TotalRecord int
If @SortBy = '' 
    Set @SortBy = ' DocTransCode Asc '

    Set @SqlStatement = 'Select * from                
        (Select ROW_NUMBER() OVER (Order By ' + @SortBy + ') as number, dt.DocTransCode,dt.DocTypeCode,dtb.FileName,dtb.SizeFileBytes,dtb.Pixel, dtb.ComputerName
        From DocTransBinary dtb  join DocTrans dt  on dtb.DocTransID =dt.Id ' + @WhereCond  + ') Qry
        where number between ' + @StartRecord  + ' and  ' + @EndRecord  
        exec (@SqlStatement)
Set @TotalRecord =  @@ROWCOUNT