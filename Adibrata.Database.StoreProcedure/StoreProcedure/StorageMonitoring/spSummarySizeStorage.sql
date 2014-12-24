CREATE PROCEDURE [dbo].[spSummarySizeStorage]
    -- Add the parameters for the stored procedure here

AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
select COUNT(*) as totalfile ,
(select TOP 1 DocTransBinary.Id from DocTransBinary order by SizeFileBytes Desc) as MaxFileID,
(select TOP 1 DocTransBinary.Id from DocTransBinary order by SizeFileBytes asc) as MinFileID,
 MAX(doctransbinary.SizeFileBytes) as Maximum,min(doctransbinary.SizeFileBytes) as Minimum,
 SUM(doctransbinary.SizeFileBytes) as Summary,AVG(doctransbinary.SizeFileBytes) as Average from doctransbinary with (nolock)
END
