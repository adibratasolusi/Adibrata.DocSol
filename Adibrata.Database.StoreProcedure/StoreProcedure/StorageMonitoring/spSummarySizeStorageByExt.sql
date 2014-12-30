ALTER PROCEDURE [dbo].[spSummarySizeStorageByExt]
    -- Add the parameters for the stored procedure here
       @Ext varchar (10 )
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON ;


select COUNT (*) as totalfile ,
( select TOP 1 DocTransBinary. Id from DocTransBinary where
 dbo .GetColumnValue ( DocTransBinary. FileName ,'.' , 2) = @Ext order   by SizeFileBytes Desc )  as MaxFileID,
( select TOP 1 DocTransBinary. Id from DocTransBinary where
 dbo .GetColumnValue ( DocTransBinary. FileName ,'.' , 2) = @Ext order by SizeFileBytes asc ) as  MinFileID,
  MAX( doctransbinary.SizeFileBytes ) as Maximum ,min ( doctransbinary.SizeFileBytes ) as Minimum,
  SUM( doctransbinary.SizeFileBytes ) as Summary ,AVG ( doctransbinary.SizeFileBytes ) as Average from doctransbinary with ( nolock)


  where
 dbo.GetColumnValue ( DocTransBinary. FileName ,'.' , 2) = @Ext
END
